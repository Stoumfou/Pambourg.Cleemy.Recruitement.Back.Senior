namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class User
    {
        public User(string lastName, string firstName, int currencyID)
        {
            if (string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(firstName)
                || currencyID == 0)
            {
                return;
            }

            LastName = lastName;
            FirstName = firstName;
            CurrencyID = currencyID;
        }
    }
}
