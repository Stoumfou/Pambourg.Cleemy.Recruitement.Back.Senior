using Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public ExpenseService(IExpenseRepository expenseRepository,
            IUserRepository userRepository,
            ICurrencyRepository currencyRepository,
            IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _expenseTypeRepository = expenseTypeRepository;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpenseByUserIdAsync(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            IEnumerable<Expense> expenses = await _expenseRepository.FindByUserIdAsync(userId);
            if (!expenses.Any())
            {
                return null;
            }

            return expenses.Select(expense => new ExpenseDTO(expense)).ToList();
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpenseByUserIdAsync(int userId, string sortBy, string sortOrder)
        {
            if (userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(sortBy) || string.IsNullOrWhiteSpace(sortOrder))
            {
                return await GetExpenseByUserIdAsync(userId);
            }

            if (!ExpenseConstant.SortBy.Contains(sortBy))
            {
                throw new ArgumentOutOfRangeException(nameof(sortBy));
            }

            if (!ExpenseConstant.SortOrder.Contains(sortOrder.ToLowerInvariant()))
            {
                throw new ArgumentOutOfRangeException(nameof(sortOrder));
            }

            IEnumerable<Expense> expenses = await _expenseRepository.FindByUserIdAsync(userId, sortBy, sortOrder);
            if (!expenses.Any())
            {
                return null;
            }

            return expenses.Select(expense => new ExpenseDTO(expense)).ToList();
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllExpenseAsync()
        {
            IEnumerable<Expense> expenses = await _expenseRepository.GetAllAsync();
            if (!expenses.Any())
            {
                return null;
            }

            return expenses.Select(expense => new ExpenseDTO(expense)).ToList();
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllExpenseAsync(string sortBy, string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortBy) || string.IsNullOrWhiteSpace(sortOrder))
            {
                return await GetAllExpenseAsync();
            }

            if (!ExpenseConstant.SortBy.Contains(sortBy))
            {
                throw new ArgumentOutOfRangeException(nameof(sortBy));
            }

            if (!ExpenseConstant.SortOrder.Contains(sortOrder.ToLowerInvariant()))
            {
                throw new ArgumentOutOfRangeException(nameof(sortOrder));
            }

            IEnumerable<Expense> expenses = await _expenseRepository.GetAllAsync(sortBy, sortOrder);
            if (!expenses.Any())
            {
                return null;
            }

            return expenses.Select(expense => new ExpenseDTO(expense)).ToList();
        }

        public async Task CreateAsync(CreateExpenseDTO createExpenseDTO)
        {
            if (createExpenseDTO == null)
            {
                throw new ArgumentNullException(nameof(createExpenseDTO));
            }

            createExpenseDTO.ValidateCreateExpenseDTO();

            ExpenseType expenseType = await _expenseTypeRepository.FindAsyncByLabel(createExpenseDTO.Type);
            if (expenseType == null)
            {
                throw new NullReferenceException($"{nameof(createExpenseDTO.Type)} : {createExpenseDTO.Type}, could not be found");
            }

            User user = await _userRepository.FindAsyncById(createExpenseDTO.UserID);
            if (user == null)
            {
                throw new NullReferenceException($"{nameof(createExpenseDTO.UserID)} : {createExpenseDTO.UserID}, could not be found");
            }

            Currency currency = await _currencyRepository.FindAsyncByCode(createExpenseDTO.CurrencyCode);
            if (currency == null)
            {
                throw new NullReferenceException($"{nameof(createExpenseDTO.CurrencyCode)} : {createExpenseDTO.CurrencyCode}, could not be found");
            }

            if (user.Currency.ID != currency.ID)
            {
                throw new InvalidCurrencyException($"{nameof(createExpenseDTO.CurrencyCode)} {createExpenseDTO.CurrencyCode}, must be the same as the currency of the userID {user.ID} : {user.Currency.Code}");
            }

            Expense expense = new Expense(user.ID, expenseType.ID, user.CurrencyID, createExpenseDTO.DateCreated, createExpenseDTO.Amount, createExpenseDTO.Comment)
            {
                User = user
            };
            expense.IsDuplicated();

            await _expenseRepository.InsertAsync(expense);
        }
    }
}
