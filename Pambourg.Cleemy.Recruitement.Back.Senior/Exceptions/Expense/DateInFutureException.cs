using System;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense
{
    public class DateInFutureException : Exception
    {
        public DateInFutureException(string message) : base(message)
        {

        }
    }
}
