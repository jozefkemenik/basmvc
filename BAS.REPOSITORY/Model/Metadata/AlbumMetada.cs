using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.Model
{

    [MetadataType(typeof(AlbumMetadata))]
    public partial class Album_t
    {
        
        public string AlbumName
        {
            get
            {
                return "Album" + Id;
            }
        }

        public string Name { get; set; }

    }

    public class AlbumMetadata 
    {

        
    }
    
        
}
