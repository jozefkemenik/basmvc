
using BAS.Repository;
using BAS.Repository.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        readonly IRepository<UserProfile> _uprep;
        readonly IRepository<webpages_Roles> _wr;
        readonly IRepository<webpages_Membership> _wm;
        readonly Entities _entities;
        public UserProfileService(IRepository<UserProfile> uprep, IRepository<webpages_Roles> wr, IRepository<webpages_Membership> wm, Entities entities)
        {
            _wr= wr;
            _wm=wm;
            _uprep = uprep;
            _entities = entities;
        }
        public UserProfile GetProfile(int userId)
        {
            var up = _uprep.GetById(userId);
            if (up!=null)
                up.all_Webpages_Roles = _wr.GetAll().ToList();
            //if (up != null && String.IsNullOrEmpty(up.Email))
            //{
            //    up.Email = up.UserName;
            //}
            return up;
        }
        public void SaveProfile(UserProfile up)
        {
            _uprep.Update(up);
            _uprep.SaveAll();
        }
        public bool IsAlreadingExistingName(string username)
        {
            return _uprep.GetAll().Any(c => c.UserName == username);
        }


        public ICollection<UserProfile> GetAll(int userId) {


            var result = _entities.UserProfile.Include("webpages_Roles").ToList();
            var list = _wr.GetAll().ToList();
            foreach (var item in result)
            {
                item.CurrentUserId = userId;
                item.all_Webpages_Roles = list;
            }
            return result;
        }



        public  void SetUserRole(int userId)
        {
            //var query = _entities.UserProfile.Include("webpages_Roles").Where(u => u.UserId == userId);
            SetUserRole(userId, "User");
        }
        public void SetAdminRole(int userId)
        {
            SetUserRole(userId, "Administrator");
        }
        public void DeleteUser(int userId)
        {

            var list=  _wr.GetAll().ToList();
            {
                foreach (var item in list)
                {
                     var removeList = item.UserProfile.Where(u => u.UserId == userId).ToList();
                    if (removeList.Any())
                    {
                        foreach (var r in removeList)
                        {
                            item.UserProfile.Remove(r);
                        }
                    }
                }

                _wr.SaveAll();

            }

            var ms = _wm.GetById(userId);
            if (ms != null)
            {
                _wm.Delete(ms);
                _wm.SaveAll();
            }

            var up = _uprep.GetById(userId);
            if (up != null)
            {
                _uprep.Delete(up);
                _uprep.SaveAll();
            }
        }
        public void UpdateUser(UserProfile up)
        {
 
        }




        private void SetUserRole(int userId, string rolename)
        {

            var queryUp = _entities.UserProfile.Include("webpages_Roles").Where(u => u.UserId == userId);
            var queryRole = _entities.webpages_Roles.Include("UserProfile").Where(r => r.RoleName == rolename);
            if (queryUp.Any() && queryRole.Any())
            {
                var up = queryUp.First();
                var role = queryRole.First();

                up.webpages_Roles.Clear();
                up.webpages_Roles.Add(role);
                 _entities.SaveChanges();
                   
            }
        }

    }
}
