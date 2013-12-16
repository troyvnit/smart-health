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
            Order = new OrderDto
                    {
                        OrderDetails = new List<OrderDetailDto>()
                    };
            ViewedProducts = new List<ProductDto>();
            ViewedArticles = new List<ArticleDto>();
        }
        public virtual OrderDto Order { get; set; }
        public virtual IList<ProductDto> ViewedProducts { get; set; }
        public virtual IList<ArticleDto> ViewedArticles { get; set; }
        public virtual int UserId { get; set; }
    }
}
