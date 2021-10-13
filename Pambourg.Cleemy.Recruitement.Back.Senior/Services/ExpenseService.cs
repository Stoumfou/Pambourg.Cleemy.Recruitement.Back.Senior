﻿using Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense;
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

            IEnumerable<Expense> expenses = await _expenseRepository.FindAsyncByUserId(userId);
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

            if (!ExpenseConstant.SortBy.Contains(sortBy.ToLowerInvariant()))
            {
                throw new ArgumentOutOfRangeException(nameof(sortBy));
            }

            if (!ExpenseConstant.SortOrder.Contains(sortOrder.ToLowerInvariant()))
            {
                throw new ArgumentOutOfRangeException(nameof(sortOrder));
            }

            IEnumerable<Expense> expenses = await _expenseRepository.FindAsyncByUserId(userId, sortBy, sortOrder);
            if (!expenses.Any())
            {
                return null;
            }

            return expenses.Select(expense => new ExpenseDTO(expense)).ToList();
        }

        public async Task CreateAsync(CreateExpenseDTO createExpenseDTO)
        {

            ValidateCreateExpenseDTO(createExpenseDTO);

            ExpenseType expenseType = await _expenseTypeRepository.FindAsyncByLabel(createExpenseDTO.Type);
            if (expenseType == null)
            {
                throw new NullReferenceException($"{nameof(createExpenseDTO.Type)} : {createExpenseDTO.Type}, could not be find");
            }

            User user = await _userRepository.FindAsyncById(createExpenseDTO.UserID);
            if (user == null)
            {
                throw new NullReferenceException($"{nameof(createExpenseDTO.UserID)} : {createExpenseDTO.UserID}, could not be find");
            }

            Currency currency = await _currencyRepository.FindAsyncByCode(createExpenseDTO.CurrencyCode);
            if (currency == null)
            {
                throw new NullReferenceException($"{nameof(createExpenseDTO.CurrencyCode)} : {createExpenseDTO.CurrencyCode}, could not be find");
            }

            if (user.Currency.ID != currency.ID)
            {
                throw new InvalidCurrencyException($"{nameof(createExpenseDTO.CurrencyCode)} {createExpenseDTO.CurrencyCode}, must be the same as the currency of the userID {user.ID} : {user.Currency.Code}");
            }

            if (user.Expenses.Where(e => e.DateCreated == createExpenseDTO.DateCreated // TODO See with product for 'same date' == day or dateTime?
                && e.Amount == createExpenseDTO.Amount && e.Currency.ID == currency.ID).Any())
            {
                throw new AlreadyExistException($"An expense already exist with this date({createExpenseDTO.DateCreated}), amount({createExpenseDTO.Amount}) and currency({createExpenseDTO.CurrencyCode})");
            }


            Expense expense = new Expense(user.ID, expenseType.ID, user.CurrencyID, createExpenseDTO.DateCreated, createExpenseDTO.Amount, createExpenseDTO.Comment);
            await _expenseRepository.InsertAsync(expense);
        }

        private void ValidateCreateExpenseDTO(CreateExpenseDTO createExpenseDTO)
        {
            if (createExpenseDTO == null)
            {
                throw new ArgumentNullException(nameof(createExpenseDTO));
            }

            if (createExpenseDTO.DateCreated > DateTime.Now)
            {
                throw new DateInFutureException($"{nameof(createExpenseDTO.DateCreated)} : {createExpenseDTO.DateCreated}, can not be in the futur");
            }

            if (createExpenseDTO.DateCreated < DateTime.Now.AddMonths(ExpenseConstant.MaxExpenseDate))
            {
                throw new DateTooOldException($"{nameof(createExpenseDTO.DateCreated)} : {createExpenseDTO.DateCreated}, can not be older than three months");
            }

            if (string.IsNullOrWhiteSpace(createExpenseDTO.Comment))
            {
                throw new CommentEmptyException($"{nameof(createExpenseDTO.Comment)} is required");
            }
        }
    }
}
