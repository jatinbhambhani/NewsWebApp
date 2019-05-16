using System.Web;
using System.Web.Optimization;

namespace _5032_MVC_News
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/DataTables/css/dataTables.bootstrap.css",
                       "~/Content/bootstrap-datepicker3.standalone.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/base/css").Include(
                    "~/Content/themes/jquery-ui.css",
                    "~/Content/themes/jquery-ui.min.css",
                    "~/Content/themes/jquery-ui.structure.css",
                    "~/Content/themes/jquery-ui.theme.css",
                    "~/Content/themes/jquery-ui.theme.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                    "~/Scripts/newsScript.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jquery-ui.min.js"));
        }
    }
}
