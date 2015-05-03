using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace module_Backend.Controllers
{
    public class PanelController : Controller
    {
        //
        // GET: /Panel/
        public ActionResult Index()
        {
            ViewBag.Title = "Panel";

            return View();
        }
	}
}