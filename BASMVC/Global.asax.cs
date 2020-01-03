
using Autofac;
using BAS.Core;
using BAS.Core.Infrastucture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace BASMVC
{


    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        static MemoryCacheManager _cacheManager;

        static MvcApplication()
        {
            _cacheManager = new MemoryCacheManager();
        }
        string _logIpFile
        {
            get
            {
                var dtNow = DateTime.Now;
                string dtstring = String.Format("{0}_{1}_{2}",dtNow.Year,dtNow.Month,dtNow.Day);
                return Path.Combine(Server.MapPath("~/"), "Uploads", "IPLog_"+dtstring+".txt");
            }

        }

        static public int CountVisitors
        {
            get
            {
                if (!_cacheManager.IsSet("countVisitors"))
                {
                    var o = new object();
                    lock (o)
                        {
                            _cacheManager.Set("countVisitors", 0, int.MaxValue);
                        }
                    }   
                    return _cacheManager.Get<int>("countVisitors");

            }
            private set
            {
                var o = new object();
                lock(o)
                {
                    if (!_cacheManager.IsSet("countVisitors"))
                        _cacheManager.Set("countVisitors", value, int.MaxValue);
                    else
                      _cacheManager.SetValue<int>("countVisitors", value);
                }
            }
        }
        protected void Application_Start()
        {



            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);


            DependencyConfigure.Initialize(typeof(MvcApplication));
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).PropertiesAutowired();


        }

        protected void Session_Start(object sender, EventArgs e)
        {
            CountNewRequest();
        }





        private void CountNewRequest()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            ipList = (string.IsNullOrEmpty(ipList)) ? Request.ServerVariables["REMOTE_ADDR"] : ipList.Split(',')[0];
            var o = new
            {
                Date = DateTime.Now,
                IP = ipList,
                Browser = Request.UserAgent
            };

            // Check if file already exists. If yes, delete it. 
            System.IO.File.AppendAllText(_logIpFile, @o.Date + ";" + o.IP + ";" + o.Browser+"\r\n");
            if (!File.Exists(_logIpFile))
            {
                CountVisitors = 1;
            }
            else
            {
                if (CountVisitors == 0)
                {

                    using (StreamReader r = new StreamReader(_logIpFile))
                    {
                        int i = 0;
                        while (r.ReadLine() != null)
                        {
                            i++;
                        }
                        CountVisitors = i;
                    }
                }
                CountVisitors= CountVisitors+1;



            }





        }

    }

}