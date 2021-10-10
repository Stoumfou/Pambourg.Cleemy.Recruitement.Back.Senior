using System;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class Expense
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ExpenseTypeID { get; set; }
        public int CurrencyID { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }

        public Currency Currency { get; set; }
        public User User { get; set; }
        public ExpenseType Type { get; set; }
    }
}
