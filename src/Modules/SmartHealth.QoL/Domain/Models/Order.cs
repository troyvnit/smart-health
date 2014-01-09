using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class Order : Entity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            CreatedDate = DateTime.Now;
        }
        public virtual decimal TotalAmount { get; set; }

        public virtual decimal NetAmount { get; set; }

        public virtual decimal FeeAmount { get; set; }

        public virtual bool IsPayed { get; set; }

        public virtual int TransactionStatus { get; set; }

        public virtual PayType PayType { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User OrderUser { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; }
    }

    public enum PayType
    {
        [Display(Name = "Smart Health")]
        SmartHealth,
        [Display(Name = "Bao Kim")]
        BaoKim,
        [Display(Name = "Payoo")]
        Payoo
    }
}
