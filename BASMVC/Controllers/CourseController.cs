using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASMVC.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/

        public ActionResult Music()
        {
            ViewBag.ActiveIndex = 1;
            return View();
        }


        public ActionResult Brush()
        {
            ViewBag.ActiveIndex = 1;
            return View();
        }


        public ActionResult Dance()
        {
            ViewBag.ActiveIndex = 1;
            return View();
        }

        public ActionResult Art()
        {
            ViewBag.ActiveIndex = 1;
            return View();
        }


        public ActionResult Fees()
        {
            ViewBag.ActiveIndex = 1;
            return View();
        }

    }
}
