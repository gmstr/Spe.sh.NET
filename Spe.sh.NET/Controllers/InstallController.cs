using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;
using DbLinq.Sqlite;

namespace Spe.sh.NET.Controllers
{
    public class InstallController : Controller
    {
        //
        // GET: /Install/

        public ActionResult Index()
        {
            Application.Repository.SetupInitialData();

            return RedirectToAction("Index", "Home");
        }

    }
}
