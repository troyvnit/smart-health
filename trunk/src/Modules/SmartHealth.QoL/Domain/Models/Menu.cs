using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Menu : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Url { get; set; }

        public virtual int Priority { get; set; }

        public virtual string Icon { get; set; }

        public virtual int ParentId { get; set; }
    }
}
