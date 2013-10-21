using AutoMapper;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;

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
            Mapper.CreateMap<Article, ArticleDto>().ForMember(a => a.Categories, o => o.Ignore());
        }
    }
}