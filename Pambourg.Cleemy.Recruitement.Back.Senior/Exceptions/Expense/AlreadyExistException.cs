using System;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Exceptions.Expense
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string message) : base(message)
        {

        }
    }
}
