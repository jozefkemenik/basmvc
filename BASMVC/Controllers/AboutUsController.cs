using BAS.Core.Services;
using BAS.Repository.Model;
using BASMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BASMVC.Controllers
{
    public class AboutUsController : BaseController
    {

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.ActiveIndex = 5;
            return View();
        }

        [HttpPost]
        public ActionResult Contact(MessageVM vm)
        {
            if (!ModelState.IsValid)
                return Json(new { wasSent = false });

            var subject = "Sprava od " + vm.Name;
            var message = "<table>"+
                            "<tr><td>Email:</td><td>" + vm.Email + "</td></tr>"+
                            "<tr><td>Telefon:</td><td>" + vm.Phone + "</td></tr>"+
                            "<tr><td>Sprava:</td><td>" + vm.Message + "</td></tr>"+
                          "</table>";
            var errorMessage = "";
            var wasSent = BAS.Core.Helper.EmailHelper.SendEmail(subject, message, out errorMessage);
            return Json(new { wasSent = wasSent });
        }

        public ActionResult History()
        {
            ViewBag.ActiveIndex = 5;
            return View();
        }

        public ActionResult Teachers()
        {
            ViewBag.ActiveIndex = 5;
            return View();
        }

        public ActionResult Office()
        {
            ViewBag.ActiveIndex = 5;
            return View();
        }

    }
}