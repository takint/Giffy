using System.Web;
using System.Web.Optimization;

namespace Giffy
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/vendors/css")
                       //.Include("~/wwwroot/vendors/bower_components/mediaelement/build/mediaelementplayer.min.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/animate.css/animate.min.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/material-design-iconic-font/dist/css/material-design-iconic-font.min.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/bootstrap-sweetalert/lib/sweet-alert.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/angular-loading-bar/src/loading-bar.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/lightgallery/dist/css/lightgallery.min.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/jquery.gifplayer/gifplayer.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/ng-tags-input/ng-tags-input.bootstrap.min.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/vendors/bower_components/ng-tags-input/ng-tags-input.min.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/css/app.min.1.css", new CssRewriteUrlTransformWrapper())
                       .Include("~/wwwroot/css/app.min.2.css", new CssRewriteUrlTransformWrapper()));
            
            Scripts.DefaultTagFormat = @"<script src=""{0}"" defer></script>";

            bundles.Add(new ScriptBundle("~/vendors/core").Include(
                        "~/wwwroot/vendors/bower_components/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/vendors/angular").Include(
                        "~/wwwroot/vendors/bower_components/angular/angular.js",
                        "~/wwwroot/vendors/bower_components/angular-animate/angular-animate.min.js",
                        "~/wwwroot/vendors/bower_components/angular-resource/angular-resource.min.js"));

            bundles.Add(new ScriptBundle("~/vendors/lightgallery").Include(
                        "~/wwwroot/vendors/bower_components/lightgallery/dist/js/lightgallery.min.js"));

            bundles.Add(new ScriptBundle("~/vendors/angular/modules").Include(
                        "~/wwwroot/vendors/bower_components/angular-ui-router/release/angular-ui-router.min.js",
                        "~/wwwroot/vendors/bower_components/angular-loading-bar/src/loading-bar.js",
                        "~/wwwroot/vendors/bower_components/oclazyload/dist/ocLazyLoad.min.js",
                        "~/wwwroot/vendors/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
                        "~/wwwroot/vendors/bower_components/angular-local-storage/dist/angular-local-storage.min.js",
                        "~/wwwroot/vendors/bower_components/angular-hideheader/dist/angular-hide-header.min.js",
                        "~/wwwroot/vendors/bower_components/angular-validation/dist/angular-validation.min.js",
                        "~/wwwroot/vendors/bower_components/angular-validation/dist/angular-validation-rule.min.js"));

            bundles.Add(new ScriptBundle("~/vendors/common").Include(
                       "~/wwwroot/vendors/bower_components/jquery.nicescroll/jquery.nicescroll.min.js",
                       "~/wwwroot/vendors/bower_components/bootstrap-sweetalert/lib/sweet-alert.min.js",
                       "~/wwwroot/vendors/bower_components/Waves/dist/waves.min.js",
                       "~/wwwroot/vendors/bootstrap-growl/bootstrap-growl.min.js",
                       "~/wwwroot/vendors/bower_components/ng-table/dist/ng-table.min.js",
                       "~/wwwroot/vendors/bower_components/ng-tags-input/ng-tags-input.min.js"));

            bundles.Add(new ScriptBundle("~/vendors/misloading").Include(
                       "~/wwwroot/vendors/bower_components/flot/jquery.flot.js",
                       "~/wwwroot/vendors/bower_components/flot.curvedlines/curvedLines.js",
                       "~/wwwroot/vendors/bower_components/flot/jquery.flot.resize.js",
                       "~/wwwroot/vendors/bower_components/moment/min/moment-with-locales.min.js",
                       "~/wwwroot/vendors/bower_components/fullcalendar/dist/fullcalendar.min.js",
                       "~/wwwroot/vendors/bower_components/flot-orderBars/js/jquery.flot.orderBars.js",
                       "~/wwwroot/vendors/bower_components/flot/jquery.flot.pie.js",
                       "~/wwwroot/vendors/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js",
                       "~/wwwroot/vendors/bower_components/angular-nouislider/src/nouislider.min.js"));

            #if DEBUG

            bundles.Add(new ScriptBundle("~/app").Include(
                       "~/wwwroot/app/app.js",
                       "~/wwwroot/app/config.js",
                       "~/wwwroot/app/factories.js",
                       "~/wwwroot/app/filters.js",
                       "~/wwwroot/app/pages/account/controllers/login.js",
                       "~/wwwroot/app/pages/account/controllers/profile.js",
                       "~/wwwroot/app/pages/account/controllers/profile/posts.js",
                       "~/wwwroot/app/pages/common/controllers/main.js",
                       "~/wwwroot/app/pages/league/controllers/league.js",
                       "~/wwwroot/app/pages/player/controllers/player.js",
                       "~/wwwroot/app/pages/management/common/controllers/manage.js",
                       "~/wwwroot/app/pages/management/posts/controllers/posts.js",
                       "~/wwwroot/app/pages/team/controllers/team.js",
                       "~/wwwroot/app/pages/wall/controllers/gag.js",
                       "~/wwwroot/app/pages/wall/controllers/wall.js",
                       "~/wwwroot/app/services.js",
                       "~/wwwroot/app/templates.js",
                       "~/wwwroot/app/modules/template.js",
                       "~/wwwroot/app/modules/ui.js",
                       "~/wwwroot/app/modules/charts/flot.js",
                       "~/wwwroot/app/modules/charts/other-charts.js",
                       "~/wwwroot/app/modules/form.js",
                       "~/wwwroot/app/modules/media.js",
                       "~/wwwroot/app/modules/components.js",
                       "~/wwwroot/app/modules/calendar.js",
                       "~/wwwroot/app/modules/upload.js"));

            #else
            
            bundles.Add(new ScriptBundle("~/app").Include(
                       "~/wwwroot/publish/min/app.js"));

            BundleTable.EnableOptimizations = true;

            #endif
        }
    }

    public class CssRewriteUrlTransformWrapper : IItemTransform
    {
        public string Process(string includedVirtualPath, string input)
        {
            return new CssRewriteUrlTransform().Process("~" + VirtualPathUtility.ToAbsolute(includedVirtualPath), input);
        }
    }
}
