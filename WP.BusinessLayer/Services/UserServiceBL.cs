using System.Collections.Generic;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.DataLayer.Entities;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public class UserServiceBL : ABaseServiceBL, IUserBL
    {
        public UserServiceBL(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool Create(UserBL user)
        {
            if (null == user) return false;
            if (null != Dbcontext.UowRepositoryUsers.Find(q => q.Login == user.Login)) return false;

            var item = new User() { Login = user.Login, Email = user.Email, Password = user.Password };
            Dbcontext.UowRepositoryUsers.Create(item);
            try
            {
                Dbcontext.Save();
            }
            catch (System.Exception)
            {

                return false;
            }
            
            return true;
        }

        public void DeleteUser(int id)
        {
            if (0 >= id) return;

            Dbcontext.UowRepositoryUsers.Delete(id);
            Dbcontext.Save();
        }

        public void Dispose()
        {
            Dbcontext.Dispose();
        }

        public UserBL GetUser(int id)
        {
            if (0 >= id) return new UserBL();

            return AutoMapperBL<User, UserBL>.Map(Dbcontext.UowRepositoryUsers.Get(id));
        }
        public IEnumerable<UserBL> GetUsers()
        {
            return AutoMapperBL<IEnumerable<User>, List<UserBL>>.Map(Dbcontext.UowRepositoryUsers.GetAll);            
        }

        public void Update(UserBL user)
        {
            if (null == user) return;
            throw new System.NotImplementedException();
        }
    }
}
