using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class ProductGroup : Entity
    {
        public ProductGroup()
        {
            Products = new List<Product>();
        }
        public virtual string Name { get; set; }

        public virtual Language Language { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
