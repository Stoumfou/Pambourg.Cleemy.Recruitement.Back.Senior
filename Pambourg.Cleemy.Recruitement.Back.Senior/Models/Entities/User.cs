using System.Collections.Generic;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class User
    {
        public int ID { get; set; }
        public int CurrencyID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public Currency Currency { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
