using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Image : Entity
    {
        public Image()
        {
            Product = new Product();
        }

        public virtual string ImageUrl { get; set; }

        public virtual string ThumbnailUrl { get; set; }

        public virtual string Description { get; set; }

        public virtual Folder Folder { get; set; }

        public virtual Product Product { get; set; }
    }
}
