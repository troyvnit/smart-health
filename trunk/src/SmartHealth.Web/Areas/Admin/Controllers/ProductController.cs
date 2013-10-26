using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    public class ProductController : BaseController
    {
        private readonly IService<Product> productService;
        private readonly IService<Tag> tagService;

        public ProductController(IService<Product> productService, IService<Tag> tagService)
        {
            this.productService = productService;
            this.tagService = tagService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTags()
        {
            var tags = tagService.GetAll().OrderByDescending(a => a.Id).Select(Mapper.Map<Tag, TagDto>).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdateTag(string models)
        {
            var tagDto = JsonConvert.DeserializeObject<List<TagDto>>(models).FirstOrDefault();
            var tag = Mapper.Map<TagDto, Tag>(tagDto);
            tagService.SaveOrUpdate(tag, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTag(string models)
        {
            var tagDto = JsonConvert.DeserializeObject<List<TagDto>>(models).FirstOrDefault();
            var tag = Mapper.Map<TagDto, Tag>(tagDto);
            tagService.Delete(tag, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProducts()
        {
            var products = productService.GetAll().OrderByDescending(a => a.CreatedDate).Where(a => a.IsDeleted != true).Select(product => new ProductDto
            {
                Brand = product.Brand,
                Tags = string.Join(",", product.Tags.Select(a => a.Id)),
                Description = product.Description,
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsActived = product.IsActived,
                LanguageId = product.Language.Id,
                IsMainProduct = product.IsMainProduct,
                MarketPrice = product.MarketPrice,
                Name = product.Name,
                Property = product.Property,
                Review = product.Review,
                SmartHealthPrice = product.SmartHealthPrice,
                ViewCount = product.ViewCount,
                Weight = product.Weight
            }).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult CreateOrUpdateProduct(ProductDto productDto)
        {
            var product = Mapper.Map<ProductDto, Product>(productDto);
            if (!string.IsNullOrEmpty(productDto.Tags))
            {
                var tagIds = productDto.Tags.Split(',');
                foreach (var tagId in tagIds)
                {
                    product.Tags.Add(tagService.Get(Convert.ToInt32(tagId)));
                }
            }
            product.Language = productService.Get<Language>(productDto.LanguageId);
            productService.SaveOrUpdate(product, true);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProducts(string ids)
        {
            foreach (var product in ids.Split(',').Select(id => productService.Get(Convert.ToInt32(id))))
            {
                product.IsDeleted = true;
                productService.SaveOrUpdate(product, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActiveProducts(string ids)
        {
            foreach (var product in ids.Split(',').Select(id => productService.Get(Convert.ToInt32(id))))
            {
                product.IsActived = !product.IsActived;
                productService.SaveOrUpdate(product, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult MakeMainProducts(string ids)
        {
            foreach (var product in ids.Split(',').Select(id => productService.Get(Convert.ToInt32(id))))
            {
                product.IsMainProduct = !product.IsMainProduct;
                productService.SaveOrUpdate(product, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
