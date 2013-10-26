using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Tag : Entity
    {
        public virtual string Name { get; set; }

        public virtual Language Language { get; set; }

        public virtual IList<Product> Products { get; set; }

        public virtual IList<Article> Articles { get; set; } 
    }
}
