using Microsoft.Extensions.Logging;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;
using Nop.Plugin.Misc.EliteAuctions.Bids.Models;
using Nop.Plugin.Misc.EliteAuctions.Bids.Services;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

namespace Nop.Plugin.Misc.EliteAuctions.MarketPlace
{
    public class MarketPlaceFactory : IMarketPlaceFactory
    {
        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IAuctionService _auctionService;
        private readonly IAuctionStageService _auctionStageService;
        private readonly IAuctionStageHistoryService _auctionStageHistoryService;
        private readonly IBidService _bidService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IPermissionService _permissionService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly INotificationService _notificationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IRepository<ShoppingCartItem> _shoppingCartItemRepository;
        private readonly ILogger<MarketPlaceFactory> _logger;

        #endregion

        #region Constructor

        public MarketPlaceFactory(IWorkContext workContext,
            IStoreContext storeContext,
            IAuctionService auctionService,
            IAuctionStageService auctionStageService,
            IAuctionStageHistoryService auctionStageHistoryService,
            IBidService bidService,
            IProductService productService,
            ICustomerService customerService,
            IPermissionService permissionService,
            IShoppingCartService shoppingCartService,
            INotificationService notificationService,
            IDateTimeHelper dateTimeHelper,
            IRepository<ShoppingCartItem> shoppingCartItemRepository,
            ILogger<MarketPlaceFactory> logger)
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _auctionService = auctionService;
            _auctionStageService = auctionStageService;
            _auctionStageHistoryService = auctionStageHistoryService;
            _bidService = bidService;
            _productService = productService;
            _customerService = customerService;
            _permissionService = permissionService;
            _shoppingCartService = shoppingCartService;
            _notificationService = notificationService;
            _dateTimeHelper = dateTimeHelper;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _logger = logger;
        }

        #endregion

        #region Utilities

        private async Task<Auction> EnsureAuctionExists(AuctionModel auctionModel)
        {
            var auction = await _auctionService.GetAuctionByProductId(auctionModel.ProductId);
            var currentStore = await _storeContext.GetCurrentStoreAsync();

            if (auction == null)
            {
                auction = auctionModel.ToEntity<Auction>();
                auction.ProductId = auctionModel.ProductId;
                auction.WinningCustomerId = null;
                auction.StoreId = currentStore.Id;

                await _auctionService.InsertAuction(auction);
            }

            auction.ExtensionTriggerSeconds = auctionModel.ExtensionTriggerSeconds;
            auction.StartDateTimeUtc = (DateTime)auctionModel.StartDateTimeUtc;
            auction.EndDateTimeUtc = (DateTime)auctionModel.EndDateTimeUtc;
            auction.ReserveBidPrice = auctionModel.ReserveBidPrice;
            auction.StartingBidPrice = auctionModel.StartingBidPrice;

            await _auctionService.UpdateAuction(auction);

            return auction;
        }

        private async Task SetInitialAuctionStage(int auctionId)
        {
            var auctionStageHistory = await _auctionStageHistoryService.GetCurrentAuctionStageByAuctionId(auctionId);
            if (auctionStageHistory == null)
            {
                var auctionStage = await _auctionStageService.GetInitialAuctionStage();

                await _auctionStageHistoryService.InsertAuctionStageHistory(new AuctionStageHistory
                {
                    AuctionId = auctionId,
                    AuctionStageId = auctionStage.Id
                });
            }
        }

        private async Task<(bool IsInitialBid, Bid bid)> GetOrInitializeWinningBid(int auctionId, Customer currentCustomer, decimal bidAmount)
        {
            var currentWinningBid = await _bidService.GetHighestBid(auctionId);
            var auction = await _auctionService.GetAuctionById(auctionId);

            if (currentWinningBid == null && bidAmount >= auction.StartingBidPrice)
            {
                currentWinningBid = new Bid
                {
                    AuctionId = auctionId,
                    CustomerId = currentCustomer.Id,
                    Amount = bidAmount
                };

                await _bidService.InsertBid(currentWinningBid);

                await MoveAuctionToNextStage(auctionId);

                _notificationService.SuccessNotification("Bid successfully placed. You are the new winning bid");

                return (true, currentWinningBid);
            }

            return (false, currentWinningBid);
        }

        private async Task<Bid> ValidateAndPlaceBid(int auctionId, decimal currentWinningBidAmount, decimal customerBidAmount, Customer currentCustomer)
        {
            if (customerBidAmount <= currentWinningBidAmount)
            {
                return null;
            }

            var newBid = new Bid
            {
                AuctionId = auctionId,
                CustomerId = currentCustomer.Id,
                Amount = customerBidAmount
            };

            await _bidService.InsertBid(newBid);

            _notificationService.SuccessNotification("Bid successfully placed.");

            return newBid;
        }

        private async Task UpdateProductMinimumCustomerEnteredPrice(decimal currentWinningBid, int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            product.MinimumCustomerEnteredPrice = currentWinningBid;
            await _productService.UpdateProductAsync(product);
        }

        #endregion

        #region Methods

        public async Task MoveAuctionToNextStage(int auctionId)
        {
            var currentAuctionStageFromHistory = await _auctionStageHistoryService.GetCurrentAuctionStageByAuctionId(auctionId);
            var nextStage = await _auctionStageService.GetAuctionStageById(currentAuctionStageFromHistory.AuctionStageId + 1);

            if (nextStage != null)
            {
                var auctionStageHistory = new AuctionStageHistory
                {
                    AuctionId = auctionId,
                    AuctionStageId = nextStage.Id
                };

                await _auctionStageHistoryService.InsertAuctionStageHistory(auctionStageHistory);
            }
        }

        public async Task PrepareProductAuction(AuctionModel auctionModel)
        {
            var auction = await EnsureAuctionExists(auctionModel);

            await SetInitialAuctionStage(auction.Id);

            _notificationService.SuccessNotification("Auction settings applied");
        }

        public async Task PlaceBid(BidModel bidModel)
        {
            var currentCustomer = await _workContext.GetCurrentCustomerAsync();

            var auction = await _auctionService.GetAuctionByProductId(bidModel.ProductId);
            if (auction == null)
                _notificationService.ErrorNotification("You cannot make a bid on this product. Product not on auction.");

            var product = await _productService.GetProductByIdAsync(auction.ProductId);

            if (product == null)
                _notificationService.ErrorNotification("Product not available");

            var now = _dateTimeHelper.ConvertToUtcTime(DateTime.UtcNow);
            var endDate = _dateTimeHelper.ConvertToUtcTime(auction.EndDateTimeUtc);

            if (now <= endDate)
            {
                var currentWinningBid = await GetOrInitializeWinningBid(auction.Id, currentCustomer, bidModel.CustomerEnteredPrice);

                if (!currentWinningBid.IsInitialBid)
                    currentWinningBid.bid = await ValidateAndPlaceBid(auction.Id, currentWinningBid.bid.Amount, bidModel.CustomerEnteredPrice, currentCustomer);

                if (currentWinningBid.bid == null)
                {
                    var effectiveBidFloor = Math.Max(product.MinimumCustomerEnteredPrice, auction.StartingBidPrice);

                    _notificationService.WarningNotification($"Your bid must be higher than {effectiveBidFloor:C}");
                }
                else
                {
                    await UpdateProductMinimumCustomerEnteredPrice(currentWinningBid.bid.Amount, bidModel.ProductId);
                    auction.CurrentBidPrice = currentWinningBid.bid.Amount;
                    await _auctionService.UpdateAuction(auction);
                }
            }
            else
            {
                _notificationService.WarningNotification($"Auction Ended");
            }
        }

        #endregion
    }
}
