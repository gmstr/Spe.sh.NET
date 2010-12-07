using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spe.sh.NET.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /
        [HttpPost]
        public ActionResult Index(string url)
        {
            ViewData["token"] = Shorten(url);

            return View();
        }

        //
        // GET/POST: /s?url={url}
        public string Shorten(string url)
        {
            string token = "";
            try
            {
                var uri = new Uri(url);
                token = Application.Repository.AddUrl(uri);
                ViewData["token"] = token;
            }
            catch (UriFormatException)
            {
                TempData["error"] = "The provided URL was in an invalid format. Please try again.";
            }
            catch (Exception ex)
            {
                return "An error has occurred: " + ex.Message;
            }
            return Request.Url.Scheme + "://" + Request.Url.Authority + "/" + token;
        }

        //
        // GET: /{token}
        [HttpGet]
        public ActionResult Direct(string token)
        {
            var uri = Application.Repository.ResolveUrl(token, Request.ServerVariables["REMOTE_ADDR"], Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "");

            return uri == null ? (ActionResult)RedirectToAction("NotFound", "Error") : Redirect(uri.ToString());

        }

    }
}
