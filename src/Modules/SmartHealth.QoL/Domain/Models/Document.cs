using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Document : Entity
    {
        public Document()
        {
            Article = new Article();
        }

        public virtual string Name { get; set; }

        public virtual string DownloadUrl { get; set; }

        public virtual string ThumbnailUrl { get; set; }

        public virtual string Description { get; set; }

        public virtual int Priority { get; set; }

        public virtual Article Article { get; set; }
    }
}
