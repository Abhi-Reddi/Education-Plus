using System.Configuration;
using System.Web.Mvc;

namespace EduPlus.WebUI.Controllers
{
    public class CommonBaseClass : Controller
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["EduPlusCString"].ConnectionString;
            }
        }
    }
}
