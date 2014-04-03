using AutoMapper;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Core.Domain.Dtos;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Web.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ArticleCategory, ArticleCategoryDto>();
            Mapper.CreateMap<Article, ArticleDto>().ForMember(a => a.Categories, o => o.Ignore()).ForMember(a => a.Products, o => o.Ignore());
            Mapper.CreateMap<ProductGroup, ProductGroupDto>();
            Mapper.CreateMap<Product, ProductDto>().ForMember(a => a.Groups, o => o.Ignore()).ForMember(a => a.Products, o => o.Ignore());
            Mapper.CreateMap<Folder, FolderDto>();
            Mapper.CreateMap<Media, MediaDto>();
            Mapper.CreateMap<Menu, MenuDto>();
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<Order, OrderDto>();
            Mapper.CreateMap<OrderDetail, OrderDetailDto>().ForMember(a => a.OrderId, o => o.MapFrom(a => a.Order.Id));
            Mapper.CreateMap<Document, DocumentDto>().ForMember(a => a.ArticleId, o => o.MapFrom(a => a.Article.Id));
        }
    }
}