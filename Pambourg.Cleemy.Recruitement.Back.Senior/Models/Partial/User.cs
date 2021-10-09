namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class User
    {
        public User(string lastName, string firstName, string currency)
        {
            if (!string.IsNullOrWhiteSpace(lastName)
                && !string.IsNullOrWhiteSpace(firstName)
                && !string.IsNullOrWhiteSpace(currency))
            {
                LastName = lastName;
                FirstName = firstName;
                Currency = currency;
            }
        }
    }
}
