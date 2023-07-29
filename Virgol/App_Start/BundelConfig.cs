using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace Virgol.App_Start
{
    public class BundelConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //for css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                                                             "~/Content/bootstrap.css",
                                                             "~/Content/bootstrap.min.css",
                                                             "~/Content/bootstrap.rtl.css",
                                                             "~/Content/bootstrap.rtl.min.css",
                                                             "~/Content/bootstrap.grid.css",
                                                             "~/Content/bootstrap.grid.min.css",
                                                             "~/Content/main.css",
                                                             "~/Content/variables.css",
                                                             "~/Content/swiper-bundle.min.css",
                                                             "~/Content/Site.css"
                                                         ));
            //for js
            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                                  "~/Scripts/bootstrap.js",
                                  "~/Scripts/jquery-{version}.js",
                                  "~/Scripts/jquery.validate*",
                                  "~/Scripts/swiper-bundle.min.js",
                                  "~/Scripts/main.js",
                                  "~/Scripts/bootstrap.bundel.js"));
        }

    }
}