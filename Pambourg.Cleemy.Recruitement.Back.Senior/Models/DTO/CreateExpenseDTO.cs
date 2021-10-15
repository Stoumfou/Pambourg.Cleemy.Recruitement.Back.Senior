using Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services;
using System;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO
{
    public class CreateExpenseDTO
    {
        public int UserID { get; set; }
        public string Type { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }

        public void ValidateCreateExpenseDTO()
        {

            if (DateCreated > DateTime.Now)
            {
                throw new DateInFutureException($"{nameof(DateCreated)} : {DateCreated}, can not be in the futur");
            }

            if (DateCreated < DateTime.Now.AddMonths(ExpenseConstant.MaxExpenseDate))
            {
                throw new DateTooOldException($"{nameof(DateCreated)} : {DateCreated}, can not be older than three months");
            }

            if (string.IsNullOrWhiteSpace(Comment))
            {
                throw new CommentEmptyException($"{nameof(Comment)} is required");
            }
        }
    }
}
