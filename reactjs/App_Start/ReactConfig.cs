using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebWallet.WebUI.ReactConfig), "Configure")]

namespace WebWallet.WebUI
{
    public static class ReactConfig
    {
        public static void Configure()
        {
            ReactSiteConfiguration.Configuration.AddScript("~/Scripts/transactionhistory.jsx");
            ReactSiteConfiguration.Configuration.AddScript("~/Scripts/registration.jsx");
            ReactSiteConfiguration.Configuration.AddScript("~/Scripts/main.jsx");
            ReactSiteConfiguration.Configuration.AddScript("~/Scripts/cabinet.jsx");
            ReactSiteConfiguration.Configuration.AddScript("~/Scripts/about.jsx");
            ReactSiteConfiguration.Configuration.AddScript("~/Scripts/sendmoney.jsx");
        }
    }
}