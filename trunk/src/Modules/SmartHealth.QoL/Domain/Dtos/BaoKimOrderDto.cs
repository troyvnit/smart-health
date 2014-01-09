using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class BaoKimOrderDto
    {
        public virtual string order_id { get; set; }
        public virtual string transaction_id { get; set; }
        public virtual int created_on { get; set; }
        public virtual int payment_type { get; set; }
        public virtual int transaction_status { get; set; }
        public virtual decimal total_amount { get; set; }
        public virtual decimal net_amount { get; set; }
        public virtual decimal fee_amount { get; set; }
        public virtual int merchant_id { get; set; }
        public virtual string customer_name { get; set; }
        public virtual string customer_email { get; set; }
        public virtual string customer_phone { get; set; }
        public virtual string customer_address { get; set; }
    }
}
