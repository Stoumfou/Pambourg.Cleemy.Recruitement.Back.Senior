using System;
using System.Linq;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class Expense
    {
        public Expense()
        {
        }

        public Expense(decimal amount, string comment, string currency, string fullName, string type)
        {
            DateCreated = DateTime.Now;
            Amount = amount;
            Comment = comment;
        }

        private const int MaxExpenseDate = -3;


        public bool IsValidDate()
        {
            return DateCreated > DateTime.Now && DateCreated < DateTime.Now.AddMonths(MaxExpenseDate);
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
