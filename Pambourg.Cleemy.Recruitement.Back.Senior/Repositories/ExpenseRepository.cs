using Microsoft.EntityFrameworkCore;
using Pambourg.Cleemy.Recruitement.Back.Senior.Data;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services;
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

        public async Task<IEnumerable<Expense>> FindAsyncByUserId(int userId)
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
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> FindAsyncByUserId(int userId, string sortBy, string sortOrder)
        {
            if (userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(sortBy) || string.IsNullOrWhiteSpace(sortOrder))
            {
                return await FindAsyncByUserId(userId);
            }

            if (!ExpenseConstant.SortBy.Contains(sortBy.ToLowerInvariant()))
            {
                throw new ArgumentOutOfRangeException(nameof(sortBy));
            }

            if (!ExpenseConstant.SortOrder.Contains(sortOrder.ToLowerInvariant()))
            {
                throw new ArgumentOutOfRangeException(nameof(sortOrder));
            }

            IQueryable<Expense> query = _cleemyContext.Expenses
                .Include(e => e.User)
                .Include(e => e.Type)
                .Include(e => e.Currency)
                .Where(e => e.UserID == userId);

            if (sortOrder == "asc")
            {
                query.OrderBy(e => sortOrder);
            }
            else
            {
                query.OrderByDescending(e => sortOrder);
            }


            return await query.ToListAsync();
        }

        public async Task InsertAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            await _cleemyContext.Expenses.AddAsync(expense);
            await _cleemyContext.SaveChangesAsync();
        }
    }
}
