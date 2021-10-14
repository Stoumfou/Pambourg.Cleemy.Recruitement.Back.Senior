using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> FindAsyncByUserId(int userId);
        Task<IEnumerable<Expense>> FindAsyncByUserId(int userId, string sortBy, string sortOrder);
        Task InsertAsync(Expense expense);
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<IEnumerable<Expense>> GetAllAsync(string sortBy, string sortOrder);
    }
}
