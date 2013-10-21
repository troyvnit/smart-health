﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Web.Mvc;
using Newtonsoft.Json;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.IRepository;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Web.Controllers;

namespace SmartHealth.Web.Areas.Admin.Controllers
{
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
            return View();
        }

        public ActionResult GetCategories()
        {
            var categories = articleCategoryService.GetAll().Where(a => a.IsDeleted != true).Select(Mapper.Map<ArticleCategory, ArticleCategoryDto>).ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdateCategory(string models)
        {
            var categoryDto = JsonConvert.DeserializeObject<List<ArticleCategoryDto>>(models).FirstOrDefault();
            var category = Mapper.Map<ArticleCategoryDto, ArticleCategory>(categoryDto);
            articleCategoryService.SaveOrUpdate(category, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCategory(string models)
        {
            var categoryDto = JsonConvert.DeserializeObject<List<ArticleCategoryDto>>(models).FirstOrDefault();
            var category = Mapper.Map<ArticleCategoryDto, ArticleCategory>(categoryDto);
            articleCategoryService.Delete(category, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArticles()
        {
            var articles = articleService.GetAll().Where(a => a.IsDeleted != true).Select(article => new ArticleDto
                                                            {
                                                                Author = article.Author,
                                                                Categories = string.Join(",", article.Categories.Select(a => a.Id)),
                                                                Content = article.Content,
                                                                Description = article.Description,
                                                                Id = article.Id,
                                                                ImageUrl = article.ImageUrl,
                                                                IsActived = article.IsActived,
                                                                Priority = article.Priority,
                                                                Title = article.Title
                                                            }).ToList();
            return Json(articles, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult CreateOrUpdateArticle(ArticleDto articleDto)
        {
            var article = Mapper.Map<ArticleDto, Article>(articleDto);
            if (!string.IsNullOrEmpty(articleDto.Categories))
            {
                var categoryIds = articleDto.Categories.Split(',');
                foreach (var categoryId in categoryIds)
                {
                    article.Categories.Add(articleCategoryService.Get(Convert.ToInt32(categoryId)));
                }
            }
            articleService.SaveOrUpdate(article, true);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteArticles(string ids)
        {
            foreach (var article in ids.Split(',').Select(id => articleService.Get(Convert.ToInt32(id))))
            {
                article.IsDeleted = true;
                articleService.SaveOrUpdate(article, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
