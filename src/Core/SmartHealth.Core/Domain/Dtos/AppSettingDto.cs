using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Core.Domain.Dtos
{
    public class AppSettingDto
    {
        public virtual int Id { get; set; }
        public virtual string KeyWord { get; set; }
        public virtual string StringValue { get; set; }
        public virtual int IntValue { get; set; }
        public virtual string Description { get; set; }
    }
}
