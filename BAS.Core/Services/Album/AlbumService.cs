
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;
using BAS.Repository;
using BAS.Repository.Model;
using BAS.Core.Helper;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.ObjectModel;

namespace BAS.Core.Services
{
    public class AlbumService : IAlbumService
    {
        readonly IRepository<Album_t> _ar;
        readonly IRepository<File_t> _fr;
        readonly IRepository<New_t> _nr;
        readonly Entities _entities;
        string _serverPath;
        public AlbumService(IRepository<Album_t> ar, IRepository<File_t> fr, IRepository<New_t> nr, Entities entities)
        {
            _ar = ar;
            _fr = fr;
            _nr = nr;
            _entities = entities;

        }

        public void Initialize(string serverPath)
        {
            _serverPath = serverPath;
        }



        public Album_t GetEventAlbum()
        {
            var query = _ar.GetAll().Where(a => a.AlbumTypeId == 1);
            if (query.Any())
            {
                return GetAlbumById(query.First().Id);
            }
            return null;
        }

        public Album_t GetAlbumById(int id, bool checkAllFiles= true)
        {
            var queryAlbum = _ar.GetById(id);


            var listFiles = (from f in _entities.File_t
                             where f.Album_Id == id
                             select new
                             {
                                 Album_Id = f.Album_Id,
                                 CreatedDate = f.CreatedDate,
                                 IsOnTopAlbum = f.IsOnTopAlbum,
                                 Id = f.Id,
                                 Sort = f.Sort,
                                 FileName = f.FileName,
                                 Title = f.Title,
                                 Description = f.Description
                             }).ToList();


            //Where cust.Orders.Count > 0 And
            //      cust.CompanyName.StartsWith("H") 
            //Select cust.CustomerID, cust.CompanyName, 
            //       OrderCount = cust.Orders.Count, 
            //       cust.Country



            var album = new Album_t() { Id = id };
            var list = listFiles.Select(f => new File_t()
            {
                Album_Id = f.Album_Id,
                CreatedDate = f.CreatedDate,
                IsOnTopAlbum = f.IsOnTopAlbum,
                Id = f.Id,
                Sort = f.Sort,
                FileName = f.FileName,
                Title = f.Title,
                Description = f.Description
            }).ToList();

            if (queryAlbum != null)
            {
                var o = _nr.GetAll().Where(n => n.AlbumId == queryAlbum.Id);

                album.Id = queryAlbum.Id;
                album.CreatedDate = queryAlbum.CreatedDate;
                album.File_t = list;
                album.Name = o.Any() ? o.First().Title : "";
                album.AlbumTypeId = queryAlbum.AlbumTypeId;
            }
      

            List<File_t> tmplist = null;
            if (checkAllFiles) 
                tmplist = album.File_t.OrderBy(f => f.Sort).ToList();
            else
                tmplist = album.File_t.Where(f=>f.IsOnTopAlbum).ToList();
            foreach (var item in tmplist)
            {
                item.InitialisePaths(_serverPath);
                if (!PictureHelper.CheckFiles(item))
                {
                    PictureHelper.SaveImageToDirectory(_fr.GetById(item.Id).Image, item);
                }
            }
            return album;
        }

        public ICollection<Album_t> GetAlbums(int? userId = null)
        {

            var listAlbumIdsInfNews = _nr.GetAll().Select(n => n.AlbumId).ToList(); 


            var result = new Collection<Album_t>();
            var albums = _ar.GetAll().Where(a=>a.AlbumTypeId==2).OrderByDescending(a => a.CreatedDate).ToList();
            foreach (var album in albums)
            {

                // skip album which is not of event
                if (userId == null && !listAlbumIdsInfNews.Contains(album.Id))
                {
                    continue;
                }
                else
                {
                    var queryNew = _nr.GetAll().Where(n => n.AlbumId == album.Id);
                    if (queryNew.Any())
                    {
                        var o = queryNew.First();
                        album.Name = o.Title; 
                    }
                }


                var itemResult = new Album_t()
                {
                    Id = album.Id,
                    CreatedDate = album.CreatedDate,
    
                    Name = album.Name
                };

                var query = (from f in _entities.File_t
                             where f.Album_Id.Value == album.Id && f.IsOnTopAlbum
                             select new
                             {
                                 Album_Id = f.Album_Id,
                                 CreatedDate = f.CreatedDate,
                                 IsOnTopAlbum = f.IsOnTopAlbum,
                                 Id = f.Id,
                                 Sort = f.Sort,
                                 FileName = f.FileName,
                                 Title = f.Title,
                                 Description = f.Description
                             })
                                .ToList()
                                .Select(f => new File_t()
                                {
                                    Album_Id = f.Album_Id,
                                    CreatedDate = f.CreatedDate,
                                    IsOnTopAlbum = f.IsOnTopAlbum,
                                    Id = f.Id,
                                    Sort = f.Sort,
                                    FileName = f.FileName,
                                    Title = f.Title,
                                    Description = f.Description
                                }).ToList();


            

                if (query.Any())
                {
                    itemResult.File_t = new Collection<File_t>() { query.First() };
                }
                else
                {
                    itemResult.File_t = new Collection<File_t>();
                }

                    result.Add(itemResult);
            }
            // save to disk
            foreach (var item in result)
            {
                if (item.File_t.Any())
                {
                    var file = item.File_t.FirstOrDefault();
                    file.InitialisePaths(_serverPath);
                    if (!PictureHelper.CheckFiles(file))
                    {
                        PictureHelper.SaveImageToDirectory(_fr.GetById(file.Id).Image, file);
                    }
                }
            }
            return result;
        }


