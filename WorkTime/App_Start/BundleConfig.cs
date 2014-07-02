using System.Web;
using System.Web.Optimization;

namespace WorkTime
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jQuery/jquery-{version}.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jQuery/jquery.validate*"
                    ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/jQuery/modernizr-*"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/jQuery/bootstrap.js",
                    "~/Scripts/jQuery/respond.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/site.css"
                    ));

            bundles.Add(new StyleBundle("~/extjs/css").Include(
                    "~/Scripts/Extjs/resources/css/ext-all-debug.css",
                    "~/Scripts/Extjs/src/ux/grid/css/*.css"
                    ));

            bundles.Add(new ScriptBundle("~/extjs/scripts").Include(
                    "~/Scripts/Extjs/ext-all-debug.js",
                    "~/Scripts/Ext/Common/ux/InputTextMask.js"
                    ));



        }
    }
}
