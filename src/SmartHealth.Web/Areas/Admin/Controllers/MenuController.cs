using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Web.Controllers;

namespace SmartHealth.Web.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IService<Menu> menuService;

        public MenuController(IService<Menu> menuService)
        {
            this.menuService = menuService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Admin/Views/Menu/Index.cshtml");
        }

        public ActionResult GetMenus()
        {
            List<MenuDto> menus =
                menuService.GetAll().OrderByDescending(a => a.Id).Select(
                    Mapper.Map<Menu, MenuDto>).ToList();
            return Json(menus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdateMenu(string models)
        {
            MenuDto menuDto =
                JsonConvert.DeserializeObject<List<MenuDto>>(models).FirstOrDefault();
            if(menuDto != null)
            {
                menuDto.Language = menuService.GetAll<Language>().FirstOrDefault(a => a.CultureInfo == menuDto.Language.CultureInfo);
                Menu menu = Mapper.Map<MenuDto, Menu>(menuDto);
                menuService.SaveOrUpdate(menu, true);
                menuDto.Id = menu.Id;
            }
            return Json(menuDto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMenu(string models)
        {
            MenuDto menuDto =
                JsonConvert.DeserializeObject<List<MenuDto>>(models).FirstOrDefault();
            Menu menu = Mapper.Map<MenuDto, Menu>(menuDto);
            menuService.Delete(menu, true);
            if (menuDto != null) menuDto.Id = menu.Id;
            return Json(menuDto, JsonRequestBehavior.AllowGet);
        }
    }
}
