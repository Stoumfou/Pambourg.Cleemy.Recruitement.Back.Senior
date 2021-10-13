using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Collections.Generic;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Services
{
    public static class ExpenseConstant
    {
        public const int MaxExpenseDate = -3;
        public static List<string> SortBy = new List<string>() { nameof(Expense.DateCreated).ToLowerInvariant(), nameof(Expense.Amount).ToLowerInvariant() };
        public static List<string> SortOrder = new List<string>() { "asc".ToLowerInvariant(), "desc".ToLowerInvariant() };
    }
}
