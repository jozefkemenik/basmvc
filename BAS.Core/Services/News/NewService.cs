
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
    public class NewService : INewService
    {
    
        readonly Entities _entities;
        readonly IRepository<New_t> _nr;
        public NewService(IRepository<New_t> nr,  Entities entities)
        {
            _entities = entities;
            _nr = nr;
        }

        public ICollection<New_t> GetAll()
        {
            return this._nr.GetAll().OrderByDescending(e => e.IssueDate).ToList();
        }

        public New_t GetById(int id)
        {
            return this._nr.GetById(id);
        }

        public int AddOrUpdate(New_t n, int userId)
        {
            New_t tmpnr = null;
            if (n.Id > 0)
            {
                tmpnr = _nr.GetById(n.Id);
            }

            if (tmpnr == null)
            {
                tmpnr = new New_t();
            }

            tmpnr.IssueDate = n.IssueDate;
            tmpnr.Text = n.Text;
            tmpnr.Title = n.Title;
            tmpnr.UserId = userId;
            tmpnr.AlbumId = n.AlbumId;
            tmpnr.Location = n.Location;

            if (tmpnr.Id == 0)
            {
                tmpnr.CreatedDate = DateTime.Now;
                this._nr.Add(tmpnr);
            }
            this._nr.SaveAll();
            return tmpnr.Id;
        }

        public void DeleteById(int id)
        {
            var o = this._nr.GetById(id);
            if (o != null)
            {
                this._nr.Delete(o);
                this._nr.SaveAll();
            }            
        }


    }
}
