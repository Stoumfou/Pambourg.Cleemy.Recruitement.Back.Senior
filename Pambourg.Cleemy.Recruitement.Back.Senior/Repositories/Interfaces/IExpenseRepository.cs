﻿using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> FindAsyncByUserId(int userId);
        Task InsertAsync(Expense expense);
    }
}
