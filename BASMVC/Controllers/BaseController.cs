
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using WebMatrix.WebData;


using BAS.Core.Session.SessionObjects;
using System.Web.Security;

namespace BASMVC.Controllers
{
    public class BaseController : Controller
    {

     


        public BaseController()
        {
 
        }

        #region Properties
        /// <summary>
        /// Current culture
        /// </summary>
        //public string CurrentCultureShortCut
        //{
        //    get
        //    {
        //        return CultureHelper.CurrentCultureShortCut;
        //    }
        //}

        /// <summary>
        /// Current culture id
        /// </summary>
        //public int CurrentCultureId
        //{
        //    get
        //    {
        //        return CultureHelper.CurrentCultureId;
        //    }
        //}

        /// <summary>
        /// Current search configuration in session object for easy acces in controllers
        /// </summary>
        //public SearchCriteria CurrentSearch
        //{
        //    get
        //    {
        //        return SearchCriteria.Current;
        //    }
        //}

        #endregion

        #region Action Result
        //public virtual string Translate(string resourceKey)
        //{
        //    return DatabaseResourceManager.Instance.GetString(resourceKey) ?? resourceKey;
        //}

        public int? CurrentUserID
        {
            get
            {

                if (User != null && User.Identity.IsAuthenticated)
                {                
                    if (!CommonData.Current.UserId.HasValue)
                    {
                        CommonData.Current.UserId= WebSecurity.CurrentUserId;
                    }
                    return CommonData.Current.UserId.Value;
                }
                CommonData.Clear();
                return null;

            }
        }

       
        public string UserName
        {
            get
            {
                if (User != null && User.Identity.IsAuthenticated)
                {
                    return User.Identity.Name;
                }
                return null;
            }
        }


        public bool IsAdmin { get { return Roles.IsUserInRole(UserName, "Administrator"); } }

        
        public String ServerUploadFolderPath
        { 
            get
            {
                if (String.IsNullOrEmpty(CommonData.Current.ServerUploadFolderPath))
                {
                    CommonData.Current.ServerUploadFolderPath = Server.MapPath("~/Uploads/Albums");
                }
                return CommonData.Current.ServerUploadFolderPath;
            }
        }

        public String ServerPath
        {
            get
            {
                if (String.IsNullOrEmpty(CommonData.Current.ServerPath))
                {
                    CommonData.Current.ServerPath = Server.MapPath("~/");
                }
                return CommonData.Current.ServerPath;
            }
        }

       // public ActionResult Languages()
        //{
        //    var m = LanguageService.GetAllLanguages(true);
        //    return View(m);
        //}
        //public ActionResult SetCulture(string culture)
        //{


        //     UriBuilder uriBuilder = new UriBuilder(Request.UrlReferrer);
        //    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        //    query.Add("changelang", culture);
        //    uriBuilder.Query = query.ToString();

        //    return new RedirectResult(uriBuilder.Uri.PathAndQuery);











        //    //culture = CultureHelper.GetImplementedCulture(culture);
        //    //// Save culture in a cookie
        //    //string url = Request.UrlReferrer.AbsoluteUri""
        //    // var routeValues = Request.RequestContext.RouteData.Values;
        //    // routeValues["culture"] = culture;

        //    //Request.UrlReferrer.
        //    //return Redirect(new UrlHelper(Request.RequestContext).RouteUrl(routeValues));
        //}

        //public ActionResult ChangeLang(string lang, string returnUrl)
        //{
        //    CultureCookies.CultureShortCut = lang;
        //    string url = Request.UrlReferrer.AbsoluteUri;
        //    return Redirect(HttpUtility.UrlDecode(url));
        //}

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            return base.BeginExecuteCore(callback, state);
        } 
        #endregion
    }
}