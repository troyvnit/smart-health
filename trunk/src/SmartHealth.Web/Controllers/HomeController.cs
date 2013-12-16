using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Core.Domain.Dtos;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using AutoMapper;
using SmartHealth.Web.Helpers;

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
        private readonly IService<User> userService;

        public HomeController(IService<Product> productService, IService<ProductGroup> productGroupService, IService<Article> articleService, IService<ArticleCategory> articleCategoryService, IService<Menu> menuService, IService<Media> mediaService,IService<User> userService)
        {
            this.productService = productService;
            this.productGroupService = productGroupService;
            this.articleService = articleService;
            this.articleCategoryService = articleCategoryService;
            this.menuService = menuService;
            this.mediaService = mediaService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            if(Session["SmartHealthUser"] == null)
            {
                Session["SmartHealthUser"] = new SessionDto();
            }
            
            var introductions =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.HomeShortCut.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Select(a => new ArticleDto{ Id = a.Id, Description = a.Description, Title = a.Title, MediaUrl = a.MediaUrl}).Take(3).ToList();
            ViewBag.Introductions = introductions;

            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Select(a => new ArticleDto { Id = a.Id, Description = a.Description, Title = a.Title, MediaUrl = a.MediaUrl }).Take(7).ToList();
            ViewBag.Newses = newses;

            var typicalProducts = productService.GetAll().Where(a => a.IsActived && a.IsDeleted != true && a.Groups.Contains(productGroupService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.TypicalProduct.ToUpper()))).OrderByDescending(a => a.CreatedDate).Select(a => new ProductDto { Id = a.Id, MediaUrl = a.MediaUrl, Name = a.Name }).Take(6).ToList();
            ViewBag.TypicalProducts = typicalProducts;

            var videoLinks = mediaService.GetAll().Select(Mapper.Map<Media, MediaDto>).Where(a => a.Type == 3).ToList();
            ViewBag.VideoLinks = videoLinks;

            var partners = mediaService.GetAll().Select(Mapper.Map<Media, MediaDto>).Where(a => a.Type == 5).ToList();
            ViewBag.Partners = partners;

            var imageSliders = mediaService.GetAll().Select(Mapper.Map<Media, MediaDto>).Where(a => a.Type == 6).ToList();
            ViewBag.ImageSliders = imageSliders;
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
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.FooterNews.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(8).Select(
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
                var order = ((SessionDto)Session["SmartHealthUser"]).Order;
                order.OrderDetails.Add(new OrderDetailDto{ Product = Mapper.Map<Product, ProductDto>(product), Quantity = quantity });
                orderCount = order.OrderDetails.Count(a => a.Quantity != 0);
            }
            return Json(orderCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateOrderDetail(int i, int quantity)
        {
            var orderDetails = ((SessionDto) Session["SmartHealthUser"]).Order.OrderDetails.ToArray();
            orderDetails[i].Quantity = quantity;
            return Json(orderDetails.Count(a => a.Quantity != 0), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveOrderDetail(int i)
        {
            var orderDetails = ((SessionDto)Session["SmartHealthUser"]).Order.OrderDetails.ToArray();
            orderDetails = orderDetails.Where(a => a != orderDetails[i]).ToArray();
            return Json(orderDetails.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PublicRelation()
        {
            var prCategories = articleCategoryService.GetAll().Where(a => a.IsDeleted != true && a.IsPublicRelation && a.Language.CultureInfo.ToUpper() == RouteData.Values["lang"].ToString().ToUpper());
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
            var article = articleService.Get(id);
            ViewBag.Article = Mapper.Map<Article, ArticleDto>(article);
            ViewBag.Products = article.Products.Select(Mapper.Map<Product, ProductDto>).ToList();


            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(6).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Newses = newses;

            if (Session["SmartHealthUser"] != null)
            {
                var viewedArticles = ((SessionDto)Session["SmartHealthUser"]).ViewedArticles;
                if (viewedArticles.All(a => a.Id != id))
                {
                    viewedArticles.Add(Mapper.Map<Article, ArticleDto>(article));
                }
            }
            return View();
        }
        public ActionResult ArticleList(int? id, string search)
        {
            var newses =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.News.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(6).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.Newses = newses;

            if (id != null)
            {
                var category = articleCategoryService.Get((int)id);
                if (category != null)
                {
                    var articles = articleService.FindAll(p => p.Categories.Any(c => c.Id == id)).Select(Mapper.Map<Article, ArticleDto>).ToList();
                    ViewBag.Title = category.Name + " - " + category.Description;
                    ViewBag.CategoryName = category.Name;
                    ViewBag.Articles = articles;

                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var articles = articleService.FindAll(p => p.Title.Contains(search) || p.Description.Contains(search) || p.Content.Contains(search) || string.IsNullOrEmpty(search)).Select(Mapper.Map<Article, ArticleDto>).ToList();
                ViewBag.Title = search;
                ViewBag.CategoryName = search;
                ViewBag.Articles = articles;
                return View();
            }
        }

        public ActionResult AdvertisementBanner()
        {
            var banners = mediaService.GetAll().Select(Mapper.Map<Media, MediaDto>).Where(a => a.Type == 41 || a.Type == 42).ToList();
            ViewBag.Banners = banners;
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendContactMessage(string email, string phone, string name, string message)
        {
            var eMail = new EMail
                            {
                                Phone = phone,
                                Name = name,
                                Message = message,
                                Email = email,
                                FromAddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromAddress"),
                                ToAddress = System.Configuration.ConfigurationManager.AppSettings.Get("ToAddress"),
                                SMTPClient = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient"),
                                UserName = System.Configuration.ConfigurationManager.AppSettings.Get("UserName"),
                                Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password"),
                                ReplyTo = System.Configuration.ConfigurationManager.AppSettings.Get("ReplyTo"),
                                SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort"),
                                isEnableSSL =
                                    System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() ==
                                    "YES"
                            };
            eMail.SendMail("Email", new String[] { "Smart Health Contact", eMail.Name, eMail.Phone, eMail.Email, eMail.Message });
            return Content("Success");
        }

        public ActionResult Login()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return Redirect("/" + RouteData.Values["lang"]);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserDto userDto)
        {
            var user = userService.GetAll().FirstOrDefault(a => a.Username == userDto.Username && a.Password == userDto.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
                FormsAuthentication.RedirectFromLoginPage(user.Id.ToString(), true);
                return Redirect("/" + RouteData.Values["lang"]);
            }
            ModelState.AddModelError("error", "The user name or password provided is incorrect.");
            return View("Login", userDto);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserDto user)
        {
            if (!userService.GetAll().Any(a => a.Username == user.Username || a.Email == user.Email))
            {
                var newUser = new User
                              {
                                  Username = user.Username,
                                  Password = user.Password,
                                  Email = user.Email,
                                  DisplayName = user.DisplayName,
                                  Location = user.Location,
                                  DOB = user.DOB,
                                  Gender = user.Gender,
                                  UserType = UserType.Member,
                                  LastName = user.DisplayName,
                                  FirstName = user.DisplayName,
                                  LastLoginTime = DateTime.Now,
                                  ModifiedTime = DateTime.Now
                              };
                userService.SaveOrUpdate(newUser, true);
                FormsAuthentication.SetAuthCookie(newUser.Id.ToString(), true);
                FormsAuthentication.RedirectFromLoginPage(newUser.Id.ToString(), user.RememberMe);
                return Redirect("/" + RouteData.Values["lang"]);
            }
            ModelState.AddModelError("error", "The user name or email provided is existed.");
            return View("Register", user);
        }

        public ActionResult EditProfile(int id)
        {
            var user = userService.Get(id);
            if (user != null)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Location = user.Location,
                    DOB = user.DOB,
                    Gender = user.Gender
                };
                return View(userDto);
            }
            return Redirect("/" + RouteData.Values["lang"]);
        }

        [HttpPost]
        public ActionResult EditProfile(UserDto userDto)
        {
            var user = userService.Get(userDto.Id);
            if (user != null)
            {
                user.Email = userDto.Email;
                user.DisplayName = userDto.DisplayName;
                user.Location = userDto.Location;
                user.DOB = userDto.DOB;
                user.Gender = userDto.Gender;
                user.LastName = userDto.DisplayName;
                user.FirstName = userDto.DisplayName;
                user.ModifiedTime = DateTime.Now;
                userService.SaveOrUpdate(user, true);
                return View("EditProfile", userDto);
            }
            ModelState.AddModelError("error", "The account provided is not existed.");
            return RedirectToAction("Register");
        }

        public ActionResult ChangePassword(int id)
        {
            var user = userService.Get(id);
            if (user != null)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username
                };
                return View(userDto);
            }
            return Redirect("/" + RouteData.Values["lang"]);
        }

        [HttpPost]
        public ActionResult ChangePassword(UserDto userDto)
        {
            var user = userService.Get(userDto.Id);
            if (user != null)
            {
                if (user.Password == userDto.OldPassword)
                {
                    user.Password = userDto.Password;
                    userService.SaveOrUpdate(user, true);
                    return RedirectToAction("EditProfile", new {id=user.Id});
                }
                else
                {
                    ModelState.AddModelError("error", "The password is incorrect.");
                    return View("ChangePassword", userDto);
                }
            }
            ModelState.AddModelError("error", "The account provided is not existed.");
            return RedirectToAction("Register");
        }

        public ActionResult Logout()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            return Redirect("/" + RouteData.Values["lang"]);
        }

        public ActionResult Order()
        {
            var user = userService.Get(currentUser.Id);
            var userDto = user != null ? Mapper.Map<User, UserDto>(user) : new UserDto();
            return View(userDto);
        }

        [HttpPost]
        public ActionResult Order(UserDto userDto)
        {
            var user = userService.Get(userDto.Id);
            user = user ?? new User{UserType = UserType.Guest, Password = Guid.NewGuid().ToString(), Username = "Guest"};
                user.Email = userDto.Email;
                user.DisplayName = userDto.DisplayName;
                user.Location = userDto.Location;
                user.DOB = userDto.DOB;
                user.Phone = userDto.Phone;
                user.Gender = userDto.Gender;
                user.LastName = userDto.DisplayName;
                user.FirstName = userDto.DisplayName;
                user.ModifiedTime = DateTime.Now;
                userService.SaveOrUpdate(user, true);
            if (Session["SmartHealthUser"] != null)
            {
                var orderDto = ((SessionDto) Session["SmartHealthUser"]).Order;
                var order = Mapper.Map<OrderDto, Order>(orderDto);
                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.Order = order;
                }
                order.OrderUser = user;
                userService.SaveOrUpdate<Order>(order, true);
                ((SessionDto) Session["SmartHealthUser"]).Order = new OrderDto();
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}
