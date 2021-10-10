using Pambourg.Cleemy.Recruitement.Back.Senior.Services;
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

        public bool IsValidDate()
        {
            return DateCreated > DateTime.Now && DateCreated < DateTime.Now.AddMonths(ExpenseConstant.MaxExpenseDate);
        }

        public bool IsValid()
        {
            if (!IsValidDate()
                || string.IsNullOrWhiteSpace(Comment)
                || User.Expenses.FirstOrDefault(e => e.DateCreated == DateCreated && e.Amount == Amount) != null
                || Currency != User.Currency)
            {
                return false;
            }

            return true;
        }

    }
}
