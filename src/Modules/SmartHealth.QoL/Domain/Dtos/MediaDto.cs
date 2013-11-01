using System;

namespace SmartHealth.Box.Domain.Dtos
{
    public class MediaDto
    {
        public virtual int? Id { get; set; }

        public virtual string MediaUrl { get; set; }

        public virtual string ThumbnailUrl { get; set; }

        public virtual string Description { get; set; }

        public virtual int FolderId { get; set; }

        public virtual int ProductId { get; set; }

        public virtual int Type { get; set; }
    }
}
