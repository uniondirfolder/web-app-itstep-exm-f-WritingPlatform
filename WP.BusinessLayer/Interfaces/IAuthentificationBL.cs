
namespace WP.BusinessLayer.Interfaces
{
    public interface IAuthentificationBL
    {
        bool CheckLogin(string login, string password);
        int GetUserId(string login, string password);
        bool GetUserStatus(string login, string password);
        bool Logout();
    }
}
