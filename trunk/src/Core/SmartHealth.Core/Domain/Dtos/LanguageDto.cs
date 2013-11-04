using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Core.Domain.Models
{
    public class LanguageDto
    {
        public virtual int Id { get; set; }

        public virtual string CultureInfo { get; set; }

        public virtual string Name { get; set; }
    }
}
