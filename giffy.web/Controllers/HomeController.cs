using System.Configuration;
using System.Web.Mvc;
using Giffy.DataAccess.Services;

namespace Giffy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;

        public HomeController(IPostService postService)
        {
            this.postService = postService;
        }

        public ActionResult Index(string attr1, string attr2, string attr3, string attr4)
        {
            ViewBag.SiteName = ConfigurationManager.AppSettings["SiteName"];
            ViewBag.SiteTitle = ConfigurationManager.AppSettings["SiteTitle"];
            ViewBag.FacebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
            ViewBag.AbsoluteUri = Request.Url.AbsoluteUri;
            if(attr1 == "gag" && !string.IsNullOrEmpty(attr2))
            {
                var gag = postService.GetGag(attr2);
                if(gag != null)
                {
                    return View(gag);
                }
            }
            return View();
        }

        public ActionResult AuthComplete()
        {
            return View();
        }
    }
}
