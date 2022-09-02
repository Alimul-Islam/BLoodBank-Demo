using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BB2.Models;

namespace BB2.Controllers
{
    public class HomeController : Controller
    {
        FinalBB2Entities db = new FinalBB2Entities();
        public ActionResult Index()
        {

            var req = db.ReqBloods.OrderByDescending(r => r.ReqId).ToList().Take(3);

            ViewBag.Request = req;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}