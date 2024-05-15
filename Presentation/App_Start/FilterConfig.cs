using System.Web;
using System.Web.Mvc;

namespace Presentation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //we restricting access to information from this application 
            filters.Add(new AuthorizeAttribute());
        }
    }
}
