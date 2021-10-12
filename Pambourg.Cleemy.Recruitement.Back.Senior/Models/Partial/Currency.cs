namespace Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities
{
    public partial class Currency
    {
        public Currency()
        {
        }

        public Currency(string code, string label)
        {
            if (string.IsNullOrWhiteSpace(code)
                || string.IsNullOrWhiteSpace(label))
            {
                return;
            }

            Code = code;
            Label = label;
        }
    }
}
