using System.Web;
using System.Web.Optimization;

namespace eCommerceProject
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
                        "~/Scripts/modernizr-*",
                        "~/libs/fontawesome-free-5.12.1-web/js/all.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                        "~/Scripts/sitejs.js"));

            bundles.Add(new ScriptBundle("~/Dashboard/jscripts").Include(
            "~/libs/plugins/jquery/jquery.min.js",
            "~/libs/plugins/jquery-ui/jquery-ui.min.js",
            "~/libs/plugins/bootstrap/js/bootstrap.bundle.min.js",
            "~/libs/plugins/chart.js/Chart.min.js",
            "~/libs/plugins/sparklines/sparkline.js",
            "~/libs/plugins/jqvmap/jquery.vmap.min.js",
            "~/libs/plugins/jqvmap/maps/jquery.vmap.usa.js",
            "~/libs/plugins/jquery-knob/jquery.knob.min.js",
            "~/libs/plugins/moment/moment.min.js",
            "~/libs/plugins/daterangepicker/daterangepicker.js",
            "~/libs/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
            "~/libs/plugins/summernote/summernote-bs4.min.js",
            "~/libs/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
            "~/libs/dist/js/adminlte.js",
            "~/libs/dist/js/pages/dashboard.js",
            "~/libs/dist/js/demo.js"
            ));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/libs/fontawesome-free-5.12.1-web/css/all.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Dashboard/styles").Include(
              "~/libs/plugins/fontawesome-free/css/all.min.css",
              "~/libs/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
              "~/libs/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
              "~/libs/plugins/jqvmap/jqvmap.min.css",
              "~/libs/dist/css/adminlte.min.css",
              "~/libs/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
              "~/libs/plugins/daterangepicker/daterangepicker.css",
              "~/libs/plugins/summernote/summernote-bs4.css"
              ));
        }
    }
}
