using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.Model
{
    [MetadataType(typeof(FileMetadata))]
    public partial class File_t
    {
        private string serverPath= ""; 

        public void InitialisePaths(string sp)
        {
            serverPath =sp;
        }

        //public string ORG_FILE
        //{
        //    get
        //    {
        //        return  Path.Combine(serverPath, this.OrigPath.Trim('/').Replace("/", "\\"));

        //    }
        //}



        public string NEWTHUM_FILE
        {
            get
            {
                return Path.Combine(serverPath, this.NewThumPath.Trim('/').Replace("/", "\\"));

            }
        }

        public string THUM_FILE
        {
            get
            {
                return  Path.Combine(serverPath, this.ThumPath.Trim('/').Replace("/", "\\"));
            }
        }

        public string STAND_FILE
        {
            get 
            {
                return  Path.Combine(serverPath, this.StandPath.Trim('/').Replace("/", "\\"));
            }
        }

        public string ThumPath { get { return "/Uploads/Albums/Album" + Album_Id + "/thum_album" + Album_Id + "_file" + Id + ".jpg"; } }
        public string StandPath { get { return "/Uploads/Albums/Album" + Album_Id + "/stand_album" + Album_Id + "_file" + Id + ".jpg"; } }
        public string OrigPath { get { return "/Uploads/Albums/Album" + Album_Id + "/or_album" + Album_Id + "_file" + Id + ".jpg"; } }
        public string NewThumPath { get { return "/Uploads/Albums/Album" + Album_Id + "/newthum_album" + Album_Id + "_file" + Id + ".jpg"; } }
    }

    public class FileMetadata
    {


    }
}
