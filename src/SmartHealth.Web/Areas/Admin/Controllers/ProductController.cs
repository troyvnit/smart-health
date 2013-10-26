using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
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
            var tags = tagService.GetAll().Select(Mapper.Map<Tag, TagDto>).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProducts()
        {
            var products = productService.GetAll().Where(a => a.IsDeleted != true).Select(product => new ProductDto
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
                var categoryIds = productDto.Tags.Split(',');
                foreach (var categoryId in categoryIds)
                {
                    product.Tags.Add(tagService.Get(Convert.ToInt32(categoryId)));
                }
            }
            product.Language = productService.Get<Language>(productDto.LanguageId);
            productService.SaveOrUpdate(product, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
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
    }
}
