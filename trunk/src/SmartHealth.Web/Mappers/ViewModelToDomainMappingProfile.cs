using AutoMapper;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;

namespace SmartHealth.Web.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ArticleCategoryDto, ArticleCategory>();
            Mapper.CreateMap<ArticleDto, Article>().ForMember(a => a.Categories, o => o.Ignore());
        }
    }
}