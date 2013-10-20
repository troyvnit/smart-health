using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class ProductTab : Entity
    {
        public virtual string TabName { get; set; }

        public virtual bool IsPropertyTab { get; set; }

        public virtual int Priority { get; set; }

        public virtual ProductArticle Article { get; set; }
    }
}
