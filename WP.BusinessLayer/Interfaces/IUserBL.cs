
using System.Collections.Generic;
using WP.BusinessLayer.ObjectValues;

namespace WP.BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        void Create(UserBL user);
        void Update(UserBL user);
        UserBL GetUser(int id);
        IEnumerable<UserBL> GetUsers();
        void DeleteUser(int id);
        void Dispose();
    }
}
