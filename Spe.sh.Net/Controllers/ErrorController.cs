using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spe.sh.Net.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/NotFound
        public ActionResult NotFound(FormCollection collection)
        {
            return View();
        }

        //
        // GET: /Error/Problem
        public ActionResult Problem(FormCollection collection)
        {
            return View();
        }
    }
}
