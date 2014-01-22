using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Web.Controllers;

namespace SmartHealth.Web.Areas.Administrator.Controllers
{
    public class MediaController : BaseController
    {
        private readonly IService<Folder> folderService;
        private readonly IService<Media> mediaService;

        public MediaController(IService<Folder> folderService, IService<Media> mediaService)
        {
            this.folderService = folderService;
            this.mediaService = mediaService;
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
            var images = mediaService.GetAll().Where(a => (a.Folder != null && a.Folder.Id == id) || id == null).Select(Mapper.Map<Media, MediaDto>).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteImage(int id)
        {
            mediaService.Delete(mediaService.Get(id), true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductImages(int id)
        {
            var images = mediaService.GetAll().Where(a => a.Product != null && a.Product.Id == id && a.Type == 1).Select(Mapper.Map<Media, MediaDto>).ToList();
            foreach (var image in images)
            {
                image.ProductId = id;
            }
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductVideos(int id)
        {
            var videos = mediaService.GetAll().Where(a => a.Product != null && a.Product.Id == id && a.Type == 2).Select(Mapper.Map<Media, MediaDto>).ToList();
            foreach (var video in videos)
            {
                video.ProductId = id;
            }
            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHomePageVideos()
        {
            var videos = mediaService.GetAll().Where(a => a.Type == 3).Select(Mapper.Map<Media, MediaDto>).ToList();
            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBannerImages()
        {
            var videos = mediaService.GetAll().Where(a => a.Type == 41 || a.Type == 42).Select(Mapper.Map<Media, MediaDto>).ToList();
            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPartnerIcons()
        {
            var videos = mediaService.GetAll().Where(a => a.Type == 5).Select(Mapper.Map<Media, MediaDto>).ToList();
            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSliderImages()
        {
            var videos = mediaService.GetAll().Where(a => a.Type == 6).Select(Mapper.Map<Media, MediaDto>).ToList();
            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDocuments()
        {
            var documents = mediaService.GetAll<Document>().Select(Mapper.Map<Document, DocumentDto>).ToList();
            return Json(documents, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdateDocument(string models)
        {
            var documentDto = JsonConvert.DeserializeObject<List<DocumentDto>>(models).FirstOrDefault();
            var document = Mapper.Map<DocumentDto, Document>(documentDto);
            if (documentDto != null) document.Article = mediaService.Get<Article>(documentDto.ArticleId);
            mediaService.SaveOrUpdate(document, true);
            return Json(Mapper.Map<Document, DocumentDto>(document), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDocument(string models)
        {
            var documentDto = JsonConvert.DeserializeObject<List<DocumentDto>>(models).FirstOrDefault();
            if (documentDto != null)
            {
                if (documentDto.Id != null)
                {
                    var document = mediaService.Get<Document>((int)documentDto.Id);
                    mediaService.Delete(document, true);
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdateVideo(string models)
        {
            var mediaDto = JsonConvert.DeserializeObject<List<MediaDto>>(models).FirstOrDefault();
            var media = Mapper.Map<MediaDto, Media>(mediaDto);
            if (mediaDto != null) media.Product = mediaService.Get<Product>(mediaDto.ProductId);
            mediaService.SaveOrUpdate(media, true);
            return Json(Mapper.Map<Media, MediaDto>(media), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteVideo(string models)
        {
            var mediaDto = JsonConvert.DeserializeObject<List<MediaDto>>(models).FirstOrDefault();
            if (mediaDto != null)
            {
                if (mediaDto.Id != null)
                {
                    var media = mediaService.Get((int)mediaDto.Id);
                    mediaService.Delete(media, true);
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductImage(int id, int productId)
        {
            var product = mediaService.Get<Product>(productId);
            product.Medias.Remove(mediaService.Get(id));
            mediaService.SaveOrUpdate(product, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int? folderId, int? productId)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(file.FileName);
                    var pathString = Server.MapPath("~/Media");
                    if (!Directory.Exists(pathString))
                    {
                        Directory.CreateDirectory(pathString);
                    }
                    var physicalPath = Path.Combine(pathString, fileName);
                    file.SaveAs(physicalPath);
                    mediaService.Save(new Media{ Folder = folderId != null ? folderService.Get((int)folderId) : null, Product = productId != null ? mediaService.Get<Product>((int)productId) : null, MediaUrl = "/Media/" + fileName, Type = 1});
                }
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
