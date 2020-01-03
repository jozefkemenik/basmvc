using BAS.Core.Helper;
using BAS.Core.Services;
using BAS.Core.Session.SessionObjects;
using BAS.Repository.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace BASMVC.Controllers
{
    public class FilesController : BaseController
    {
        IAlbumService _ias;
        public FilesController(IAlbumService ias)
        {
            _ias = ias;
        }

        #region Actions
        public ActionResult GetAlbums()
        {
            ViewBag.ActiveIndex = 4;
            _ias.Initialize(ServerPath);
            var model = _ias.GetAlbums();         
            return PartialView("_AlbumsComponentPartial", model);
        }

        public ActionResult GetAlbumId(int albumId, string ru=null)
        {
            ViewBag.ReturnUrl = ru;
            ViewBag.ActiveIndex = 4;
            _ias.Initialize(ServerPath);
            var model = _ias.GetAlbumById(albumId);
            return View("~/Views/Home/GalleryAlbum.cshtml", model);
        }
        #endregion

        #region Private Methods


        #endregion
    }



}
