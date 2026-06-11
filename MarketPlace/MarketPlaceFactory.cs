using Microsoft.Extensions.Logging;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;
using Nop.Plugin.Misc.EliteAuctions.Bids.Models;
using Nop.Plugin.Misc.EliteAuctions.Bids.Services;
using Nop.Services.Catalog;
using Nop.Services.Messages;

namespace Nop.Plugin.Misc.EliteAuctions.MarketPlace
{
    public class MarketPlaceFactory : IMarketPlaceFactory
    {
        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IAuctionService _auctionService;
        private readonly IAuctionStageService _auctionStageService;
        private readonly IAuctionStageHistoryService _auctionStageHistoryService;
        private readonly IBidService _bidService;
        private readonly IProductService _productService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<MarketPlaceFactory> _logger;

        #endregion

        #region Constructor

        public MarketPlaceFactory(IWorkContext workContext,
            IAuctionService auctionService,
            IAuctionStageService auctionStageService,
            IAuctionStageHistoryService auctionStageHistoryService,
            IBidService bidService,
            IProductService productService,
            INotificationService notificationService,
            ILogger<MarketPlaceFactory> logger)
        {
            _workContext = workContext;
            _auctionService = auctionService;
            _auctionStageService = auctionStageService;
            _auctionStageHistoryService = auctionStageHistoryService;
            _bidService = bidService;
            _productService = productService;
            _notificationService = notificationService;
            _logger = logger;
        }

        #endregion

        #region Utilities

        private async Task<Auction> EnsureAuctionExists(int productId)
        {
            var auction = await _auctionService.GetAuctionByProductId(productId);

            if (auction == null)
            {
                auction = new Auction
                {
                    ProductId = productId,
                    WinningCustomerId = null
                };

                await _auctionService.InsertAuction(auction);
            }

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

        private async Task MoveAuctionToNextStage(int auctionId)
        {
            var currentAuctionStageFromHistory = await _auctionStageHistoryService.GetCurrentAuctionStageByAuctionId(auctionId);
            var nextStage = await _auctionStageService.GetAuctionStageById(currentAuctionStageFromHistory.AuctionStageId++);

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

        private async Task<(bool IsInitialBid, Bid bid)> GetOrInitializeWinningBid(int auctionId, Customer currentCustomer, decimal bidAmount)
        {
            var currentWinningBid = await _bidService.GetHighestBid(auctionId);
            if (currentWinningBid == null)
            {
                currentWinningBid = new Bid
                {
                    AuctionId = auctionId,
                    CustomerId = currentCustomer.Id,
                    Amount = bidAmount
                };

                await _bidService.InsertBid(currentWinningBid);

                await MoveAuctionToNextStage(auctionId);

                _notificationService.SuccessNotification("Bid successfully placed. You are the first and winning bid");

                return (true, currentWinningBid);
            }

            return (false, currentWinningBid);
        }

        private async Task<Bid> ValidateAndPlaceBid(int auctionId, decimal currentWinningBidAmount, decimal customerBidAmount, Customer currentCustomer)
        {
            if (customerBidAmount <= currentWinningBidAmount)
            {
                _notificationService.WarningNotification(
                    $"Your bid must be higher than the current winning bid of {currentWinningBidAmount:C}");
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
            product.MinimumCustomerEnteredPrice = currentWinningBid + 1m;
            await _productService.UpdateProductAsync(product);
        }

        #endregion

        #region Methods

        public async Task PrepareProductAuction(AuctionModel auctionModel)
        {
            var auction = await EnsureAuctionExists(auctionModel.ProductId);

            await SetInitialAuctionStage(auction.Id);
        }

        public async Task PlaceBid(BidModel bidModel)
        {
            var currentCustomer = await _workContext.GetCurrentCustomerAsync();

            var auction = await _auctionService.GetAuctionByProductId(bidModel.ProductId);
            if (auction == null)
                _notificationService.ErrorNotification("You cannot make a bid on this product. Product not on auction yet.");


            var currentWinningBid = await GetOrInitializeWinningBid(auction.Id, currentCustomer, bidModel.CustomerEnteredPrice);

            if (!currentWinningBid.IsInitialBid)
                currentWinningBid.bid = await ValidateAndPlaceBid(auction.Id, currentWinningBid.bid.Amount, bidModel.CustomerEnteredPrice, currentCustomer);


            await UpdateProductMinimumCustomerEnteredPrice(currentWinningBid.bid.Amount, bidModel.ProductId);
        }

        #endregion
    }
}
