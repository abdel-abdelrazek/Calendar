using System.Web;
using System.Web.Optimization;

namespace Calendar
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.unobtrusive-ajax.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/rxjs").Include(
                       "~/Scripts/rxjs.js"));

            bundles.Add(new ScriptBundle("~/bundles/searchtextbox").Include(
                       "~/Scripts/events-filter.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                      "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
                   "~/Scripts/moment.js",
                      "~/Content/fontawesome/js/all.js",
                   "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            bundles.Add(new ScriptBundle("~/bundles/general").Include(
                    "~/Scripts/general.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/fontawesome/css/all.css",
                      "~/Content/bootstrap-datetimepicker.css"));

        }

    }
}
