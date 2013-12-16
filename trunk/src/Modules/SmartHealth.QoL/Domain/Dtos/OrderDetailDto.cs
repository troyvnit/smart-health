using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class OrderDetailDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
