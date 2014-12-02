using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UserController : BaseController
    {
        private readonly IService<User> userService;

        public UserController(IService<User> userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Administrator/Views/User/Index.cshtml");
        }

        public ActionResult GetUsers()
        {
            var users =
                userService.GetAll().OrderByDescending(a => a.CreatedTime).Select(
                    user => new UserDto
                                   {
                                       Id = user.Id,
                                       Username = user.Username,
                                       Password = user.Password,
                                       Email = user.Email,
                                       DisplayName = user.DisplayName,
                                       Location = user.Location,
                                       DOB = user.DOB,
                                       Gender = user.Gender
                                   });
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateOrUpdateUser(string models)
        {
            var userDto =
                JsonConvert.DeserializeObject<List<UserDto>>(models).FirstOrDefault();
            User user = Mapper.Map<UserDto, User>(userDto);
            user.FirstName = user.LastName = user.DisplayName;
            userService.SaveOrUpdate(user, true);
            return Json(Mapper.Map<User, UserDto>(user), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUsers(string models)
        {
            var userDto =
                JsonConvert.DeserializeObject<List<UserDto>>(models).FirstOrDefault();
            if (userDto != null)
            {
                var orders = userService.GetAll<Order>().Where(a => a.OrderUser.Id == userDto.Id);
                foreach (var order in orders)
                {
                    userService.Delete(order, true);
                }
                User user = userService.Get(userDto.Id);
                userService.Delete(user, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
