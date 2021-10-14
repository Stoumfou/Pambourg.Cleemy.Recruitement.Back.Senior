using Moq;
using Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.DTO;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.UnitTests
{
    public class ExpenseServiceTests
    {
        [Fact]
        public async Task CreateAsyncOkAsync()
        {
            /// Arrange
            CreateExpenseDTO createExpenseDTO = new CreateExpenseDTO()
            {
                Amount = 19.90M,
                Comment = "Jap",
                CurrencyCode = "USD",
                DateCreated = DateTime.Now,
                Type = "Restaurant",
                UserID = 1
            };

            Mock<IExpenseTypeRepository> expenseTypeRepoMock = new Mock<IExpenseTypeRepository>();
            expenseTypeRepoMock.Setup(e => e.FindAsyncByLabel(It.IsAny<string>())).ReturnsAsync(new ExpenseType() { ID = 1, Label = "Restaurant" });

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(e => e.FindAsyncById(It.IsAny<int>())).ReturnsAsync(new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense>()
            });

            Mock<ICurrencyRepository> currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock.Setup(e => e.FindAsyncByCode(It.IsAny<string>())).ReturnsAsync(new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" });
            Mock<IExpenseRepository> expenseRepoMock = new Mock<IExpenseRepository>();
            ExpenseService expenseService = new ExpenseService(expenseRepoMock.Object, userRepoMock.Object, currencyRepoMock.Object, expenseTypeRepoMock.Object);

            /// Act
            Exception exception = await Record.ExceptionAsync(() => expenseService.CreateAsync(createExpenseDTO));

            /// Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task CreateAsyncKoDateToOld()
        {
            /// Arrange
            CreateExpenseDTO createExpenseDTO = new CreateExpenseDTO()
            {
                Amount = 19.90M,
                Comment = "Jap",
                CurrencyCode = "USD",
                DateCreated = DateTime.Now.AddMonths(-4),
                Type = "Restaurant",
                UserID = 1
            };

            Mock<IExpenseTypeRepository> expenseTypeRepoMock = new Mock<IExpenseTypeRepository>();
            expenseTypeRepoMock.Setup(e => e.FindAsyncByLabel(It.IsAny<string>())).ReturnsAsync(new ExpenseType() { ID = 1, Label = "Restaurant" });

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(e => e.FindAsyncById(It.IsAny<int>())).ReturnsAsync(new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense>()
            });

            Mock<ICurrencyRepository> currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock.Setup(e => e.FindAsyncByCode(It.IsAny<string>())).ReturnsAsync(new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" });
            Mock<IExpenseRepository> expenseRepoMock = new Mock<IExpenseRepository>();
            ExpenseService expenseService = new ExpenseService(expenseRepoMock.Object, userRepoMock.Object, currencyRepoMock.Object, expenseTypeRepoMock.Object);

            /// Act, Assert
            await Assert.ThrowsAsync<DateTooOldException>(() => expenseService.CreateAsync(createExpenseDTO));
        }

        [Fact]
        public async Task CreateAsyncKoDateInFutur()
        {
            /// Arrange
            CreateExpenseDTO createExpenseDTO = new CreateExpenseDTO()
            {
                Amount = 19.90M,
                Comment = "Jap",
                CurrencyCode = "USD",
                DateCreated = DateTime.Now.AddDays(4),
                Type = "Restaurant",
                UserID = 1
            };

            Mock<IExpenseTypeRepository> expenseTypeRepoMock = new Mock<IExpenseTypeRepository>();
            expenseTypeRepoMock.Setup(e => e.FindAsyncByLabel(It.IsAny<string>())).ReturnsAsync(new ExpenseType() { ID = 1, Label = "Restaurant" });

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(e => e.FindAsyncById(It.IsAny<int>())).ReturnsAsync(new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense>()
            });

            Mock<ICurrencyRepository> currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock.Setup(e => e.FindAsyncByCode(It.IsAny<string>())).ReturnsAsync(new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" });
            Mock<IExpenseRepository> expenseRepoMock = new Mock<IExpenseRepository>();
            ExpenseService expenseService = new ExpenseService(expenseRepoMock.Object, userRepoMock.Object, currencyRepoMock.Object, expenseTypeRepoMock.Object);

            /// Act, Assert
            await Assert.ThrowsAsync<DateInFutureException>(() => expenseService.CreateAsync(createExpenseDTO));
        }

        [Fact]
        public async Task CreateAsyncKoNoComment()
        {
            /// Arrange
            CreateExpenseDTO createExpenseDTO = new CreateExpenseDTO()
            {
                Amount = 19.90M,
                Comment = string.Empty,
                CurrencyCode = "USD",
                DateCreated = DateTime.Now,
                Type = "Restaurant",
                UserID = 1
            };

            Mock<IExpenseTypeRepository> expenseTypeRepoMock = new Mock<IExpenseTypeRepository>();
            expenseTypeRepoMock.Setup(e => e.FindAsyncByLabel(It.IsAny<string>())).ReturnsAsync(new ExpenseType() { ID = 1, Label = "Restaurant" });

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(e => e.FindAsyncById(It.IsAny<int>())).ReturnsAsync(new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense>()
            });

            Mock<ICurrencyRepository> currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock.Setup(e => e.FindAsyncByCode(It.IsAny<string>())).ReturnsAsync(new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" });
            Mock<IExpenseRepository> expenseRepoMock = new Mock<IExpenseRepository>();
            ExpenseService expenseService = new ExpenseService(expenseRepoMock.Object, userRepoMock.Object, currencyRepoMock.Object, expenseTypeRepoMock.Object);

            /// Act, Assert
            await Assert.ThrowsAsync<CommentEmptyException>(() => expenseService.CreateAsync(createExpenseDTO));
        }

        [Fact]
        public async Task CreateAsyncKoDifferentCurrency()
        {
            /// Arrange
            CreateExpenseDTO createExpenseDTO = new CreateExpenseDTO()
            {
                Amount = 19.90M,
                Comment = "Jap",
                CurrencyCode = "RUB",
                DateCreated = DateTime.Now,
                Type = "Restaurant",
                UserID = 1
            };

            Mock<IExpenseTypeRepository> expenseTypeRepoMock = new Mock<IExpenseTypeRepository>();
            expenseTypeRepoMock.Setup(e => e.FindAsyncByLabel(It.IsAny<string>())).ReturnsAsync(new ExpenseType() { ID = 1, Label = "Restaurant" });

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(e => e.FindAsyncById(It.IsAny<int>())).ReturnsAsync(new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = new List<Expense>()
            });

            Mock<ICurrencyRepository> currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock.Setup(e => e.FindAsyncByCode(It.IsAny<string>())).ReturnsAsync(new Currency() { ID = 2, Code = "RUB", Label = "Rouble russe" });
            Mock<IExpenseRepository> expenseRepoMock = new Mock<IExpenseRepository>();
            ExpenseService expenseService = new ExpenseService(expenseRepoMock.Object, userRepoMock.Object, currencyRepoMock.Object, expenseTypeRepoMock.Object);

            /// Act, Assert
            await Assert.ThrowsAsync<InvalidCurrencyException>(() => expenseService.CreateAsync(createExpenseDTO));
        }

        [Fact]
        public async Task CreateAsyncKoExpenseAlreadyExist()
        {

            /// Arrange
            DateTime now = DateTime.Now;
            CreateExpenseDTO createExpenseDTO = new CreateExpenseDTO()
            {
                Amount = 19.90M,
                Comment = "Jap",
                CurrencyCode = "USD",
                DateCreated = now,
                Type = "Restaurant",
                UserID = 1
            };

            Mock<IExpenseTypeRepository> expenseTypeRepoMock = new Mock<IExpenseTypeRepository>();
            expenseTypeRepoMock.Setup(e => e.FindAsyncByLabel(It.IsAny<string>())).ReturnsAsync(new ExpenseType() { ID = 1, Label = "Restaurant" });

            List<Expense> expenses = new List<Expense>
            {
                new Expense() { Amount = 19.90M, DateCreated = now, Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" } }
            };
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(e => e.FindAsyncById(It.IsAny<int>())).ReturnsAsync(new User()
            {
                ID = 1,
                CurrencyID = 1,
                FirstName = "toto",
                LastName = "tutu",
                Currency = new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" },
                Expenses = expenses
            });


            Mock<ICurrencyRepository> currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock.Setup(e => e.FindAsyncByCode(It.IsAny<string>())).ReturnsAsync(new Currency() { ID = 1, Code = "USD", Label = "Dollar américain" });
            Mock<IExpenseRepository> expenseRepoMock = new Mock<IExpenseRepository>();
            ExpenseService expenseService = new ExpenseService(expenseRepoMock.Object, userRepoMock.Object, currencyRepoMock.Object, expenseTypeRepoMock.Object);

            /// Act, Assert
            await Assert.ThrowsAsync<AlreadyExistException>(() => expenseService.CreateAsync(createExpenseDTO));
        }
    }
}