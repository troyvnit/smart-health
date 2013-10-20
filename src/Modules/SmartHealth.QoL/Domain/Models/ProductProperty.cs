using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class ProductProperty : Entity
    {
        public virtual string Key { get; set; }

        public virtual string Value { get; set; }

        public virtual bool IsMain { get; set; }

        public virtual bool IsDefault { get; set; }

        public virtual int Priority { get; set; }

        public virtual Product Product { get; set; }

        public virtual Language Language { get; set; } 
    }
}
