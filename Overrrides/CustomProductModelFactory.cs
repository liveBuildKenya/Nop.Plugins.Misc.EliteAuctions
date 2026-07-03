using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;
using Nop.Core.Events;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping.Date;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Factories;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.EliteAuctions.Overrrides
{
    public class CustomProductModelFactory : ProductModelFactory
    {
        private readonly IEventPublisher _eventPublisher;
        protected readonly CaptchaSettings _captchaSettings;
        protected readonly CatalogSettings _catalogSettings;
        protected readonly CustomerSettings _customerSettings;
        protected readonly GpsrSettings _gpsrSettings;
        protected readonly ICategoryService _categoryService;
        protected readonly ICurrencyService _currencyService;
        protected readonly ICustomerService _customerService;
        protected readonly ICustomWishlistService _customWishlistService;
        protected readonly IDateRangeService _dateRangeService;
        protected readonly IDateTimeHelper _dateTimeHelper;
        protected readonly IDownloadService _downloadService;
        protected readonly IGenericAttributeService _genericAttributeService;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IJsonLdModelFactory _jsonLdModelFactory;
        protected readonly ILocalizationService _localizationService;
        protected readonly IManufacturerService _manufacturerService;
        protected readonly IPermissionService _permissionService;
        protected readonly IPictureService _pictureService;
        protected readonly IPriceCalculationService _priceCalculationService;
        protected readonly IPriceFormatter _priceFormatter;
        protected readonly IProductAttributeParser _productAttributeParser;
        protected readonly IProductAttributeService _productAttributeService;
        protected readonly IProductReviewService _productReviewService;
        protected readonly IProductService _productService;
        protected readonly IProductTagService _productTagService;
        protected readonly IProductTemplateService _productTemplateService;
        protected readonly IReviewTypeService _reviewTypeService;
        protected readonly IShoppingCartService _shoppingCartService;
        protected readonly ISpecificationAttributeService _specificationAttributeService;
        protected readonly IStaticCacheManager _staticCacheManager;
        protected readonly IStoreContext _storeContext;
        protected readonly IStoreService _storeService;
        protected readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        protected readonly ITaxService _taxService;
        protected readonly IUrlRecordService _urlRecordService;
        protected readonly IVendorService _vendorService;
        protected readonly IVideoService _videoService;
        protected readonly IWebHelper _webHelper;
        protected readonly IWorkContext _workContext;
        protected readonly MediaSettings _mediaSettings;
        protected readonly OrderSettings _orderSettings;
        protected readonly SeoSettings _seoSettings;
        protected readonly ShippingSettings _shippingSettings;
        protected readonly VendorSettings _vendorSettings;

        public CustomProductModelFactory(IEventPublisher eventPublisher,
            CaptchaSettings captchaSettings,
            CatalogSettings catalogSettings,
            CustomerSettings customerSettings,
            GpsrSettings gpsrSettings,
            ICategoryService categoryService,
            ICurrencyService currencyService,
            ICustomerService customerService,
            ICustomWishlistService customWishlistService,
            IDateRangeService dateRangeService,
            IDateTimeHelper dateTimeHelper,
            IDownloadService downloadService,
            IGenericAttributeService genericAttributeService,
            IHttpContextAccessor httpContextAccessor,
            IJsonLdModelFactory jsonLdModelFactory,
            ILocalizationService localizationService,
            IManufacturerService manufacturerService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IPriceCalculationService priceCalculationService,
            IPriceFormatter priceFormatter,
            IProductAttributeParser productAttributeParser,
            IProductAttributeService productAttributeService,
            IProductReviewService productReviewService,
            IProductService productService,
            IProductTagService productTagService,
            IProductTemplateService productTemplateService,
            IReviewTypeService reviewTypeService,
            IShoppingCartService shoppingCartService,
            ISpecificationAttributeService specificationAttributeService,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            IStoreService storeService,
            IShoppingCartModelFactory shoppingCartModelFactory,
            ITaxService taxService,
            IUrlRecordService urlRecordService,
            IVendorService vendorService,
            IVideoService videoService,
            IWebHelper webHelper,
            IWorkContext workContext,
            MediaSettings mediaSettings,
            OrderSettings orderSettings,
            SeoSettings seoSettings,
            ShippingSettings shippingSettings,
            VendorSettings vendorSettings) : base(captchaSettings, catalogSettings, customerSettings, gpsrSettings, categoryService, currencyService, customerService,
                customWishlistService, dateRangeService, dateTimeHelper, downloadService, genericAttributeService, httpContextAccessor, jsonLdModelFactory, localizationService,
                manufacturerService, permissionService, pictureService, priceCalculationService, priceFormatter, productAttributeParser, productAttributeService, productReviewService, productService,
                productTagService, productTemplateService, reviewTypeService, shoppingCartService, specificationAttributeService, staticCacheManager, storeContext, storeService, shoppingCartModelFactory,
                taxService, urlRecordService, vendorService, videoService, webHelper, workContext, mediaSettings, orderSettings, seoSettings, shippingSettings, vendorSettings)
        {
            _eventPublisher = eventPublisher;
            _captchaSettings = captchaSettings;
            _catalogSettings = catalogSettings;
            _customerSettings = customerSettings;
            _gpsrSettings = gpsrSettings;
            _categoryService = categoryService;
            _currencyService = currencyService;
            _customerService = customerService;
            _customWishlistService = customWishlistService;
            _dateRangeService = dateRangeService;
            _dateTimeHelper = dateTimeHelper;
            _downloadService = downloadService;
            _genericAttributeService = genericAttributeService;
            _httpContextAccessor = httpContextAccessor;
            _jsonLdModelFactory = jsonLdModelFactory;
            _localizationService = localizationService;
            _manufacturerService = manufacturerService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _priceCalculationService = priceCalculationService;
            _priceFormatter = priceFormatter;
            _productAttributeParser = productAttributeParser;
            _productAttributeService = productAttributeService;
            _productReviewService = productReviewService;
            _productService = productService;
            _productTagService = productTagService;
            _productTemplateService = productTemplateService;
            _reviewTypeService = reviewTypeService;
            _shoppingCartService = shoppingCartService;
            _specificationAttributeService = specificationAttributeService;
            _staticCacheManager = staticCacheManager;
            _storeContext = storeContext;
            _storeService = storeService;
            _shoppingCartModelFactory = shoppingCartModelFactory;
            _taxService = taxService;
            _urlRecordService = urlRecordService;
            _vendorService = vendorService;
            _webHelper = webHelper;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _orderSettings = orderSettings;
            _seoSettings = seoSettings;
            _shippingSettings = shippingSettings;
            _vendorSettings = vendorSettings;
            _videoService = videoService;
        }

        public override async Task<IEnumerable<ProductOverviewModel>> PrepareProductOverviewModelsAsync(IEnumerable<Product> products,
        bool preparePriceModel = true, bool preparePictureModel = true,
        int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
        bool forceRedirectionAfterAddingToCart = false)
        {
            var models = await base.PrepareProductOverviewModelsAsync(products, preparePriceModel, preparePictureModel, productThumbPictureSize, prepareSpecificationAttributes, forceRedirectionAfterAddingToCart);

            foreach (var model in models)
                await _eventPublisher.PublishAsync(new ProductOverviewModelPreparedEvent(model));

            return models;
        }
    }

    public class ProductOverviewModelPreparedEvent
    {
        public ProductOverviewModel Model { get; }

        public ProductOverviewModelPreparedEvent(ProductOverviewModel model)
        {
            Model = model;
        }
    }


}
