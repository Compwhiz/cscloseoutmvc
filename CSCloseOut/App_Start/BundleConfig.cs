using System.Web;
using System.Web.Optimization;

namespace CSCloseOut
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Foundation
            bundles.Add(new ScriptBundle("~/bundles/foundation")
            {
                //CdnFallbackExpression = "window.Foundation"
            }.Include(
                //"~/Scripts/foundation/fastclick.js",
                //"~/Scripts/foundation/jquery.cookie.js",
                //"~/Scripts/foundation/foundation.js",
                //"~/Scripts/foundation/foundation.*"
                 "~/Scripts/mm-foundation-tpls-{version}.js"
                 ));
            #endregion

            #region jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));
            #endregion

            #region AngularJS

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                 "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularUi").Include(
                 "~/Scripts/angular-ui-router.js",
                 "~/Scripts/lodash.js"));

            #endregion

            #region App
            bundles.Add(new ScriptBundle("~/bundles/app")
                 .Include("~/App/App.js")
                 .IncludeDirectory("~/App", "*Module.js", true)
                 .IncludeDirectory("~/App", "*Provider.js", true)
                 .IncludeDirectory("~/App", "*Factory.js", true)
                 .IncludeDirectory("~/App", "*Service.js", true)
                 .IncludeDirectory("~/App", "*Controller.js", true)
                 .IncludeDirectory("~/App", "exl*", true)//Directives
                 );
            #endregion

            #region Lodash
            bundles.Add(new ScriptBundle("~/bundles/lodash").Include("~/Scripts/lodash.js"));
            #endregion

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
            {
                CdnFallbackExpression = "window.Modernizr"
            }.Include(
                 "~/Scripts/modernizr-{version}.js"));

            #region CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/Site2.css"));
            #endregion
        }
    }
}
