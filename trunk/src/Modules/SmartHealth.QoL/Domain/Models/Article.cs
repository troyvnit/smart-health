using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Article : Entity
    {
        public Article()
        {
            CreatedDate = DateTime.UtcNow;
            Categories = new List<ArticleCategory>();
        }

        public virtual string Title { get; set; }

        [StringLength(4001)]
        public virtual string Description { get; set; }

        [StringLength(4001)]
        public virtual string Content { get; set; }

        public virtual string Author { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual int Priority { get; set; }

        public virtual bool IsActived { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual string Tags { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User CreatedUser { get; set; }

        public virtual IList<ArticleCategory> Categories { get; set; }
    }
}
