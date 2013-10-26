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
            Tags = new List<Tag>();
            Images = new List<Image>();
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Property { get; set; }

        public virtual string Review { get; set; }

        public virtual decimal MarketPrice { get; set; }

        public virtual decimal SmartHealthPrice { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Weight { get; set; }

        public virtual int ViewCount { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual bool IsActived { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual bool IsMainProduct { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User CreatedUser { get; set; }

        public virtual Language Language { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public virtual IList<Image> Images { get; set; } 
    }
}
