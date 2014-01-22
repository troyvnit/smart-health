using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class DocumentDto
    {
        public virtual int? Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string DownloadUrl { get; set; }

        public virtual string ThumbnailUrl { get; set; }

        public virtual string Description { get; set; }

        public virtual string Content { get; set; }

        public virtual int Priority { get; set; }

        public virtual int ArticleId { get; set; }
    }
}
