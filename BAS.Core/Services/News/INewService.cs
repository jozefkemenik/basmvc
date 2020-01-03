
using BAS.Repository.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Core.Services
{
    public interface INewService
    {
        ICollection<New_t> GetAll();
        New_t GetById(int id);

        void DeleteById(int id);

        int AddOrUpdate(New_t n, int userId);
    }
}
