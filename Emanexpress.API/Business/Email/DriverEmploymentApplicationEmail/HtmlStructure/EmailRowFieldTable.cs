namespace Emanexpress.API.Business.Email
{
    internal class EmailRowFieldTable
    {
        public string Label { get; }
        public string Value { get; }
        public float? WidthPercentage { get; }

        public EmailRowFieldTable(string label, string value, float? widthPercentage = null)
        {
            Label = label;
            Value = value;
            WidthPercentage = widthPercentage;
        }
    }
}