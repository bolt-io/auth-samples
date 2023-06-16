using System.Web;
using System.Web.Mvc;

namespace framework_4._7._2_B2C_MVC_WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
