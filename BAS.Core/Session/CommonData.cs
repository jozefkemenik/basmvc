using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Core.Session.SessionObjects
{
    public class CommonData : SessionContext<CommonData>
    {
        public CommonData() { }
        public string ServerUploadFolderPath { get; set; }
        public string ServerPath { get; set; }

        public int? UserId {get; set; }

        public string UserName { get; set; }
    }
}