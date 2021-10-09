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

            var users = new User[]
            {
                new User("Stark","Anthony","Dollar américain"),
                new User("Romanova","Natasha","Rouble russe")
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
