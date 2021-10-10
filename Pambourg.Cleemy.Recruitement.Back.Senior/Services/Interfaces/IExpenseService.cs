﻿using Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Services.Interfaces
{
    public interface IExpenseService
    {
        Task CreateAsync(CreateExpenseDTO expenseDTO);
        Task<IEnumerable<ExpenseDTO>> GetExpenseByUserIdAsync(int userId);
    }
}
