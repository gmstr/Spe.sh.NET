using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spe.sh.Net.Controllers
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
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var uri = new Uri(collection["url"].ToString());
                var token = Application.Repository.AddUrl(uri);
                ViewData["token"] = token;
            }
            catch (UriFormatException ex)
            {
                TempData["error"] = "The provided URL was in an invalid format. Please try again.";
            }
            catch (Exception ex)
            {
                return RedirectToAction("Problem", "Error");
            }
            return View();
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
