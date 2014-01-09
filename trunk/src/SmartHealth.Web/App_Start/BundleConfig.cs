using System.Web;
using System.Web.Optimization;

namespace SmartHealth.Web
{
    public class BundleConfig
    {
         // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.8.2.js"));

            

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.8.24.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.unobtrusive*",
                       "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/ie7/js").Include(
                       "~/Scripts/json2.js"));


            bundles.Add(new ScriptBundle("~/bundles/master/js").Include(                      
                        "~/Scripts/bx-slider.js",
                        "~/Scripts/jquery.colorbox.js",
                        "~/Scripts/customize-forms.js",
                         "~/Scripts/browsers.js",
                       "~/Scripts/combobox.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/master/js/header").Include(
                       "~/Scripts/jqueryTab.js",
                       "~/Areas/Administrator/ClipOne/plugins/jquery-ui/jquery-ui-1.10.2.custom.min.js",
                       "~/Scripts/js/cloud-zoom.1.0.2.js",
                       "~/Scripts/js/jquery.nivo.slider.pack.js",
                       "~/Scripts/js/jquery.flexslider-min.js",
                       "~/Scripts/js/jquery.jcarousel.min.js",
                       "~/Scripts/js/jquery.jcarousel.min_call.js",
                       "~/Scripts/jquery.lazyload.min.js",
                       "~/Scripts/jquery.sharrre.min.js",
                       "~/Scripts/js/masonry.pkgd.min.js",
                       "~/Scripts/js/jRating.jquery.js",
                       "~/Areas/Administrator/ClipOne/plugins/bootstrap/js/bootstrap.min.js",
                       "~/Areas/Administrator/ClipOne/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/master/js/footer").Include(
                       "~/Scripts/js/taadv_floating.js",
                       "~/FancyBox/jquery.fancybox.js",
                       "~/FancyBox/helpers/jquery.fancybox-buttons.js",
                       "~/FancyBox/helpers/jquery.fancybox-thumbs.js",
                       "~/FancyBox/helpers/jquery.fancybox-media.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/style.css",
                "~/Content/style-banner.css",
                "~/Content/nivo-slider.css",
                "~/Content/nivo-slider/themes/default/default.css",
                "~/Content/flexslider.css",
                "~/Content/jRating.jquery.css",
                "~/FancyBox/jquery.fancybox.css",
                "~/FancyBox/helpers/jquery.fancybox-buttons.css",
                "~/FancyBox/helpers/jquery.fancybox-thumbs.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css/contact").Include(
                "~/Content/contact.css",
                "~/Areas/Administrator/ClipOne/plugins/bootstrap/css/bootstrap.min.css",
                "~/Areas/Administrator/ClipOne/plugins/datepicker/css/datepicker.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                "~/Areas/Administrator/ClipOne/plugins/bootstrap/css/bootstrap.min.css"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}