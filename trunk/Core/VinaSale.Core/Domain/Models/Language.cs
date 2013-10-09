using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinaSale.Infrastructure.Domain.Models;

namespace VinaSale.Core.Domain.Models
{
    [Serializable]
    public class Language : Entity
    {
        public virtual string CultureInfo { get; set; }
        public virtual string Name { get; set; }
    }
}
