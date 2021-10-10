using Microsoft.EntityFrameworkCore;
using Pambourg.Cleemy.Recruitement.Back.Senior.Data;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly CleemyContext _cleemyContext;

        public ExpenseRepository(CleemyContext cleemyContext)
        {
            _cleemyContext = cleemyContext;
        }

        public async Task<IEnumerable<Expense>> FindAsync(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _cleemyContext.Expenses
                .Include(e => e.User)
                .Include(e => e.Type)
                .Include(e => e.Currency)
                .Where(e => e.UserID == userId)
                .OrderByDescending(e => e.DateCreated)
                .ToListAsync();
        }

        public async Task<int> InsertAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            await _cleemyContext.Expenses.AddAsync(expense);
            await _cleemyContext.SaveChangesAsync();

            return expense.ID;
        }
    }
}
