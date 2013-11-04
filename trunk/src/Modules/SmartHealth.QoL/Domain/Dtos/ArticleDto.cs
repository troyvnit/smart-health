using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class ArticleDto
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string Content { get; set; }

        public virtual string Author { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string Categories { get; set; }

        public virtual int Priority { get; set; }

        public virtual bool IsActived { get; set; }

        public virtual string Tags { get; set; }

        public virtual string FullUrl { get; set; }
    }
}
