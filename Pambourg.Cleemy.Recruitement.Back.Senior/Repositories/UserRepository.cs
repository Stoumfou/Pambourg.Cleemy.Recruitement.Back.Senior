using Microsoft.EntityFrameworkCore;
using Pambourg.Cleemy.Recruitement.Back.Senior.Data;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CleemyContext _cleemyContext;

        public UserRepository(CleemyContext cleemyContext)
        {
            _cleemyContext = cleemyContext;
        }

        public async Task<User> FindAsyncById(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _cleemyContext.Users
                .Include(u => u.Currency)
                .Include(u => u.Expenses)
                .Where(u => u.ID == userId)
                .FirstOrDefaultAsync();
        }
    }
}
