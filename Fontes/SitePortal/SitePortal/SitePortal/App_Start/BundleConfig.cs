using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SitePortal.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/EscolhaSite").Include(
                        "~/Scripts/Libs/jquery.js",
                        "~/Scripts/Libs/show_hide.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Rotativo").Include(
                        "~/Scripts/Libs/jquery-1.7.1.min.js",
                        //"~/Scripts/Libs/jquery-1.11.0.min.js",
                        "~/Scripts/Libs/coin-slider.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Rotator").Include(
                        "~/Scripts/Libs/jquery.easing.1.3.min.js",
                        "~/Scripts/Libs/jquery.list-rotator.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/RotatorJssor").Include(
                        "~/Scripts/Libs/jssor_slider/jssor.js",
                        "~/Scripts/Libs/jssor_slider/jssor.slider.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/CMSCounter").Include(
                        "~/Scripts/jCMSCounter.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Geral").Include(
                        "~/Scripts/jBusca.js",
                        "~/Scripts/jHome.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Calculadora").Include(
                        "~/Scripts/jCalculadora.js",
                        "~/Scripts/Libs/jquery.maskMoney.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/modern").Include(
                        "~/Scripts/Libs/modernizr.custom.86080.js"
                        ));

            #endregion

            #region -> CSS

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/style.css"
            ));

            bundles.Add(new StyleBundle("~/Content/Rotativo").Include(
                "~/Content/coin-slider-styles.css"
            ));

            bundles.Add(new StyleBundle("~/Content/Rotator").Include(
                "~/Content/list-rotator.css"
            ));

            bundles.Add(new StyleBundle("~/Content/demo").Include(
                "~/Content/demo.css",
                "~/Content/style1.css"
            ));
            #endregion

        }

    }
}
