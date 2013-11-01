using System;
using System.Collections.Generic;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Folder : Entity
    {
        public Folder()
        {
            Images = new List<Media>();
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int ParentId { get; set; }

        public virtual IList<Media> Images { get; set; }
    }
}
