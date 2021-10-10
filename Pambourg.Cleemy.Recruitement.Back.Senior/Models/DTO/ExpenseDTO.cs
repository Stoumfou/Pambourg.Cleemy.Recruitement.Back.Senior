using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO
{
    public class ExpenseDTO
    {
        public ExpenseDTO(Expense expense)
        {
            ID = expense.ID;
            FullName = expense.User.FirstName + " " + expense.User.LastName;
            Type = expense.Type.Label;
            Currency = expense.Currency.Label;
            DateCreated = expense.DateCreated;
            Amount = expense.Amount;
            Comment = expense.Comment;
        }

        public int ID { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }

}
