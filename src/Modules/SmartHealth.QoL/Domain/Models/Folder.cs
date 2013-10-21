using System;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Folder : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual Folder Parent { get; set; }
    }
}
