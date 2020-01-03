
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
    public class EventService : IEventService
    {
        IRepository<Event_t> _er;
        readonly Entities entities;
        public EventService(IRepository<Event_t> er, Entities entities)
        {
            this._er = er;
            this.entities = entities;
        }


        public ICollection<Event_t> GetAll()
        {
            return this._er.GetAll().OrderByDescending(e=>e.IssueDate).ToList();
        }

        public Event_t GetById(int id)
        {
            return this._er.GetById(id);
        }

        public int AddOrUpdate(Event_t ev, int userId)
        {
            Event_t tmpev = null;
            if (ev.Id > 0)
            {
                tmpev = _er.GetById(ev.Id);
            }

            if (tmpev == null)
            {
                tmpev = new Event_t();
            }

            tmpev.IssueDate = ev.IssueDate;
            tmpev.Text = ev.Text;
            tmpev.Title = ev.Title;
            tmpev.UserId = userId;
            tmpev.Location = ev.Location;

            if (tmpev.Id == 0)
            {
                tmpev.CreatedDate = DateTime.Now;
                this._er.Add(tmpev);
            }
            this._er.SaveAll();
            return tmpev.Id;
        }


        public void DeleteById(int id)
        {
            var o = this._er.GetById(id);
            if (o != null)
            {
                this._er.Delete(o);
                this._er.SaveAll();
            }
        }
    }
}
