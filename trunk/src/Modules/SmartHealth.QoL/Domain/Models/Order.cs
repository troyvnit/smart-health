using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public virtual City DeliveryCity { get; set; }

        public virtual string DeliveryAddress { get; set; }

        public virtual string ReceiverName { get; set; }

        public virtual string ReceiverPhone { get; set; }

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

    public enum City
    {
        [Description("An Giang")]
        AG,
        [Description("Bà Rịa - Vũng Tàu")]
        BRVT,
        [Description("Bắc Giang")]
        BG,
        [Description("Bắc Kạn")]
        BK,
        [Description("Bạc Liêu")]
        BL,
        [Description("Bắc Ninh")]
        BN,
        [Description("Bến Tre")]
        BT,
        [Description("Bình Định")]
        BDH,
        [Description("Bình Dương")]
        BDG,
        [Description("Bình Phước")]
        BP,
        [Description("Bình Thuận")]
        BTH,
        [Description("Cà Mau")]
        CM,
        [Description("Cao Bằng")]
        CB,
        [Description("Đắk Lắk")]
        DKL,
        [Description("Đắk Nông")]
        DKN,
        [Description("Điện Biên")]
        DB,
        [Description("Đồng Nai")]
        DN,
        [Description("Đồng Tháp")]
        DT,
        [Description("Gia Lai")]
        GL,
        [Description("Hà Giang")]
        HG,
        [Description("Hà Nam")]
        HNM,
        [Description("Hà Tĩnh")]
        HT,
        [Description("Hải Dương")]
        HD,
        [Description("Hậu Giang")]
        HAUG,
        [Description("Hòa Bình")]
        HB,
        [Description("Hưng Yên")]
        HY,
        [Description("Khánh Hòa")]
        KH,
        [Description("Kiên Giang")]
        KG,
        [Description("Kon Tum")]
        KT,
        [Description("Lai Châu")]
        LCH,
        [Description("Lâm Đồng")]
        LD,
        [Description("Lạng Sơn")]
        LS,
        [Description("Lào Cai")]
        LC,
        [Description("Long An")]
        LA,
        [Description("Nam Định")]
        NĐ,
        [Description("Nghệ An")]
        NA,
        [Description("Ninh Bình")]
        NB,
        [Description("Ninh Thuận")]
        NTH,
        [Description("Phú Thọ")]
        PT,
        [Description("Quảng Bình")]
        QB,
        [Description("Quảng Nam")]
        QNM,
        [Description("Quảng Ngãi")]
        QNG,
        [Description("Quảng Ninh")]
        QN,
        [Description("Quảng Trị")]
        QT,
        [Description("Sóc Trăng")]
        ST,
        [Description("Sơn La")]
        SL,
        [Description("Tây Ninh")]
        TN,
        [Description("Thái Bình")]
        TB,
        [Description("Thái Nguyên")]
        TNG,
        [Description("Thanh Hóa")]
        TH,
        [Description("Thừa Thiên Huế")]
        TTH,
        [Description("Tiền Giang")]
        TG,
        [Description("Trà Vinh")]
        TV,
        [Description("Tuyên Quang")]
        TQ,
        [Description("Vĩnh Long")]
        VL,
        [Description("Vĩnh Phúc")]
        VP,
        [Description("Yên Bái")]
        YB,
        [Description("Phú Yên")]
        PY,
        [Description("Cần Thơ")]
        CT,
        [Description("Đà Nẵng")]
        DNG,
        [Description("Hải Phòng")]
        HP,
        [Description("Hà Nội")]
        HN,
        [Description("TP HCM")]
        HCM
    }
}
