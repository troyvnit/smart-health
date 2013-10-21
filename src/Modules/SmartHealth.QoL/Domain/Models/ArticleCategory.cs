using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class ArticleCategory : Entity
    {
        public  ArticleCategory()
        {
            Articles = new List<Article>();
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual IList<Article> Articles { get; set; } 
    }
}
