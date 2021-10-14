using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Collections.Generic;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Services
{
    public static class ExpenseConstant
    {
        public const int MaxExpenseDate = -3;
        public static List<string> SortBy = new List<string>() { nameof(Expense.DateCreated), nameof(Expense.Amount) };
        public static List<string> SortOrder = new List<string>() { "asc".ToLowerInvariant(), "desc".ToLowerInvariant() };
    }
}
