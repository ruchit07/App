using App.Data.Models.Enums;

namespace App.Data.Models
{
    public class Filter
    {
        public int PageSize { get; set; } = 20;

        public int Page { get; set; } = 1;

        public string Query { get; set; }

        public OrderBy OrderBy { get; set; }

        public string OrderByColumn { get; set; }

        public string Uid { get; set; }

        public string[] GetKeyWords()
        {
            return Query.Split(' ');
        }
    }
}
