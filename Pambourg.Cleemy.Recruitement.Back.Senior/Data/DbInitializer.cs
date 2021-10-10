using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(CleemyContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            ExpenseType[] expenseTypes = new ExpenseType[]
            {
                new ExpenseType("Restaurant"),
                new ExpenseType("Hotel"),
                new ExpenseType("Misc")
            };
            context.ExpenseTypes.AddRange(expenseTypes);

            Currency[] currencies = new Currency[]
            {
                new Currency("USD","Dollar américain"),
                new Currency("RUB" ,"Rouble russe")
            };
            context.Currencies.AddRange(currencies);

            User[] users = new User[]
            {
                new User("Stark","Anthony",1),
                new User("Romanova","Natasha",2)
            };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();


            Expense[] expenses = new Expense[]
            {
                new Expense()
                {
                    Amount = 123,
                    Comment = "test",
                    CurrencyID = 1,
                    DateCreated = System.DateTime.Now.AddMonths(-1),
                    ExpenseTypeID = 1,
                    UserID = 1
                }
            };
            context.Expenses.AddRange(expenses);

            await context.SaveChangesAsync();
        }
    }
}
