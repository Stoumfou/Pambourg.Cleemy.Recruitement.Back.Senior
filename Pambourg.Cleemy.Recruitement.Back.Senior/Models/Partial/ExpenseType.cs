namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class ExpenseType
    {
        public ExpenseType()
        {
        }

        public ExpenseType(string label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                Label = label;
            }
        }
    }
}
