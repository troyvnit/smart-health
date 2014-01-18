using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Payoo.Lib;
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
    [EnableCompression]
    public class HomeController : BaseController
    {
        private readonly IService<Product> productService;
        private readonly IService<ProductGroup> productGroupService;
        private readonly IService<Article> articleService;
        private readonly IService<ArticleCategory> articleCategoryService;
        private readonly IService<Menu> menuService;
        private readonly IService<Media> mediaService;
        private readonly IService<User> userService;
        private readonly IService<DocumentLog> mediaLogService;

        public HomeController(IService<Product> productService, IService<ProductGroup> productGroupService, IService<Article> articleService, IService<ArticleCategory> articleCategoryService, IService<Menu> menuService, IService<Media> mediaService, IService<User> userService, IService<DocumentLog> mediaLogService)
        {
            this.productService = productService;
            this.productGroupService = productGroupService;
            this.articleService = articleService;
            this.articleCategoryService = articleCategoryService;
            this.menuService = menuService;
            this.mediaService = mediaService;
            this.userService = userService;
            this.mediaLogService = mediaLogService;
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

            var newsesMenu = Mapper.Map<Menu, MenuDto>(menuService.GetAll().FirstOrDefault(a => a.Name.ToUpper() == Resources.SH.News.ToUpper()));
            ViewBag.NewsesMenu = newsesMenu;

            var clientSupportArticles =
                articleService.GetAll().Where(a => a.Categories.Contains(articleCategoryService.GetAll().FirstOrDefault(b => b.Name.ToUpper() == Resources.SH.ClientSupport.ToUpper())) && a.IsActived && a.IsDeleted != true).OrderByDescending(a => a.Priority).ThenByDescending(a => a.CreatedDate).Take(8).Select(
                    Mapper.Map<Article, ArticleDto>).ToList();
            ViewBag.ClientSupportArticles = clientSupportArticles;

            var clientSupportArticlesMenu = Mapper.Map<Menu, MenuDto>(menuService.GetAll().FirstOrDefault(a => a.Name.ToUpper() == Resources.SH.ClientSupport.ToUpper()));
            ViewBag.ClientSupportArticlesMenu = clientSupportArticlesMenu;

            return View();
        }

        public ActionResult GetCounter()
        {
            var userCount = userService.GetAll().Count;
            ViewBag.UserCount = userCount;
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

                var relatedProducts = productService.GetAll().Where(a => a.IsDeleted != true).Select(Mapper.Map<Product, ProductDto>).ToList();
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
                order.OrderDetails.Add(new OrderDetailDto{ Product = Mapper.Map<Product, ProductDto>(product), Quantity = quantity, Order = order});
                orderCount = order.OrderDetails.Count(a => a.Quantity != 0);
            }
            return Json(orderCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateOrderDetail(int i, int quantity)
        {
            var orderDetails = ((SessionDto) Session["SmartHealthUser"]).Order.OrderDetails.ToArray();
            orderDetails[i].Quantity = quantity;
            decimal totalAmount = orderDetails.Sum(orderDetailDto => orderDetailDto.Product.SmartHealthPrice*orderDetailDto.Quantity);
            return Json(new { productCount = orderDetails.Count(a => a.Quantity != 0), totalAmount}, JsonRequestBehavior.AllowGet);
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
                    var articles = articleService.FindAll(p => p.Categories.Any(c => c.Id == id)).OrderByDescending(a => a.CreatedDate).Select(Mapper.Map<Article, ArticleDto>).ToList();
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
            eMail.SendMail("Email","Mailformat.xml", new String[] { "Smart Health Contact", eMail.Name, eMail.Phone, eMail.Email, eMail.Message });
            return Content("Success");
        }

        public ActionResult Login(string returnUrl)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return Redirect("/" + RouteData.Values["lang"]);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserDto userDto, string returnUrl)
        {
            var user = userService.GetAll().FirstOrDefault(a => a.Username == userDto.Username && a.Password == userDto.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
                FormsAuthentication.RedirectFromLoginPage(user.Id.ToString(), true);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
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

        public ActionResult Order(int? order_no)
        {
            var user = currentUser != null ? userService.Get(currentUser.Id) : null;
            var userDto = user != null ? Mapper.Map<User, UserDto>(user) : new UserDto();
            var order = new OrderDto();
            if (order_no != null)
            {
                order = Mapper.Map<Order, OrderDto>(userService.Get<Order>((int) order_no));
                ViewBag.Order = order;
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Order(OrderDto deliveryInfo, PayType payType)
        {
            var user = userService.Get(deliveryInfo.OrderUser.Id);
            if (Session["SmartHealthUser"] != null)
            {
                var orderDto = ((SessionDto) Session["SmartHealthUser"]).Order;
                var order = Mapper.Map<OrderDto, Order>(orderDto);
                order.PayType = PayType.SmartHealth;
                order.NetAmount = order.TotalAmount;
                order.FeeAmount = 0;
                order.OrderUser = user;
                order.DeliveryCity = deliveryInfo.DeliveryCity;
                order.DeliveryAddress = deliveryInfo.DeliveryAddress;
                order.ReceiverName = deliveryInfo.ReceiverName;
                order.ReceiverPhone = deliveryInfo.ReceiverPhone;
                userService.SaveOrUpdate<Order>(order, true);
                var orderDetailString = "";
                BuidDescriptionFactory builder = new BuidDescriptionFactory();
                decimal totalAmount = 0;
                foreach (var orderDetail in order.OrderDetails)
                {
                    userService.SaveOrUpdate(orderDetail, true);
                    var totalPrice = orderDetail.Product.SmartHealthPrice * orderDetail.Quantity;
                    totalAmount += totalPrice;
                    orderDetailString += orderDetail.Product.Name + " x " + orderDetail.Quantity;
                    builder.AddItem(new PayooOrderItem(orderDetail.Product.Name, (long) orderDetail.Product.SmartHealthPrice, orderDetail.Quantity));
                }
                ((SessionDto)Session["SmartHealthUser"]).Order = new OrderDto();

                switch (payType)
                {
                    case PayType.BaoKim:
                    {
                        orderDetailString = orderDetailString.Length > 300 ? orderDetailString.Substring(0, 300) : orderDetailString;
                        var payment = new BaoKimPayment();
                        var payUrl = payment.createRequestUrl(order.Id.ToString(), "smarthealth.vn@gmail.com", totalAmount.ToString(), "0", "0", orderDetailString, Url.Action("BaoKimPaymentSuccess", "Home"), Url.Action("Order", "Home"), Url.Action("PaymentDetail", "Home"));
                        return Json(new { isSuccess = true, productCount = 0, orderId = order.Id, payUrl }, JsonRequestBehavior.AllowGet);
                    }
                    case PayType.Payoo:
                    {
                        Random r = new Random();
                        string session = r.Next().ToString();
                        PayooOrder payooOrder = new PayooOrder();
                        payooOrder.Session = session;
                        payooOrder.BusinessUsername = ConfigurationManager.AppSettings["BusinessUsername"];
                        payooOrder.OrderCashAmount = (long) totalAmount;
                        payooOrder.OrderNo = order.Id.ToString();
                        payooOrder.ShippingDays = short.Parse(ConfigurationManager.AppSettings["ShippingDays"]);
                        payooOrder.ShopBackUrl = ConfigurationManager.AppSettings["ShopBackUrl"];
                        payooOrder.ShopDomain = ConfigurationManager.AppSettings["ShopDomain"];
                        payooOrder.ShopID = long.Parse(ConfigurationManager.AppSettings["ShopID"]);
                        payooOrder.ShopTitle = ConfigurationManager.AppSettings["ShopTitle"];
                        payooOrder.StartShippingDate = DateTime.Now.ToString("dd/MM/yyyy");
                        payooOrder.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];

                        //You can do

                        //order.OrderDescription = HttpUtility.UrlEncode("<table width='100%' border='1' cellspacing='0'><thead><tr><td width='40%' align='center'><b>Tên hàng</b></td><td width='20%' align='center'><b>Đơn giá</b></td><td width='15%' align='center'><b>Số lượng</b></td><td width='25%' align='center'><b>Thành tiền</b></td></tr></thead><tbody><tr><td align='left'>HP Pavilion DV3-3502TX</td><td align='right'>23,109,210</td><td align='center'>1</td><td align='right'>23,109,210</td></tr><tr><td align='left'>FAN Notebook (B4)</td><td align='right'>266,850</td><td align='center'>1</td><td align='right'>266,850</td></tr><tr><td align='right' colspan='3'><b>Tổng tiền:</b></td><td align='right'>23,376,060</td></tr><tr><td align='left' colspan='4'>Some notes for the order</td></tr></tbody></table>");

                        //or
                        long ShippingFee = 10000;
                        payooOrder.OrderDescription = HttpUtility.UrlEncode(builder.GenerateDescription(ShippingFee, ""));

                        string XML = PaymentXMLFactory.GetPaymentXMLWithoutSign(payooOrder);
                        string ChecksumKey = ConfigurationManager.AppSettings["ChecksumKey"];
                        string Checksum = SHA1encode.hash(ChecksumKey + XML);
                        return Json(new { isSuccess = true, redirectToProviderHTML = RedirectToProvider(ConfigurationManager.AppSettings["PayooCheckout"], XML, Checksum) }, JsonRequestBehavior.AllowGet);
                    }
                        break;
                    default:
                        return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { isSuccess = false }, JsonRequestBehavior.AllowGet);
        }

        private string RedirectToProvider(string ProviderUrl, string XMLCheckout, string Checksum)
        {
            string redirect = "<html><head><title></title></head>";
            redirect += "<body><form action='" + ProviderUrl + "' method='post' style='margin-top: 50px; text-align: center;'>";
            redirect += "<noscript><input type='submit' value='Click if not redirected' /></noscript>";
            redirect += "<div id='ContinueButton' style='display: none;'><input type='submit' value='Click if not redirected' />";
            redirect += "</div><input type='hidden' name='OrdersForPayoo' value='" + XMLCheckout + "'/>";
            redirect += "</div><input type='hidden' name='CheckSum' value='" + Checksum + "'/></form>";
            redirect += "</body></html>";
            return redirect;
        }

        public ActionResult Partner()
        {
            var partners = mediaService.GetAll().Where(a => a.Type == 5).Select(Mapper.Map<Media, MediaDto>).ToList();
            ViewBag.Partners = partners;
            return View();
        }

        public ActionResult Videos()
        {
            var videos = mediaService.GetAll().Where(a => a.Type == 3).Select(Mapper.Map<Media, MediaDto>).ToList();
            ViewBag.Videos = videos;
            return View();
        }

        public ActionResult Document()
        {
            var documents = mediaService.GetAll<Document>().Select(Mapper.Map<Document, DocumentDto>).ToList();
            ViewBag.Documents = documents;
            return View();
        }

        [HttpPost]
        public ActionResult DownloadDocument(string email, int documentId)
        {
            var document = mediaService.Get<Document>(documentId);
            var eMail = new EMail
            {
                FromAddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromAddress"),
                ToAddress = email,
                SMTPClient = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient"),
                UserName = System.Configuration.ConfigurationManager.AppSettings.Get("UserName"),
                Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password"),
                ReplyTo = System.Configuration.ConfigurationManager.AppSettings.Get("ReplyTo"),
                SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort"),
                isEnableSSL =
                    System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() ==
                    "YES"
            };
            eMail.SendMail("Email", "MailFormat_DownloadDocument.xml", new String[] { "Smart Health Download Document", "Thông tin tài liệu: ", document.Name, document.DownloadUrl });
            mediaLogService.SaveOrUpdate(new DocumentLog{Email = email, Document = mediaService.Get<Document>(documentId)}, true);
            return Content("Success");
        }

        public ActionResult Rating(int idBox, int rate)
        {
            var product = productService.Get(idBox);
            if (product != null)
            {
                product.RatingCount = product.RatingCount + 1;
                product.Rating = product.Rating == 0 ? product.Rating + rate : (product.Rating + rate) / 2 + ((product.Rating + rate) % 2 > 0 ? 1 : 0);
                productService.SaveOrUpdate(product, true);
            }
            return Content("Success");
        }

        public ActionResult BaoKimPaymentSuccess(BaoKimOrderDto baoKimOrder)
        {
            if (baoKimOrder != null)
            {
                var order = userService.Get<Order>(Convert.ToInt32(baoKimOrder.order_id));
                order.TotalAmount = baoKimOrder.total_amount;
                order.NetAmount = baoKimOrder.net_amount;
                order.FeeAmount = baoKimOrder.fee_amount;
                order.TransactionStatus = baoKimOrder.transaction_status;
                order.IsPayed = baoKimOrder.transaction_status == 4;
                order.PayType = PayType.BaoKim;
                order.OrderUser.Email = baoKimOrder.customer_email;
                order.OrderUser.DisplayName = baoKimOrder.customer_name;
                order.OrderUser.Location = baoKimOrder.customer_address;
                order.OrderUser.Phone = baoKimOrder.customer_phone;
                userService.SaveOrUpdate<Order>(order, true);
            }
            return View();
        }

        public ActionResult PaymentDetail()
        {
            return null;
        }

        public ActionResult PayooPaymentSuccess()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PayooPaymentNotifyListener(string notifyData)
        {
            if (!string.IsNullOrEmpty(notifyData))
            {

                PayooNotify listener = new PayooNotify(notifyData);

                PaymentNotification invoice = listener.GetPaymentNotify();

                //Xác thực chữ ký của payoo trong gói notify
                PayooSignature py = new PayooSignature(Server.MapPath(@"App_Data\Certificates\payoo_public_cert.pem"));
                if (py.Verify(listener.NotifyData, listener.Signature))
                {
                    //Neu trang thai don hang cua payoo là PAYMENT_RECEIVED -> tien hanh xu ly databse cap nhat don hang
                    if (invoice.State == "PAYMENT_RECEIVED")
                    {
                        var order = userService.Get<Order>(Convert.ToInt32(invoice.OrderNo));
                        order.TotalAmount = invoice.OrderCashAmount;
                        order.NetAmount = invoice.OrderCashAmount;
                        order.FeeAmount = 0;
                        order.IsPayed = invoice.State == "PAYMENT_RECEIVED";
                        order.PayType = PayType.Payoo;
                        userService.SaveOrUpdate<Order>(order, true);
                    }
                }
                else
                {
                    //ConfirmToPayoo fail. Log for manual investigation.
                    string LogPath = Server.MapPath(@"App_Data\log.txt");
                    LogWriter.WriteLog(LogPath, "ConfirmToPayoo fail. ");
                }
            }
            return Content("Success");
        }
    }
}
