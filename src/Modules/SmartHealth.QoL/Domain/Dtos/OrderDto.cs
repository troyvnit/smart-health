using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.Dtos;

namespace SmartHealth.Box.Domain.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderDetails = new List<OrderDetailDto>();
        }
        public virtual UserDto OrderUser { get; set; }

        public virtual IList<OrderDetailDto> OrderDetails { get; set; }
    }
}
