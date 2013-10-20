﻿using System.Web;
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



            BundleTable.EnableOptimizations = true;
        }
    }
}