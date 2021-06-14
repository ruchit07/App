namespace App.Data.Model
{
    public static partial class SelectOption
    {
        public static class Lead
        {
            public const string LeadStage = "LDSTG";
            public const string LeadCategory = "LDCAT";
            public const string LeadSource = "LDSRC";
            public const string LeadRating = "LDRTG";

            public static string[] Get() => new string[] { LeadStage, LeadCategory, LeadSource, LeadRating };
        }
    }
}
