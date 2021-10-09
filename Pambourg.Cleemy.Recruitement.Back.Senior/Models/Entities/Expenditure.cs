using System;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class Expenditure
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Nature { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Comment { get; set; }

        public User User { get; set; }
    }

    //  public enum NaturePambourgCleemyRecruitementBackSenior
    //  {    
    //      "Restaurant",
    //      "Hotel",
    //      "Misc"  
    //}
}
