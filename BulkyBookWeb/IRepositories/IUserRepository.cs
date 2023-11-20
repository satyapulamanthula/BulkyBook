using BulkyBookWeb.Models;

namespace BulkyBookWeb.IRepositories
{
    public interface IUserRepository
    {
        User GetAllusers(User user);
        void AddUser(User user);
    }

}
