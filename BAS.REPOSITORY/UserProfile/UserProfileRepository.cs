using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.UserProfile
{
    public class UserProfileRepository : Repository<Model.UserProfile>, IUserProfileRepository
    {

        public UserProfileRepository(Model.Entities context) : base(context) { }
 

        public Model.UserProfile GetUserProfileByUserId(int userId)
        {
            return DbContext.UserProfile.Where(r=>r.UserId==userId).FirstOrDefault();
        }


        public bool ExistByName(string name)
        {
            return DbContext.UserProfile.Any(r => r.UserName==name);
        }
      

    }
}
