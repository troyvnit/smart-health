using System;
using System.Collections.Generic;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Product : Entity
    {
        public Product()
        {
            CreatedDate = DateTime.UtcNow;
            ProductProperties = new List<ProductProperty>();
            Tabs = new List<ProductTab>();
        }

        public virtual string ProductName { get; set; }

        public virtual ProductArticle Description { get; set; }

        public virtual decimal MarketPrice { get; set; }

        public virtual decimal SmartHealthPrice { get; set; }

        public virtual int ViewCount { get; set; }

        public virtual string AvatarUrl { get; set; }

        public virtual bool IsActived { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual bool IsMainProduct { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User CreatedUser { get; set; }

        public virtual IList<ProductProperty> ProductProperties { get; set; }

        public virtual IList<ProductTab> Tabs { get; set; }
    }
}
