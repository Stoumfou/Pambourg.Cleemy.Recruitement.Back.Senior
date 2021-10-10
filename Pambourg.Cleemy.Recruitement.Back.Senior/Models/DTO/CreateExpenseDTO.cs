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
    }
}
