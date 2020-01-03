
using BAS.Repository.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Core.Services
{
    public interface IEventService
    {
        ICollection<Event_t> GetAll();
        Event_t GetById(int id);

        void DeleteById(int id);
        int AddOrUpdate(Event_t ev, int userId);

    }
}
