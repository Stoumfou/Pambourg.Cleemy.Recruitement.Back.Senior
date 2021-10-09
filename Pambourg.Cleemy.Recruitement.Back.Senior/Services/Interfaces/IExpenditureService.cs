using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using System.Collections.Generic;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Services.Interfaces
{
    public interface IExpenditureService
    {
        IEnumerable<Expenditure> GetExpenditureByUserId(int userId);
    }
}
