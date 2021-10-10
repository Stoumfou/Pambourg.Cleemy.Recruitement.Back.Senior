using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces
{
    public interface IExpenseTypeRepository
    {
        Task<ExpenseType> FindAsyncByLabel(string ExpenseTypeLabel);
    }
}