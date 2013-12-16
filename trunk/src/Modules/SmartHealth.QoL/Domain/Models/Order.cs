using System;
using System.Collections.Generic;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Order : Entity
    {
        public Order()
        {
            CreatedDate = DateTime.Now;
        }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User OrderUser { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; }
    }
}
