using System.Web.Optimization;

namespace DG.Haer.Api
{
    public class BoundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        { 
            bundles.Add(new ScriptBundle("~/scripts/vendors").Include(
                "~/Scripts/vendors/jquery-1.12.3.js",
                "~/Scripts/vendors/angular.js",
                "~/Scripts/vendors/angular-route.js",
                "~/Scripts/vendors/angular-locale_pl-pl.js",
                "~/Scripts/vendors/angular-messages.js",
                "~/Scripts/vendors/angular-resource.js",
                "~/Scripts/vendors/semantic.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/app").Include(
                "~/Scripts/app/app.mdl.js",
                "~/Scripts/app/app.conf.js",
                "~/Scripts/app/app.route.js",
                "~/Scripts/app/app.ctrl.js",
                "~/Scripts/app/app.services.js",
                "~/Scripts/app/core/contacts/contacts.mdl.js",
                "~/Scripts/app/core/contacts/add/add.ctrl.js",
                "~/Scripts/app/core/contacts/list/list.ctrl.js",
                "~/Scripts/app/services/services.mdl.js",
                "~/Scripts/common/common.mdl.js"
            ));

            bundles.Add(new StyleBundle("~/style").Include(
                "~/Content/css/semantic.css",
                "~/Content/css/site.css"
            ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
