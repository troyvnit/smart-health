using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHealth.Web.Controllers
{
    public class ErrorsController : BaseController
    {
        //
        // GET: /Errors/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error400()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}
