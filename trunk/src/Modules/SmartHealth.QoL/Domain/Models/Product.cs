using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Medias = new List<Media>();
            Groups = new List<ProductGroup>();
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Introduction { get; set; }

        public virtual string Property { get; set; }

        public virtual string Review { get; set; }

        public virtual decimal MarketPrice { get; set; }

        public virtual decimal SmartHealthPrice { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Weight { get; set; }

        public virtual int ViewCount { get; set; }

        public virtual int Quantity { get; set; }

        public virtual string Status { get; set; }

        public virtual string MediaUrl { get; set; }

        public virtual bool IsActived { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User CreatedUser { get; set; }

        public virtual string Tags { get; set; }

        public virtual IList<Media> Medias { get; set; }

        public virtual IList<ProductGroup> Groups { get; set; } 

        public virtual IList<Article> Articles { get; set; } 
    }
}
