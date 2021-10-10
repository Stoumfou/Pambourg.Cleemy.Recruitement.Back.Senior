using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> FindAsync(int userId);
        Task<int> InsertAsync(Expense expense);
    }
}
