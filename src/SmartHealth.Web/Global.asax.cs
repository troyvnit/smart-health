using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Core.Infrastructure.Nhibernate;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Infrastructure.Web.Bootstrap;
using SmartHealth.Infrastructure.Web.Nhibernate;
using SmartHealth.Web.Mappers;

namespace SmartHealth.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        private WebSessionStorage webSessionStorage;
        private IService<User> userService = new Service<User>(new UserRepository()); 
        public override void Init()
        {
            base.Init();
            webSessionStorage = new WebSessionStorage(this);
        }       

        protected void Application_Start()
        {
            ComponentRegistrar.RegisterComponents("SmartHealth.Web");

            AreaRegistration.RegisterAllAreas();
            AutoMapperConfiguration.Configure(); 
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        int id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                        string roles = string.Empty;
                        User user = userService.Get(id);

                            roles = user.UserType.ToString();
                        //let us extract the roles from our own custom cookie


                        //Let us set the Pricipal with our user specific details
                        e.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(id.ToString(), "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }
    }
}