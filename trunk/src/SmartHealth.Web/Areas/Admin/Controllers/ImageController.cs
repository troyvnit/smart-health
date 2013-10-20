using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHealth.Web.Areas.Admin.Models;
using SmartHealth.Web.Controllers;

namespace SmartHealth.Web.Areas.Admin.Controllers
{
    public class ImageController : BaseController
    {
        public ActionResult Index(string folder)
        {
            var result = "";
            var directories = Directory.GetDirectories(Server.MapPath("~/Media/Images/" + folder), "*.*", SearchOption.TopDirectoryOnly).Select(directory => new DirectoryInfo(directory)).ToList();
            result += ShowTree(directories);
            ViewBag.Directories = result;
            return View();
        }

        public string ShowTree(List<DirectoryInfo> infos)
        {
            var result = "<ul>";
            foreach (var info in infos)
            {
                result += "<li data-expanded=\"true\"><span class=\"k-sprite folder\"></span>" + info.Name;
                var directories = Directory.GetDirectories(info.FullName, "*.*", SearchOption.TopDirectoryOnly).Select(directory => new DirectoryInfo(directory)).Where(a => a.Parent != null && a.Parent.Name == info.Name).ToList();
                if (directories.Any())
                {
                    result += ShowTree(directories);
                }
                result += "</li>";
            }
            result += "</ul>";
            return result;
        }

        public ActionResult GetDirectories(string folder = "images")
        {
            if (!Directory.Exists(Server.MapPath("~/Media/" + folder)))
            {
                return Json(new { success = false });
            }
            string[] extensions = { ".jpg", ".gif", ".png", ".jpeg" };
            var files = Directory.GetFiles(Server.MapPath("~/Media/" + folder), "*.*", SearchOption.TopDirectoryOnly).Where(a =>
            {
                var extension = Path.GetExtension(a);
                return extension != null && extensions.Contains(extension.ToLower());
            }).ToList();
            var fileViewModels = new List<FileViewModel>();
            foreach (var file in files)
            {
                var image = Image.FromFile(file);
                var fileInfo = new FileInfo(file);
                fileViewModels.Add(new FileViewModel
                {
                    FileName = fileInfo.Name,
                    Size = fileInfo.Length.ToString(CultureInfo.InvariantCulture),
                    Height = image.Height.ToString(CultureInfo.InvariantCulture),
                    Width = image.Width.ToString(CultureInfo.InvariantCulture),
                    LastWriteDate = fileInfo.LastWriteTime,
                    Url = "/Media/" + folder + "/" + fileInfo.Name
                });
                image.Dispose();
            }
            return Json(fileViewModels.OrderByDescending(a => a.LastWriteDate), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, string folder)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var pathString = Path.Combine(Server.MapPath("~/Media/"), folder);
                    if (!Directory.Exists(pathString))
                    {
                        Directory.CreateDirectory(pathString);
                    }
                    if (fileName == null)
                    {
                        continue;
                    }
                    var physicalPath = Path.Combine(pathString, fileName);
                    file.SaveAs(physicalPath);
                }
            }

            return Json(new { Folder = Url.Content("~/Media/Images/" + folder) }, JsonRequestBehavior.AllowGet);

        }
    }
}
