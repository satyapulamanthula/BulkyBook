using BulkyBookWeb.Data;
using BulkyBookWeb.IRepositories;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetAllusers(User user) {
            // Check if the user exists in the database
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            return existingUser!;
            
        }
    }

}
