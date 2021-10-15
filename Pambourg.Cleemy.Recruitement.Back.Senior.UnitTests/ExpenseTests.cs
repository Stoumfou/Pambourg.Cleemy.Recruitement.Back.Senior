using Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.UnitTests
{
    public class ExpenseTests
    {
        [Fact]
        public void IsDuplicated()
        {
            /// Arrange
            DateTime now = DateTime.Now;
            User user = new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense> { new Expense() { Amount = 19.90M, Comment = "Jap", CurrencyID = 1, DateCreated = now, ExpenseTypeID = 1, UserID = 1 } }
            };

            Expense expense = new Expense()
            {
                Amount = 19.90M,
                Comment = "Jap",
                CurrencyID = 1,
                DateCreated = now,
                ExpenseTypeID = 1,
                UserID = 1,
                User = user
            };

            /// Act, Assert
            Assert.Throws<AlreadyExistException>(() => expense.IsDuplicated());
        }

        [Fact]
        public void IsDuplicatedDiffrentAmount()
        {
            /// Arrange
            DateTime now = DateTime.Now;
            User user = new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense> { new Expense() { Amount = 19.90M, Comment = "Jap", CurrencyID = 1, DateCreated = now, ExpenseTypeID = 1, UserID = 1 } }
            };

            Expense expense = new Expense()
            {
                Amount = 88.88M,
                Comment = "Jap 2",
                CurrencyID = 1,
                DateCreated = now,
                ExpenseTypeID = 1,
                UserID = 1,
                User = user
            };

            /// Act
            Exception exception = Record.Exception(() => expense.IsDuplicated());

            /// Assert
            Assert.Null(exception);
        }

        [Fact]
        public void IsDuplicatedDiffrentDateCreated()
        {
            /// Arrange
            DateTime now = DateTime.Now;
            User user = new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense> { new Expense() { Amount = 19.90M, Comment = "Jap", CurrencyID = 1, DateCreated = now, ExpenseTypeID = 1, UserID = 1 } }
            };

            Expense expense = new Expense()
            {
                Amount = 19.90M,
                Comment = "Jap 2",
                CurrencyID = 1,
                DateCreated = now.AddDays(-1),
                ExpenseTypeID = 1,
                UserID = 1,
                User = user
            };

            /// Act
            Exception exception = Record.Exception(() => expense.IsDuplicated());

            /// Assert
            Assert.Null(exception);
        }

        [Fact]
        public void IsDuplicatedDiffrentCurrency()
        {
            /// Arrange
            DateTime now = DateTime.Now;
            User user = new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense> { new Expense() { Amount = 19.90M, Comment = "Jap", CurrencyID = 1, DateCreated = now, ExpenseTypeID = 1, UserID = 1 } }
            };

            Expense expense = new Expense()
            {
                Amount = 19.90M,
                Comment = "Jap 2",
                CurrencyID = 2,
                DateCreated = now,
                ExpenseTypeID = 1,
                UserID = 1,
                User = user
            };

            /// Act
            Exception exception = Record.Exception(() => expense.IsDuplicated());

            /// Assert
            Assert.Null(exception);
        }
    }
}
