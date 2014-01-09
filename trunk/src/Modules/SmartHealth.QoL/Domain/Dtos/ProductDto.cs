using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class ProductDto
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Introduction { get; set; }

        public virtual string Property { get; set; }

        public virtual string Review { get; set; }

        public virtual int Rating { get; set; }

        public virtual int RatingCount { get; set; }

        public virtual decimal MarketPrice { get; set; }

        public virtual decimal SmartHealthPrice { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Weight { get; set; }

        public virtual int ViewCount { get; set; }

        public virtual int Quantity { get; set; }

        public virtual string Status { get; set; }

        public virtual string MediaUrl { get; set; }

        public virtual bool IsActived { get; set; }

        public virtual string Groups { get; set; }

        public virtual string Tags { get; set; }
    }
}
