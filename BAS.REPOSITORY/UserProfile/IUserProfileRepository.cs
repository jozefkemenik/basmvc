using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.UserProfile
{
    public interface IUserProfileRepository : IRepository<Model.UserProfile>
    {
        Model.UserProfile GetUserProfileByUserId(int userId);
        bool ExistByName(string name);
    }
}
