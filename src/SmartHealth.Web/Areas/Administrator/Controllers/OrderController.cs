using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Core.Domain.Dtos;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Web.Controllers;

namespace SmartHealth.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : BaseController
    {
        private readonly IService<Order> orderService;

        public OrderController(IService<Order> orderService)
        {
            this.orderService = orderService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Administrator/Views/Order/Index.cshtml");
        }

        public ActionResult GetOrders()
        {
            var orders =
                orderService.GetAll().OrderByDescending(a => a.CreatedDate).Select(
                    order => new OrderDto
                                   {
                                       Id = order.Id,
                                       OrderUser = Mapper.Map<User, UserDto>(order.OrderUser),
                                       ReceiverName = order.ReceiverName,
                                       ReceiverPhone = order.ReceiverPhone,
                                       DeliveryAddress = order.DeliveryAddress,
                                       DeliveryCity = order.DeliveryCity,
                                       DeliveryCityName = ToDescription(order.DeliveryCity),
                                       CreatedDate = order.CreatedDate,
                                       TotalAmount = order.TotalAmount,
                                       NetAmount = order.NetAmount,
                                       FeeAmount = order.FeeAmount
                                   });
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        public static string ToDescription(Enum en) //ext method
        {

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {

                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(DescriptionAttribute),

                                              false);

                if (attrs != null && attrs.Length > 0)

                    return ((DescriptionAttribute)attrs[0]).Description;

            }

            return en.ToString();

        }

        public ActionResult GetOrderDetails(int id)
        {
            var order = orderService.Get(id);
            if (order == null) return Json(null, JsonRequestBehavior.AllowGet);
            var orderDetails = Mapper.Map<IList<OrderDetail>, IList<OrderDetailDto>>(order.OrderDetails.Where(a => a.Quantity > 0).ToList());
            return Json(orderDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateOrUpdateOrder(string models)
        {
            var orderDto =
                JsonConvert.DeserializeObject<List<OrderDto>>(models).FirstOrDefault();
            Order order = Mapper.Map<OrderDto, Order>(orderDto);
            orderService.SaveOrUpdate(order, true);
            return Json(Mapper.Map<Order, OrderDto>(order), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DeleteOrders(string models)
        {
            var orderDto =
                JsonConvert.DeserializeObject<List<OrderDto>>(models).FirstOrDefault();
            if (orderDto != null)
            {
                Order order = orderService.Get(orderDto.Id);
                foreach (var orderDetail in order.OrderDetails)
                {
                    orderService.Delete(orderDetail);
                }
                orderService.Delete(order, true);
                return Json(Mapper.Map<Order, OrderDto>(order), JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
