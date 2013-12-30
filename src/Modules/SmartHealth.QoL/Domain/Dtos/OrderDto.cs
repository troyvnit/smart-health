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

        public virtual int Id { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual UserDto OrderUser { get; set; }

        public virtual decimal TotalPrice { get; set; }
        //{
        //    get
        //    {
        //        return OrderDetails.Sum(orderDetailDto => orderDetailDto.Quantity*orderDetailDto.Product.SmartHealthPrice);
        //    }
        //}

        public virtual IList<OrderDetailDto> OrderDetails { get; set; }
    }
}
