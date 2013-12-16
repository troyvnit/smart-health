using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class OrderDetail : Entity
    {
        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Order Order { get; set; }
    }
}
