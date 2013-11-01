using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class SessionDto
    {
        public SessionDto()
        {
            OrderProducts = new List<ProductDto>();
            ViewedProducts = new List<ProductDto>();
            ViewedArticles = new List<ArticleDto>();
        }
        public virtual IList<ProductDto> OrderProducts { get; set; }
        public virtual IList<ProductDto> ViewedProducts { get; set; }
        public virtual IList<ArticleDto> ViewedArticles { get; set; }
    }
}
