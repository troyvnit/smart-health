using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Web.Areas.Admin.Models;
using SmartHealth.Web.Controllers;

namespace SmartHealth.Web.Areas.Admin.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IService<Folder> folderService;
        private readonly IService<Image> imageService;

        public ImageController(IService<Folder> folderService, IService<Image> imageService)
        {
            this.folderService = folderService;
            this.imageService = imageService;
        }

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

        public ActionResult GetFolders(int Id = 0)
        {
            var folders = folderService.GetAll().Where(a => a.ParentId == Id).Select(Mapper.Map<Folder, FolderDto>).ToList();
            foreach (var folder in folders)
            {
                folder.HasChildren = folderService.GetAll().Count(a => a.ParentId == folder.Id) > 0;
            }
            return Json(folders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImages(int Id)
        {
            var images = imageService.GetAll().Where(a => a.Folder != null && a.Folder.Id == Id).Select(Mapper.Map<Image, ImageDto>).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductImages(int Id)
        {
            var images = imageService.GetAll().Where(a => a.Product != null && a.Product.Id == Id).Select(Mapper.Map<Image, ImageDto>).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetDirectories(string folder = "images")
        //{
        //    if (!Directory.Exists(Server.MapPath("~/Media/" + folder)))
        //    {
        //        return Json(new { success = false });
        //    }
        //    string[] extensions = { ".jpg", ".gif", ".png", ".jpeg" };
        //    var files = Directory.GetFiles(Server.MapPath("~/Media/" + folder), "*.*", SearchOption.TopDirectoryOnly).Where(a =>
        //    {
        //        var extension = Path.GetExtension(a);
        //        return extension != null && extensions.Contains(extension.ToLower());
        //    }).ToList();
        //    var fileViewModels = new List<FileViewModel>();
        //    foreach (var file in files)
        //    {
        //        var image = Image.FromFile(file);
        //        var fileInfo = new FileInfo(file);
        //        fileViewModels.Add(new FileViewModel
        //        {
        //            FileName = fileInfo.Name,
        //            Size = fileInfo.Length.ToString(CultureInfo.InvariantCulture),
        //            Height = image.Height.ToString(CultureInfo.InvariantCulture),
        //            Width = image.Width.ToString(CultureInfo.InvariantCulture),
        //            LastWriteDate = fileInfo.LastWriteTime,
        //            Url = "/Media/" + folder + "/" + fileInfo.Name
        //        });
        //        image.Dispose();
        //    }
        //    return Json(fileViewModels.OrderByDescending(a => a.LastWriteDate), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int? folderId, int? productId)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(file.FileName);
                    var pathString = Server.MapPath("~/Media/Images");
                    if (!Directory.Exists(pathString))
                    {
                        Directory.CreateDirectory(pathString);
                    }
                    var physicalPath = Path.Combine(pathString, fileName);
                    file.SaveAs(physicalPath);
                    imageService.Save(new Image{ Folder = folderId != null ? folderService.Get((int)folderId) : null, Product = productId != null ? imageService.Get<Product>((int)productId) : null, ImageUrl = "/Media/Images/" + fileName });
                }
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
