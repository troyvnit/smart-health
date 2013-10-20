using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    using System;
    using SmartHealth.Core.Domain.Models;

    [Serializable]
    public class ProductComment : Entity
    {
        public virtual string Content { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
    }
}
