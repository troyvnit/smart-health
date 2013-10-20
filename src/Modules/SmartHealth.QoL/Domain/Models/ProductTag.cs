using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class ProductTag : Entity
    {
        public virtual string Tag { get; set; }

        public virtual Language Language { get; set; }

        public virtual Product Product { get; set; }
    }
}
