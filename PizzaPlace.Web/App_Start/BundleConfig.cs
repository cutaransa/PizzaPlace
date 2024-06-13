using System.Web.Optimization;

namespace PizzaPlace.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Assets/misc/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Assets/jquery/jquery.js",
                       "~/Assets/jquery/jquery-ui.js",
                       "~/Assets/misc/tableExport.js",
                       "~/Assets/misc/jquery.base64.js",
                       "~/Assets/misc/sprintf.js",
                       "~/Assets/misc/jspdf.js",
                       "~/Assets/misc/base64.js",
                       "~/Assets/misc/html2canvas.js",
                       "~/Assets/misc/metisMenu.min.js",
                       "~/Assets/misc/sb-admin-2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                   .Include(
                        "~/Assets/ng/angular.min.js",
                        "~/Assets/ng/angular-route.min.js",
                        "~/Assets/ng/angular-cookies.min.js",
                        "~/Assets/ng/angular-animate.min.js",
                        "~/Assets/ng/angular-touch.min.js",
                        "~/Assets/ng/angular-filter.min.js",
                        "~/Assets/ng/ngMask.min.js",
                        "~/Assets/ng/Chart.js",
                        "~/Assets/ng/angular-chart.js",
                        "~/Assets/ng/angular-chart.min.js",
                        "~/Assets/ng/ui-grid.min.js",
                        "~/Assets/ng/checklist-model.js",
                        "~/Assets/misc/toaster.min.js",
                        "~/Assets/misc/loading-bar.min.js",
                        "~/Assets/misc/bootstrap-angular-validation-all.min.js",
                        "~/Assets/misc/ui-bootstrap-custom-tpls-2.5.0.min.js",
                        "~/Assets/misc/textAngular-rangy.min.js",
                        "~/Assets/misc/textAngular-sanitize.min.js",
                        "~/Assets/misc/textAngular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                    .Include("~/Assets/app/app.module.js",
                            "~/Assets/app/app.route.js",
                            "~/Assets/app/app.config.js")
                    .IncludeDirectory("~/Assets/app/services", "*.js", true)
                    .IncludeDirectory("~/Assets/app/directives", "*.js", true)
                    .IncludeDirectory("~/Assets/app/controllers", "*.js", true)
            );

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/bootstrap/js/bootstrap.js",
                      "~/Assets/misc/respond.js",
                      "~/Assets/misc/bootbox.min.js",
                      "~/Assets/styles/metisMenu.min.js",
                      "~/Assets/styles/sb-admin-2.min.js"));

            bundles.Add(new StyleBundle("~/Assets/css").Include(
                      "~/Assets/bootstrap/css/bootstrap.css",
                      "~/Assets/jquery/themes/base/jquery-ui.min.css",
                      "~/Assets/styles/ui-bootstrap-custom-2.5.0-csp.css",
                      "~/Assets/styles/toaster.min.css",
                      "~/Assets/styles/loading-bar.min.css",
                      "~/Assets/styles/metisMenu.min.css",
                      "~/Assets/styles/easyquery.css",
                      "~/Assets/styles/textAngular.css",
                      "~/Assets/styles/ui-grid.min.css",
                      "~/Assets/styles/easyquery.css",
                      "~/Assets/styles/eqview.css",
                      "~/Assets/styles/easychart.css",
                      "~/Assets/styles/sb-admin-2.css",
                      "~/Assets/styles/styles.css"));

            bundles.Add(new StyleBundle("~/Assets/fonts/css").Include(
                      "~/Assets/bootstrap/fonts/font-awesome.min.css", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = false;
        }
    }
}