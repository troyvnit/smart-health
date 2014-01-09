using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class OrderDetailDto
    {
        public virtual int Id { get; set; }
        public virtual ProductDto Product { get; set; }
        public virtual int Quantity { get; set; }
        public virtual OrderDto Order { get; set; }
    }
}
