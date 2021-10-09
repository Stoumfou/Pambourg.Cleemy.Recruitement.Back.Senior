using System;
using System.Linq;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class Expenditure
    {
        private const int MaxExpenditureDate = -3;


        public bool IsValidDate()
        {
            return DateCreated > DateTime.Now && DateCreated < DateTime.Now.AddMonths(MaxExpenditureDate);
        }

        public bool IsValid()
        {
            if (!IsValidDate()
                || string.IsNullOrWhiteSpace(Comment)
                || User.Expenditures.FirstOrDefault(e => e.DateCreated == DateCreated && e.Amount == Amount) != null
                || Currency != User.Currency)
            {
                return false;
            }

            return true;
        }

    }
}
