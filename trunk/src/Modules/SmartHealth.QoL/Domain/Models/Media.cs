using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Media : Entity
    {
        public Media()
        {
            Product = new Product();
        }

        public virtual string Name { get; set; }

        public virtual string MediaUrl { get; set; }

        public virtual string ThumbnailUrl { get; set; }

        public virtual string Description { get; set; }

        public virtual int Type { get; set; }

        public virtual Folder Folder { get; set; }

        public virtual int Priority { get; set; }

        public virtual Product Product { get; set; }
    }
}
