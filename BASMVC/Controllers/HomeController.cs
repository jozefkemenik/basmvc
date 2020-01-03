using BAS.Core.Services;
using BAS.Repository.Model;
using BASMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASMVC.Controllers
{
    public class HomeController : BaseController
    {

        IAlbumService _ias;
        INewService _ns;
        IEventService _es;
        public HomeController(IAlbumService ias, INewService ns, IEventService es)
        {
            _ias = ias;
            _ns = ns;
            _es = es;
        }

        public ActionResult Index()
        {
            ViewBag.ActiveIndex = 0;
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var model = GetIndexVm(4, 9);
            return View(model);
        }

        public ActionResult About()
        {
            
            return View();
        }

       


        public ActionResult Gallery()
        {
            ViewBag.ActiveIndex = 4;
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Events()
        {
            ViewBag.ActiveIndex = 2;
            var model = GetIndexVm(int.MaxValue,9);
            return View(model);
        }

        [HttpGet]
        public ActionResult News()
        {
            ViewBag.ActiveIndex = 3;
            var model = GetIndexVm(9, int.MaxValue);
            return View(model);
        }

        [HttpGet]
        public ActionResult NewDetail(int id)
        {
            ViewBag.ActiveIndex = 3;
            var item = _ns.GetById(id);
            IndexVM ivm = new IndexVM();
            if (item != null)
            {
                _ias.Initialize(ServerPath);
                File_t file = null;
                if (item.AlbumId.HasValue)
                {
                    file = _ias.GetAlbumById(item.AlbumId.Value, false).File_t.Where(f => f.IsOnTopAlbum).FirstOrDefault();
                }

                ivm.AlbumId = item.AlbumId;
                ivm.Id = item.Id;
                ivm.IssueDate = item.IssueDate;
                ivm.FilePath = file != null ? file.ThumPath : "null";
                ivm.Text = item.Text;
                ivm.Title = item.Title;
                ivm.Location = item.Location;

            }
            return View(ivm);
        }


        [HttpGet]
        public ActionResult EventDetail(int id)
        {
            ViewBag.ActiveIndex = 4;
            var item = _es.GetById(id);
            IndexVM ivm = new IndexVM();
            if (item != null)
            {
                var album = _ias.GetEventAlbum();
                File_t file = null;
                if (item.FileId.HasValue)
                {
                    file = album.File_t.Where(f => f.Id == item.FileId.Value).FirstOrDefault();
                }
            
                ivm.Id = item.Id;
                ivm.IssueDate = item.IssueDate;
                ivm.FilePath = file != null ? file.THUM_FILE : "null";
                ivm.Text = item.Text;
                ivm.Title = item.Title;
                ivm.Location = item.Location;

            }
            return View(ivm);
        }
        [HttpGet]
        public ActionResult GetFile(int id) {
            var contentType = "";
            var extension = ".docx";
            var fileName = "vyberove_konanie.docx";
            if (extension.ToLower() == ".docx")
            { // Handle *.jpg and   
                contentType = "application/docx";
            }
            else if (extension.ToLower() == ".jpg")
            {// Handle *.gif   
                contentType = "image/jpeg jpeg jpg jpe";
            }
            else if (extension.ToLower() == ".pdf")
            {// Handle *.pdf   
                contentType = "application/pdf";
            }
            Response.ContentType = contentType; // "application/force-download";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

            // returning the file for download as FileResult
            // third input parameter is optional
            return File(Server.MapPath("~/App_Data/Doc")+"/"+ fileName, contentType, Server.UrlEncode(fileName));
        
        }


        
        




        private IndexVM GetIndexVm(int eventCount = int.MaxValue, int newCount = int.MaxValue)
        {
            IndexVM ivm = new IndexVM();
            _ias.Initialize(ServerPath);

            //news
            var news = _ns.GetAll().Take(newCount);
            foreach (var item in news)
            {
                File_t file = null;
                if (item.AlbumId.HasValue)
                {
                    file = _ias.GetAlbumById(item.AlbumId.Value,false).File_t.Where(f => f.IsOnTopAlbum).FirstOrDefault();
                }
                ivm.NewVMCollection.Add(

                                new IndexVM()
                                {
                                    AlbumId = item.AlbumId,
                                    Id = item.Id,
                                    IssueDate = item.IssueDate,
                                    FilePath = file != null ? file.NewThumPath : "null",
                                    Text = item.Text,
                                    Title = item.Title,
                                    Location = item.Location
                                });
            }
            //events
            var events = _es.GetAll().Where(e=>e.IssueDate>=DateTime.Now).Take(eventCount);
            var album = _ias.GetEventAlbum();
            foreach (var item in events)
            {
                File_t file = null;
                if (item.FileId.HasValue)
                {
                    file = album.File_t.Where(f => f.Id == item.FileId.Value).FirstOrDefault();
                }
                ivm.EventVMCollection.Add(new IndexVM()
                {
                    Id = item.Id,
                    IssueDate = item.IssueDate,
                    FilePath = file != null ? file.NEWTHUM_FILE : "null",
                    Text = item.Text,
                    Title = item.Title,
                    Location = item.Location
                });
            }
            return ivm;
        }
    }
}
