
using BAS.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Core.Services
{
    public interface IUserProfileService
    {

        ICollection<UserProfile> GetAll(int userId);
        UserProfile GetProfile(int userId);
        void SaveProfile(UserProfile up);
        bool IsAlreadingExistingName(string username);

        void SetUserRole(int userId);
        void SetAdminRole(int userId);
        void DeleteUser(int userId);
        void UpdateUser(UserProfile up);
    }
}
