using System;
using System.Collections.Generic;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Box.Domain.Dtos
{
    public class ArticleCategoryDto
    {
        public ArticleCategoryDto()
        {
            ArticleDtos = new List<ArticleDto>();
        }

        public virtual int? Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsPublicRelation { get; set; }

        public virtual Language Language { get; set; }

        public virtual IList<ArticleDto> ArticleDtos { get; set; }
    }
}
