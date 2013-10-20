using System;

namespace SmartHealth.Box.Domain.Dtos
{
    public class ArticleCategoryDto
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}
