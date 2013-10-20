using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class ProductArticle : Entity
    {
        public virtual string Content { get; set; }

        public virtual Language Language { get; set; }

        public virtual ProductTab Tab { get; set; }
    }
}
