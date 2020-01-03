using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BASMVC.Filters;
using BASMVC.Models;
using BAS.Core.Services;
using BAS.Repository.Model;
using BAS.Core.Session.SessionObjects;
using BASMVC.ViewModel;

namespace BASMVC.Controllers
{





    [Authorize(Roles = "Administrator")]
    //[InitializeSimpleMembership]
    public class AdminController : BaseController
    {

        public class SessionData : BAS.Core.SessionContext<SessionData>
        {
            public int? NewId { get; set; }
            public int? EventId { get; set; }

            public ICollection<int?> AlbumIds { get; set; }
        }

        IAlbumService _ias;
        IEventService _ies;
        INewService _ins;
        IUserProfileService _iups;
        public AdminController(IEventService ies, INewService ins, IAlbumService ias, IUserProfileService iups)
        {
            _ies = ies;
            _ins = ins;
            _ias = ias;
            _iups = iups;
        }

        public ActionResult Index()
        {
            ViewBag.ActiveIndex = 0;
            return View();
        }

        #region Events
        [HttpGet]
        public ActionResult Events()
        {
            ViewBag.ActiveIndex = 1;
            var res = _ies.GetAll();
            return View(res);
        }

        [HttpGet]
        public ActionResult EventDetail()
        {
            ViewBag.ActiveIndex = 1;
            if (SessionData.Current.EventId.HasValue)
            {
                var res = _ies.GetById(SessionData.Current.EventId.Value);
                return View(res);
            }
            else
            {
                return View(new Event_t() {
                    IssueDate = DateTime.Now
                });
            }
        }

        [HttpGet]
        public ActionResult ShowEventDetail(int? id = null)
        {
            SessionData.Current.EventId= id;
            return RedirectToAction("EventDetail");
        }

        [HttpGet]
        public ActionResult DeleteEvent(int id)
        {
            SessionData.Current.EventId = null;
            _ies.DeleteById(id);
            return RedirectToAction("Events");
        }

        [HttpPost]
        public ActionResult EventDetail(Event_t e)
        {
            e.UpdateIssueDateByTime();
            if (SessionData.Current.EventId.HasValue)
                e.Id = SessionData.Current.EventId.Value;
            if (ModelState.IsValid)
            {
                if (CurrentUserID.HasValue)
                {
                    _ies.AddOrUpdate(e, CommonData.Current.UserId.Value);
                }
                else
                    throw new Exception("User must be loged in");

                return RedirectToAction("Events");
            }
            else
            {

                return View(e);
            }
        }


        #endregion

        #region News
        [HttpGet]
        public ActionResult News()
        {
            ViewBag.ActiveIndex = 2;
            var res = _ins.GetAll();
            return View(res);
        }

        [HttpGet]
        public ActionResult NewDetail()
        {
            ViewBag.ActiveIndex = 2;
            List<int?> albumIds = new List<int?>();
            albumIds.Add(null);
            var list =  _ias.GetNoAsignedAlbumsIds(CurrentUserID.Value);
            foreach (var item in list)
            {
                albumIds.Add(item);
            }
            if (SessionData.Current.NewId.HasValue)
            {
                var res = _ins.GetById(SessionData.Current.NewId.Value);
                if (res.AlbumId!=null)
                    albumIds.Insert(0,res.AlbumId);
                res.AlbumIds = albumIds;

                SessionData.Current.AlbumIds = albumIds;

                return View(res);
            }
            else
            {
                SessionData.Current.AlbumIds = albumIds;
                return View(new New_t() {
                    AlbumIds = albumIds,
                    IssueDate = DateTime.Now
            });
            }
        }

        [HttpGet]
        public ActionResult ShowNewDetail(int? id=null)
        {
            SessionData.Current.NewId = id;
            return RedirectToAction("NewDetail");
        }


        [HttpGet]
        public ActionResult DeleteNew(int id)
        {
            SessionData.Current.NewId = null;
            _ins.DeleteById(id);
            return RedirectToAction("News");
        }


        [HttpPost]
        public ActionResult NewDetail(New_t n)
        {
            n.UpdateIssueDateByTime();
            if (SessionData.Current.NewId.HasValue)
                n.Id = SessionData.Current.NewId.Value;
            if (ModelState.IsValid)
            {
                if (CurrentUserID.HasValue)
                {
                    _ins.AddOrUpdate(n, CommonData.Current.UserId.Value);
                }
                else
                    throw new Exception("User must be loged in");

                return RedirectToAction("News");
            }
            else
            {
                n.AlbumIds = SessionData.Current.AlbumIds;
                return View(n);
            }

        }
        #endregion

        public ActionResult Gallery()
        {
            ViewBag.ActiveIndex = 3;
            return View();
        }

        [HttpGet]
        public ActionResult CreateAlbum()
        {
            _ias.CreateAlbum(CurrentUserID.Value);
            return RedirectToAction("Gallery");
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Users()
        {
            ViewBag.ActiveIndex = 4;
            var model = _iups.GetAll(CurrentUserID.Value);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult SetUserToRoleAdmin(int id)
        {
            _iups.SetAdminRole(id);
            return RedirectToAction("Users");
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult SetUserToRoleUser(int id)
        {
            _iups.SetUserRole(id);
            return RedirectToAction("Users");
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            _iups.DeleteUser(id);
            return RedirectToAction("Users");
        }





    }
}
