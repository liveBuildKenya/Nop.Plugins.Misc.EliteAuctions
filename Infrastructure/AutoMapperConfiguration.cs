using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;

namespace Nop.Plugin.Misc.EliteAuctions.Infrastructure
{
    /// <summary>
    /// Represents mapping configuration for plugin models
    /// </summary>
    public class AutoMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AuctionModel, Auction>()
                .ForMember(model => model.DateTimeCreatedUtc, options => options.Ignore())
                .ForMember(model => model.DateTimeUpdatedUtc, options => options.Ignore())
                .ForMember(model => model.WinningCustomerId, options => options.Ignore())
                .ForMember(model => model.StoreId, options => options.Ignore())
                .ForMember(model => model.CurrentBidPrice, options => options.Ignore())
                .ForMember(model => model.ProductId, options => options.Ignore());
        }

        public int Order => 0;
    }
}
