using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace BackofficeCMS.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/Core").Include(
                        "~/Content/assets/global/plugins/jquery-1.11.0.min.js",
                        "~/Content/assets/global/plugins/jquery-migrate-1.2.1.min.js",
                        "~/Content/assets/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                        "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                        "~/Content/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                        "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Content/assets/global/plugins/jquery.blockui.min.js",
                        "~/Content/assets/global/plugins/jquery.cokie.min.js",
                        "~/Content/assets/global/plugins/uniform/jquery.uniform.min.js",
                        "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                        "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                        "~/Content/assets/global/plugins/select2/select2.min.js",
                        "~/Content/assets/global/scripts/metronic.js",
                        "~/Content/assets/admin/layout2/scripts/layout.js",
                        "~/Content/assets/admin/layout2/scripts/demo.js",
                        "~/Content/assets/admin/pages/scripts/login.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Usuario").Include(
                "~/Scripts/BackofficeCMS/jUsuario.js"
                ));

            #endregion


            #region -> CSS

            bundles.Add(new StyleBundle("~/Content/Global").Include(
                "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Content/assets/global/plugins/uniform/css/uniform.default.css",
                "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                "~/Content/assets/global/plugins/select2/select2.css",
                "~/Content/assets/admin/pages/css/login.css"
            ));

            bundles.Add(new StyleBundle("~/Content/Theme").Include(
                "~/Content/assets/global/css/components.css",
                "~/Content/assets/global/css/plugins.css",
                "~/Content/assets/admin/layout2/css/layout.css",
                "~/Content/assets/admin/layout2/css/custom.css",
                "~/Content/assets/admin/layout2/css/themes/default.css"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/style.css"
            ));

            #endregion

        }

    }
}
