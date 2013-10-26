using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;

namespace SmartHealth.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IService<Product> productService;
        private readonly IService<Tag> tagService;

        public HomeController(IService<Product> productService, IService<Tag> tagService)
        {
            this.productService = productService;
            this.tagService = tagService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProductNames()
        {
            var productNames = productService.GetAll().Select(a => a.Name).ToList();
            ViewBag.ProductNames = productNames;
            return View();
        }
    }
}
