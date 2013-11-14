using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize]
    public class ArticleController : BaseController
    {
        private readonly IService<Article> articleService;
        private readonly IService<ArticleCategory> articleCategoryService;

        public ArticleController(IService<Article> articleService, IService<ArticleCategory> articleCategoryService)
        {
            this.articleService = articleService;
            this.articleCategoryService = articleCategoryService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Admin/Views/Article/Index.cshtml");
        }

        public ActionResult ManageArticle()
        {
            return View("~/Areas/Admin/Views/Article/ManageArticle.cshtml");
        }

        public ActionResult CreateOrUpdateArticle(int? id)
        {
            var categories =
                articleCategoryService.GetAll().Where(a => a.IsDeleted != true).OrderByDescending(a => a.Id).Select(
                    Mapper.Map<ArticleCategory, ArticleCategoryDto>).ToList();
            ViewBag.Categories = categories;
            return View("~/Areas/Admin/Views/Article/CreateOrUpdateArticle.cshtml");
        }

        public ActionResult GetCategories()
        {
            List<ArticleCategoryDto> categories =
                articleCategoryService.GetAll().Where(a => a.IsDeleted != true).OrderByDescending(a => a.Id).Select(
                    Mapper.Map<ArticleCategory, ArticleCategoryDto>).ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdateCategory(string models)
        {
            ArticleCategoryDto categoryDto =
                JsonConvert.DeserializeObject<List<ArticleCategoryDto>>(models).FirstOrDefault();
            if(categoryDto != null)
            {
                categoryDto.Language = articleService.GetAll<Language>().FirstOrDefault(a => a.CultureInfo == categoryDto.Language.CultureInfo);
                ArticleCategory category = Mapper.Map<ArticleCategoryDto, ArticleCategory>(categoryDto);
                articleCategoryService.SaveOrUpdate(category, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCategory(string models)
        {
            ArticleCategoryDto categoryDto =
                JsonConvert.DeserializeObject<List<ArticleCategoryDto>>(models).FirstOrDefault();
            if (categoryDto != null)
            {
                categoryDto.Language =
                    articleService.GetAll<Language>().FirstOrDefault(
                        a => a.CultureInfo == categoryDto.Language.CultureInfo);
                ArticleCategory category = Mapper.Map<ArticleCategoryDto, ArticleCategory>(categoryDto);
                category.IsDeleted = true;
                articleCategoryService.SaveOrUpdate(category, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArticles()
        {
            var url = HttpContext.Request.Url;
            var articles =
                articleService.GetAll().Where(a => a.IsDeleted != true).OrderByDescending(a => a.CreatedDate).Select(
                    article => url != null
                                   ? new ArticleDto
                                         {
                                             Author = article.Author,
                                             Categories = string.Join(",", article.Categories.Select(a => a.Id)),
                                             Content = article.Content,
                                             Description = article.Description,
                                             Id = article.Id,
                                             MediaUrl = article.MediaUrl,
                                             IsActived = article.IsActived,
                                             Priority = article.Priority,
                                             Title = article.Title,
                                             Tags = article.Tags,
                                             FullUrl = Url.Action("Article", "Home", new {article.Id}, url.Scheme)
                                         }
                                   : null).ToList();
            return Json(articles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateOrUpdateArticle(ArticleDto articleDto)
        {
            Article article = Mapper.Map<ArticleDto, Article>(articleDto);
            if (!string.IsNullOrEmpty(articleDto.Categories))
            {
                string[] categoryIds = articleDto.Categories.Split(',');
                foreach (string categoryId in categoryIds)
                {
                    ArticleCategory category = articleCategoryService.Get(Convert.ToInt32(categoryId));
                    article.Categories.Add(category);
                }
            }
            articleService.SaveOrUpdate(article, true);
            return Json(Mapper.Map<Article, ArticleDto>(article), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteArticles(string ids)
        {
            foreach (Article article in ids.Split(',').Select(id => articleService.Get(Convert.ToInt32(id))))
            {
                article.IsDeleted = true;
                articleService.SaveOrUpdate(article, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActiveArticles(string ids)
        {
            foreach (Article article in ids.Split(',').Select(id => articleService.Get(Convert.ToInt32(id))))
            {
                article.IsActived = !article.IsActived;
                articleService.SaveOrUpdate(article, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
