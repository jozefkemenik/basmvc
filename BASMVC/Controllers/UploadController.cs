

using BAS.Core.Helper;
using BAS.Core.Services;
using BAS.Repository.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BASMVC.Controllers
{
    [Authorize]
    public class UploadController : BaseController
    {

        public class SessionData : BAS.Core.SessionContext<SessionData>
        {
            public int? AlbumId { get; set; }
        }

        IAlbumService _ias;
        public UploadController(IAlbumService ias)
        {
            _ias = ias; 
        }


        public ActionResult GetAlbums()
        {
            SessionData.Current.AlbumId = null;
            _ias.Initialize(ServerPath);
            var model = _ias.GetAlbums(CurrentUserID.Value);
            return PartialView("_UserAlbumComponentPartial", model);
        }


        public ActionResult GetAlbumById(int id)
        {
            ViewBag.ActiveIndex = 3;
            SessionData.Current.AlbumId = id;
            _ias.Initialize(ServerPath);
            var model = _ias.GetAlbumById(id);
            return PartialView("/Views/Admin/GalleryAlbum.cshtml", model);
        }

        public ActionResult ShowPicturesInAlbum()
        {
            _ias.Initialize(ServerPath);
            var model = _ias.GetAlbumById(SessionData.Current.AlbumId.Value);
            return View("_ThumbnailContainerPartial", model);
        }


        public ActionResult SaveAlbumDetail(ICollection<File_t> files)
        {

            _ias.Initialize(ServerPath);
            _ias.UpdateFilesInAlbum(files, SessionData.Current.AlbumId.Value, CurrentUserID.Value, IsAdmin);
            return RedirectToAction("Gallery", "Admin");
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase myFile = Request.Files[0];
            bool isok = false;
            string message = "File upload failed";
            string thumurl = "";
            string pvresult = null;
            File_t file = null;
            if (myFile != null && myFile.ContentLength != 0)
            {
                try
                {
                    _ias.Initialize(ServerPath);
                    file = _ias.UploadImage(CurrentUserID.Value, SessionData.Current.AlbumId.Value, myFile.FileName, myFile.InputStream);
                    isok = true;
                    message = "File uploaded successfully!";
                    pvresult = RenderPartialViewToString("_ContentContainerPartial", file);
                }
                catch (Exception ex)
                {
                    message = string.Format("File upload failed: {0}", ex.Message);
                }
            }
       

            return Json(new { isok = isok, message = message, imageid = file != null ? file.Id : 0, thumurl = thumurl, content = pvresult }, "text/html");
        }


        [HttpGet]
        public ActionResult RemoveGallery(int albumId)
        {
            _ias.Initialize(ServerPath);
            _ias.DeleteAlbumById(albumId, CurrentUserID.Value, IsAdmin);
            return RedirectToAction("Gallery", "Admin");
        }


        private string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }




    }
}
