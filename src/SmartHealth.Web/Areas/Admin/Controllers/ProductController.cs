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
        private readonly IService<ProductGroup> productGroupService;

        public ProductController(IService<Product> productService, IService<ProductGroup> productGroupService)
        {
            this.productService = productService;
            this.productGroupService = productGroupService;
        }
        public ActionResult Index()
        {
            return View("~/Areas/Admin/Views/Product/Index.cshtml");
        }

        public ActionResult GetGroups()
        {
            var groups = productGroupService.GetAll().OrderByDescending(a => a.Id).Select(Mapper.Map<ProductGroup, ProductGroupDto>).ToList();
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdateGroup(string models)
        {
            var groupDto = JsonConvert.DeserializeObject<List<ProductGroupDto>>(models).FirstOrDefault();
            if(groupDto != null)
            {
                groupDto.Language = productService.GetAll<Language>().FirstOrDefault(a => a.CultureInfo == groupDto.Language.CultureInfo);
                var group = Mapper.Map<ProductGroupDto, ProductGroup>(groupDto);
                productGroupService.SaveOrUpdate(group, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteGroup(string models)
        {
            var groupDto = JsonConvert.DeserializeObject<List<ProductGroupDto>>(models).FirstOrDefault();
            var group = Mapper.Map<ProductGroupDto, ProductGroup>(groupDto);
            productGroupService.Delete(group, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProducts()
        {
            var products = productService.GetAll().OrderByDescending(a => a.CreatedDate).Where(a => a.IsDeleted != true).Select(product => new ProductDto
            {
                Brand = product.Brand,
                Tags = product.Tags,
                Groups = string.Join(",", product.Groups.Select(a => a.Id)),
                Introduction = product.Introduction,
                Description = product.Description,
                Id = product.Id,
                MediaUrl = product.MediaUrl,
                IsActived = product.IsActived,
                MarketPrice = product.MarketPrice,
                Name = product.Name,
                Property = product.Property,
                Review = product.Review,
                SmartHealthPrice = product.SmartHealthPrice,
                ViewCount = product.ViewCount,
                Weight = product.Weight,
                Quantity = product.Quantity,
                Status = product.Status
            }).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult CreateOrUpdateProduct(ProductDto productDto)
        {
            var product = Mapper.Map<ProductDto, Product>(productDto);
            if (!string.IsNullOrEmpty(productDto.Groups))
            {
                string[] groupIds = productDto.Groups.Split(',');
                foreach (string groupId in groupIds)
                {
                    var group = productGroupService.Get(Convert.ToInt32(groupId));
                    product.Groups.Add(group);
                }
            }
            productService.SaveOrUpdate(product, true);
            return Json(Mapper.Map<Product, ProductDto>(product), JsonRequestBehavior.AllowGet);
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
    }
}
