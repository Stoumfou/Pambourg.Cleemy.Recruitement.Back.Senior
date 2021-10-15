using Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense;
using System;
using System.Linq;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class Expense
    {
        public Expense()
        {
        }

        public Expense(int userID, int expenseTypeID, int currencyID, DateTime dateCreated, decimal amount, string comment)
        {
            UserID = userID;
            ExpenseTypeID = expenseTypeID;
            CurrencyID = currencyID;
            DateCreated = dateCreated;
            Amount = amount;
            Comment = comment;
        }

        public void IsDuplicated() // TODO See with product for 'same date' == day or dateTime?
        {
            if (User.Expenses.Where(e => e.DateCreated == DateCreated && e.Amount == Amount && User.CurrencyID == CurrencyID).Any())
            {
                throw new AlreadyExistException($"An expense already exist with this date({DateCreated}), amount({Amount}) and currency({CurrencyID})");
            }
        }
    }
}
