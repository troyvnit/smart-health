using System;

namespace SmartHealth.Box.Domain.Dtos
{
    public class ImageDto
    {
        public virtual int Id { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string ThumbnailUrl { get; set; }

        public virtual string Description { get; set; }

        public virtual int FolderId { get; set; }
    }
}
