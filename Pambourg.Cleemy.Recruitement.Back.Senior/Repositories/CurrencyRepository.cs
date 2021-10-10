using Microsoft.EntityFrameworkCore;
using Pambourg.Cleemy.Recruitement.Back.Senior.Data;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CleemyContext _cleemyContext;

        public CurrencyRepository(CleemyContext cleemyContext)
        {
            _cleemyContext = cleemyContext;
        }

        public async Task<Currency> FindAsyncByCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                throw new ArgumentNullException(nameof(currencyCode));
            }

            return await _cleemyContext.Currencies
                .Where(u => u.Code == currencyCode)
                .FirstOrDefaultAsync();
        }
    }
}
