using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BAS.Core
{
    public class SessionContext<T>
        where T : SessionContext<T>, new()
    {
        private static string Key
        {
            get { return typeof(SessionContext<T>).FullName; }
        }
       private static T Value
        {
            get { return (T)HttpContext.Current.Session[Key]; }
            set { HttpContext.Current.Session[Key] = value; }
        }
       public static T Current
        {
            get
            {
                var instance = Value;
                if (instance == null)
                    lock (typeof(T))
                    {
                        instance = Value;
                        if (instance == null)
                            Value = instance = new T();
                    }
                return instance;
            }
        }
       public static void Clear()
        {
            HttpContext.Current.Session.Remove(Key);
        }
   }
}
