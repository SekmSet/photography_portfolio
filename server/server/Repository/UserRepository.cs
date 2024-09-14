using Microsoft.EntityFrameworkCore;
using server.Model;

namespace server.Repository
{
    public class UserRepository
    {
    
        private readonly DatabaseContext _context;
        
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }
    
        public Task<UserModel?> UserByNameOrEmail(string usernameOrEmail)
        {
            return _context.UserModels
                .Where(u => (u.Username == usernameOrEmail || u.EmailAddress == usernameOrEmail))
                .FirstOrDefaultAsync();
        }
    
        public Task<UserModel?> UserByNameAndEmail(string username, string email)
        {
            return _context.UserModels
                .FirstOrDefaultAsync(u => u.Username == username && u.EmailAddress == email);
        }
    }
};

