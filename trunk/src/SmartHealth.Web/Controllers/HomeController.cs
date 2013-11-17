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
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IService<Product> productService;
        private readonly IService<ProductGroup> productGroupService;
        private readonly IService<Article> articleService;
        private readonly IService<ArticleCategory> articleCategoryService;
        private readonly IService<Menu> menuService;
        private readonly IService<Media> mediaService;

        public HomeController(IService<Product> productService, IService<ProductGroup> productGroupService, IService<Article> articleService, IService<ArticleCategory> articleCategoryService, IService<Menu> menuService, IService<Media> mediaService)
        {
            this.productService = productService;
            this.productGroupService = productGroupService;
            this.articleService = articleService;
            this.articleCategoryService = articleCategoryService;
            this.menuService = menuService;
            this.mediaService = mediaService;
        }

        public ActionResult Index()
        {
            if(Session["SmartHealthUser"] == null)
            {
                Session["SmartHealthUser"] = new SessionDto();
            }
            
            var introductions =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.HomeShortCut.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(3).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Introductions = introductions;

            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(7).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Newses = newses;

            var typicalProducts = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true && a.Groups.Contains(productGroupService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.TypicalProduct.ToUpper()))).OrderByDescending(a => a.CreatedDate).Take(6).Select(Mapper.Map<Product, ProductDto>).ToList();
            ViewBag.TypicalProducts = typicalProducts;

            var videoLinks = mediaService.GetAll().Select(Mapper.Map<Media, MediaDto>).Where(a => a.Type == 3).ToList();
            ViewBag.VideoLinks = videoLinks;
            return View();
        }

        public ActionResult GetProductNames()
        {
            var productNames = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true).Select(a => a.Name).ToList();
            ViewBag.ProductNames = productNames;
            return View();
        }

        public ActionResult GetTopMenu()
        {
            var menus = menuService.GetAll().Where(a => a.Language.CultureInfo.ToUpper() == RouteData.Values["lang"].ToString().ToUpper()).OrderBy(a => a.Priority).Select(Mapper.Map<Menu, MenuDto>).ToList();
            ViewBag.Menus = menus;
            return View();
        }

        public ActionResult GetBottomMenu()
        {
            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(8).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Newses = newses;

            var clientSupportArticles =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.ClientSupport.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(8).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.ClientSupportArticles = clientSupportArticles;
            return View();
        }

        public ActionResult Product(int id)
        {
            var product = productService.Get(id);
            if (product != null)
            {
                product.ViewCount += 1;
                productService.SaveOrUpdate(product, true);

                var productGroups = productGroupService.GetAll().Where(a => a.Products.Contains(product));

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

                var productMedias = product.Medias.Select(Mapper.Map<Media, MediaDto>).ToList();
                ViewBag.ProductMedias = productMedias;

                var relatedProducts = productService.GetAll().Select(Mapper.Map<Product, ProductDto>).ToList();
                ViewBag.RelatedProducts = relatedProducts;

                var lstNews =
                    articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(6).Select(
                        Mapper.Map<Article, ArticleDto>).ToList();
                ViewBag.Newses = lstNews;
                return View();
            }
            else {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public ActionResult ProductList(int? id)
        {
            if(id == null)
            {
                var products = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.CreatedDate).Select(Mapper.Map<Product, ProductDto>).ToList();
                ViewBag.Products = products;
            }
            else
            {
                var group = productGroupService.GetAll().FirstOrDefault(b => b.Id == id);
                var products = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true && a.Groups.Contains(group)).OrderByDescending(a => a.CreatedDate).Select(Mapper.Map<Product, ProductDto>).ToList();
                ViewBag.Products = products;
                if (@group != null) ViewBag.GroupName = @group.Name;
            }
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

            var saleOffProducts = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true && a.Groups.Contains(productGroupService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.SaleOff.ToUpper()))).OrderByDescending(a => a.CreatedDate).Take(5).Select(Mapper.Map<Product, ProductDto>).ToList();
            ViewBag.SaleOffProducts = saleOffProducts;

            var tags = productGroupService.GetAll().Select(Mapper.Map<ProductGroup, ProductGroupDto>).ToList();
            ViewBag.Tags = tags;
            return View();
        }

        public ActionResult Article(int id)
        {
            var article = Mapper.Map<Article, ArticleDto>(articleService.Get(id));
            ViewBag.Article = article;

            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(6).Select(
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
        public ActionResult ArticleList(int id)
        {
            var category = articleCategoryService.Get(id);
            if (category != null)
            {
                var articles = articleService.FindAll(p => p.Categories.Any(c => c.Id == id)).Select(Mapper.Map<Article, ArticleDto>).ToList();
                ViewBag.Title = category.Name + " - " + category.Description;
                ViewBag.CategoryName = category.Name;
                ViewBag.Articles = articles;

                var newses =
                    articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(6).Select(
                        Mapper.Map<Article, ArticleDto>).ToList();
                ViewBag.Newses = newses;

                return View();
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
