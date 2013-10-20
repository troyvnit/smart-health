using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.SampleModule.Domain.Models
{
    [Serializable]
    public class ProductTag : Entity
    {
        public virtual string Tag { get; set; }

        public virtual string Language { get; set; }
    }
}
