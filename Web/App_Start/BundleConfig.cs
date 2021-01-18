using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/AppJs").Include(
                       "~/js/jquery.min.js",
                       "~/js/owl.carousel.min.js",
                       "~/js/bootstrap.min.js",
                       "~/js/revslider.js",
                       "~/js/common.js",
                       "~/js/validator/jquery.validate.js"
                       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/bootstrap.min.css",
                      "~/css/font-awesome.css",
                      "~/css/owl.carousel.min.css",
                      "~/css/owl.theme.min.css",
                      "~/css/jquery.mobile-menu.css",
                      "~/css/style.css",
                      "~/css/revslider.css",
                      "~/css/blogmate.css",
                       "~/css/cloudzoom.css",
                      "~/css/app.custom.css"
                      ));
        }
    }
}
