using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using AutoMapper;

namespace SmartHealth.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IService<Product> productService;
        private readonly IService<Tag> tagService;
        private readonly IService<Article> articleService;
        private readonly IService<ArticleCategory> articleCategoryService;
        private readonly IService<Menu> menuService;

        public HomeController(IService<Product> productService, IService<Tag> tagService, IService<Article> articleService, IService<ArticleCategory> articleCategoryService, IService<Menu> menuService)
        {
            this.productService = productService;
            this.tagService = tagService;
            this.articleService = articleService;
            this.articleCategoryService = articleCategoryService;
            this.menuService = menuService;
        }

        public ActionResult Index()
        {
            if(Session["SmartHealthUser"] == null)
            {
                Session["SmartHealthUser"] = new SessionDto();
            }
            var introductions =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.Get(1)) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(3).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Introductions = introductions;

            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.Get(2)) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(7).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Newses = newses;

            var mainProducts = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true && a.IsMainProduct).OrderByDescending(a => a.CreatedDate).Take(6).Select(Mapper.Map<Product, ProductDto>).ToList();
            ViewBag.MainProducts = mainProducts;
            return View();
        }

        public ActionResult GetProductNames()
        {
            var productNames = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true && a.IsMainProduct).Select(a => a.Name).ToList();
            ViewBag.ProductNames = productNames;
            return View();
        }

        public ActionResult GetTopMenu()
        {
            var menus = menuService.GetAll().OrderBy(a => a.Priority).Select(Mapper.Map<Menu, MenuDto>).ToList();
            ViewBag.Menus = menus;
            return View();
        }

        public ActionResult ProductDetail(int id)
        {
            var product = productService.Get(id);
            product.ViewCount += 1;
            productService.SaveOrUpdate(product, true);

            var tags = tagService.GetAll().Where(a => a.Products.Contains(product));

            var productDetail = Mapper.Map<Product, ProductDto>(product);
            ViewBag.ProductDetail = productDetail;

            if (Session["SmartHealthUser"] != null)
            {
                var viewedProducts = ((SessionDto)Session["SmartHealthUser"]).ViewedProducts;
                if (viewedProducts.All(a => a.Id != id))
                {
                    viewedProducts.Add(productDetail);
                }
            }

            var productImages = product.Images.Select(Mapper.Map<Media, MediaDto>).ToList();
            ViewBag.ProductImages = productImages;

            var relatedProducts = productService.GetAll().Select(Mapper.Map<Product, ProductDto>).ToList();
            ViewBag.RelatedProducts = relatedProducts;

            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.Get(2)) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(6).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Newses = newses;

            return View();
        }

        public ActionResult AddOrderProduct(int id, int quantity)
        {
            var product = productService.Get(id);
            var orderCount = 0;
            if (Session["SmartHealthUser"] != null)
            {
                var orderProducts = ((SessionDto)Session["SmartHealthUser"]).OrderProducts;
                orderProducts.Add(Mapper.Map<Product, ProductDto>(product));
                orderCount = orderProducts.Count;
            }
            return Json(orderCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PublicRelation()
        {
            var prCategories = articleCategoryService.GetAll().Where(a => a.IsDeleted != true && a.IsPublicRelation);
            var publicRelationCategories = new List<ArticleCategoryDto>();
            foreach (var prCategory in prCategories)
            {
                var publicRelationCategory = Mapper.Map<ArticleCategory, ArticleCategoryDto>(prCategory);
                foreach (var article in prCategory.Articles.Where(a => a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(9))
                {
                    publicRelationCategory.ArticleDtos.Add(Mapper.Map<Article, ArticleDto>(article));
                }
                publicRelationCategories.Add(publicRelationCategory);
            }
            ViewBag.PublicRelationCategories = publicRelationCategories;

            var saleOffProducts = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true && a.IsSaleOff).OrderByDescending(a => a.CreatedDate).Take(5).Select(Mapper.Map<Product, ProductDto>).ToList();
            ViewBag.SaleOffProducts = saleOffProducts;

            var tags = tagService.GetAll().Select(Mapper.Map<Tag, TagDto>).ToList();
            ViewBag.Tags = tags;
            return View();
        }

        public ActionResult ArticleDetail(int id)
        {
            var article = Mapper.Map<Article, ArticleDto>(articleService.Get(id));
            ViewBag.Article = article;

            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.Get(2)) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(6).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Newses = newses;

            if (Session["SmartHealthUser"] != null)
            {
                var viewedArticles = ((SessionDto)Session["SmartHealthUser"]).ViewedArticles;
                if (viewedArticles.All(a => a.Id != id))
                {
                    viewedArticles.Add(article);
                }
            }
            return View();
        }
    }
}
