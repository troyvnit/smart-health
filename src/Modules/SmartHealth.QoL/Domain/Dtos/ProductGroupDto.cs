using System;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Box.Domain.Dtos
{
    public class ProductGroupDto
    {
        public virtual int? Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual Language Language { get; set; }
    }
}
