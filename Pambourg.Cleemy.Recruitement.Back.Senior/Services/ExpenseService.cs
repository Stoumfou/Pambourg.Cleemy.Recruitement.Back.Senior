using Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpenseByUserIdAsync(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            IEnumerable<Expense> expenses = await _expenseRepository.FindAsync(userId);
            if (!expenses.Any())
            {
                return null;
            }

            return expenses.Select(expense => new ExpenseDTO(expense)).ToList();
        }

        public async Task<int> CreateAsync(ExpenseDTO expenseDTO)
        {
            if (expenseDTO == null)
            {
                throw new ArgumentNullException(nameof(expenseDTO));
            }



            Expense expense = new Expense(expenseDTO.Amount, expenseDTO.Comment, expenseDTO.Currency, expenseDTO.FullName, expenseDTO.Type);
            return await _expenseRepository.InsertAsync(expense);
        }
    }
}
