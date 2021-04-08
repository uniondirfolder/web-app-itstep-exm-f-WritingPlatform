

using System.Linq;
using WP.BusinessLayer.Interfaces;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public class UserAuthenticationBL: IAuthentificationBL
    {
        private IUnitOfWork _db { get; set; }

        public UserAuthenticationBL(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        private bool IsCorrectLoginPassword(string login, string password) 
        {
            return _db.UowRepositoryUsers.GetAll().SingleOrDefault(q => (q.Login == login) && (q.Password == password)) != null;
        }
        public bool CheckLogin(string login, string password)
        {
            return IsCorrectLoginPassword(login, password);
        }

        public int GetUserId(string login, string password)
        {
            if (IsCorrectLoginPassword(login, password)) 
            {
               return _db.UowRepositoryUsers.GetAll().SingleOrDefault(q => (q.Login == login) && (q.Password == password)).Id;
            }

            return 0;
        }

        public bool GetUserStatus(string login, string password)
        {
            return _db.UowRepositoryUsers.GetAll().SingleOrDefault(q => (q.Login == login) && (q.Password == password)).IsDelete;
        }

        public bool Logout()
        {
            return true;
        }
    }
}
