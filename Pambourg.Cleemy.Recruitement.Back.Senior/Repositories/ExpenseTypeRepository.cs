using Microsoft.EntityFrameworkCore;
using Pambourg.Cleemy.Recruitement.Back.Senior.Data;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories
{
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private readonly CleemyContext _cleemyContext;

        public ExpenseTypeRepository(CleemyContext cleemyContext)
        {
            _cleemyContext = cleemyContext;
        }

        public async Task<ExpenseType> FindAsyncByLabel(string ExpenseTypeLabel)
        {
            if (string.IsNullOrWhiteSpace(ExpenseTypeLabel))
            {
                throw new ArgumentNullException(nameof(ExpenseTypeLabel));
            }

            return await _cleemyContext.ExpenseTypes
                .Where(et => et.Label == ExpenseTypeLabel)
                .FirstOrDefaultAsync();
        }
    }
}
