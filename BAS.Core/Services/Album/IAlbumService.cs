
using BAS.Repository.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Core.Services
{
    public interface IAlbumService
    {
        void Initialize(string serverPath);


        void UpdateFilesInAlbum(ICollection<File_t> files, int albumId, int userId, bool isAdmin);
        //int CreateAlbum(int advertId, int userId, string serverDefaultPath);

        File_t UploadImage(int userId, int albumId, string fileName, Stream s);
        //Album_t GetAlbumByAdvId(int advertId);
        void DeleteFileById(int id, int userId, bool doSave = true, bool isAdmin = false);


        ICollection<int> GetNoAsignedAlbumsIds(int userId);

        ICollection<Album_t> GetAlbums(int? userId=null);

        Album_t GetEventAlbum();
        Album_t GetAlbumById(int id, bool checkAllFiles = true);
        int CreateAlbum(int userId);
        void DeleteAlbumById(int id, int userId, bool isAdmin = false);


        //void SavePhotoDescrition(ICollection<File_t> files, int userId);
    }
}
