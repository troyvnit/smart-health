using System;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Core.Domain.Models
{
    [Serializable]
    public class AppSetting : Entity
    {
        public virtual string KeyWord { get; set; }
        public virtual string StringValue { get; set; }
        public virtual int IntValue { get; set; }
        public virtual string Description { get; set; }
    }
}
