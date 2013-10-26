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
            return View();
        }

        public ActionResult GetFolders(int id = 0)
        {
            var folders = folderService.GetAll().Where(a => a.ParentId == id).Select(Mapper.Map<Folder, FolderDto>).ToList();
            foreach (var folder in folders)
            {
                folder.HasChildren = folderService.GetAll().Count(a => a.ParentId == folder.Id) > 0;
            }
            return Json(folders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImages(int? id)
        {
            var images = imageService.GetAll().Where(a => (a.Folder != null && a.Folder.Id == id) || id == null).Select(Mapper.Map<Image, ImageDto>).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteImage(int id)
        {
            imageService.Delete(imageService.Get(id), true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductImages(int id)
        {
            var images = imageService.GetAll().Where(a => a.Product != null && a.Product.Id == id).Select(Mapper.Map<Image, ImageDto>).ToList();
            foreach (var image in images)
            {
                image.ProductId = id;
            }
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductImage(int id, int productId)
        {
            var product = imageService.Get<Product>(productId);
            product.Images.Remove(imageService.Get(id));
            imageService.SaveOrUpdate(product, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

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
