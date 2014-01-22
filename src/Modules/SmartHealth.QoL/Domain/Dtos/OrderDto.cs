using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Core.Domain.Dtos;

namespace SmartHealth.Box.Domain.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderDetails = new List<OrderDetailDto>();
            CreatedDate = DateTime.Now;
        }

        public virtual int Id { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual int UserId { get; set; }

        public virtual UserDto OrderUser { get; set; }

        public virtual decimal TotalAmount { get; set; }

        public virtual decimal NetAmount { get; set; }

        public virtual decimal FeeAmount { get; set; }

        public virtual bool IsPayed { get; set; }

        public int TransactionStatus { get; set; }

        public virtual City DeliveryCity { get; set; }

        public virtual string DeliveryAddress { get; set; }

        public virtual string ReceiverName { get; set; }

        public virtual string ReceiverPhone { get; set; }

        public virtual string CompanyName { get; set; }

        public virtual string CompanyAddress { get; set; }

        public virtual string TaxCode { get; set; }

        public virtual PayType PayType { get; set; }
        //{
        //    get
        //    {
        //        return OrderDetails.Sum(orderDetailDto => orderDetailDto.Quantity*orderDetailDto.Product.SmartHealthPrice);
        //    }
        //}

        public virtual IList<OrderDetailDto> OrderDetails { get; set; }
    }
}