        public ICollection<int> GetNoAsignedAlbumsIds(int userId)
        {
            var query = _ar.GetAll().Where(a => a.AlbumTypeId == 2); ;

            var listAlbumIdsInfNews = _nr.GetAll().Select(n => n.AlbumId).ToList();
            return query.Where(a => !listAlbumIdsInfNews.Contains(a.Id)).Select(a=>a.Id).ToList();
        }

        public void DeleteFileById(int id, int userId, bool doSave = true, bool isAdmin = false)
        {
            var file = _fr.GetById(id);
            if (file == null || (!isAdmin && file.UserId != userId))
            {
                throw new Exception("Image does not exists per user");
            }
            file.InitialisePaths(_serverPath);

            if (File.Exists(file.NEWTHUM_FILE))
                File.Delete(file.NEWTHUM_FILE);
            if (File.Exists(file.STAND_FILE))
                File.Delete(file.STAND_FILE);
            if (File.Exists(file.THUM_FILE))
                File.Delete(file.THUM_FILE);
            _fr.Delete(file);
            if (doSave)
                _fr.SaveAll();
        }

        public void DeleteAlbumById(int id, int userId, bool isAdmin=false )
        {
            var queryAlbum = _ar.GetById(id);
            if (queryAlbum == null || (!isAdmin && queryAlbum.UserId != userId))
            {
                throw new Exception("Album does not exists per user");
            }
            foreach (var file in queryAlbum.File_t)
            {
                DeleteFileById(file.Id, userId, false, isAdmin);
            }

            var queryNew = _nr.GetAll().Where(n => n.AlbumId == id);
            if (queryNew.Any())
            {
                _nr.Delete(queryNew.First());
            }
            _ar.Delete(queryAlbum);
            _ar.SaveAll();
        }



        public int CreateAlbum(int userId)
        {
            var album = new Album_t()
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                AlbumTypeId=2
            };

            _ar.Add(album);
            _ar.SaveAll();
            return album.Id;
        }


        public File_t UploadImage(int userId, int albumId, string fileName, Stream s)
        {
            File_t file = null;
            using (MemoryStream ms = new MemoryStream())
            {
                s.CopyTo(ms);
                s.Position = 0;
                var fileId = SaveFileInAlbum(userId, albumId, fileName, ms.ToArray());
                if (fileId > 0)
                {
                    file = _fr.GetById(fileId);
                    file.InitialisePaths(_serverPath);
                    PictureHelper.SaveImageToDirectory(s, file);
                }
                s.Close();
            }
            return file;
        }

        public void UpdateFilesInAlbum(ICollection<File_t> files, int albumId, int userId, bool isAdmin)
        {
            var fileIds = (from f in _entities.File_t
                           where f.Album_Id == albumId
                           select f.Id).ToList();

            var fileIdsToRemove = fileIds.Where(i => files == null || !files.Select(f => f.Id).Contains(i)).ToList();
            foreach (var item in fileIdsToRemove)
            {
                DeleteFileById(item, userId, true, isAdmin);
            }

            if (files == null)
                return;
            var sortIndex = 0;
            foreach (var item in files)
            {
                var f = _fr.GetById(item.Id);
                f.Sort = ++sortIndex;
                f.IsOnTopAlbum = item.IsOnTopAlbum;
                f.Description = item.Description;
                _fr.SaveAll();
            }
        }

        private int SaveFileInAlbum(int userId, int albumId, string fileName, byte[] image)
        {
            var result = -1;
            result = (int)_entities.InsertFile_sp(fileName, "", "", false, false, albumId, userId, 0, image).First().Value;
            if (result < 1)
            {
                throw new Exception("Problem in uploading strem");
            }
            return result;
        }


        public void SavePhotoDescrition(ICollection<File_t> files, int userId)
        {
            var sortindex = 0;
            foreach (var item in files)
            {
                var file = _fr.GetById(item.Id);
                if (file != null && file.UserId == userId)
                {
                    file.IsOnTopAlbum = item.IsOnTopAlbum;
                    file.Description = item.Description;
                    file.Sort = sortindex++;
                }
            }
            _fr.SaveAll();
        }
        #region Private Methods
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
        #endregion
    }
}
