using System.Collections.Generic;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Currency { get; set; }

        public ICollection<Expenditure> Expenditures { get; set; }
    }
}
