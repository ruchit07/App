using System.Linq;

namespace App.Data.Models
{
    public static class LeadQuery
    {
        #region 'Query'

        #region 'Contains'
        public static IQueryable<Lead> Contains(this IQueryable<Lead> query, LeadFilter filter)
        {
            if (string.IsNullOrEmpty(filter.Query))
            {
                return query;
            }

            string[] keywords = filter.GetKeyWords();
            if(keywords.Length > 1)
            {
                foreach (var k in keywords)
                {
                    query = query.Concat(WhereLead(query, k));
                }

                return query.Distinct();
            }

            return WhereLead(query, filter.Query);

        }

        private static IQueryable<Lead> WhereLead(IQueryable<Lead> query, string k)
        {
            return query.Where(x => (x.FirstName.Contains(k)
                                      || x.LastName.Contains(k)
                                      || x.Phone.Contains(k)
                                      || x.Email.Contains(k)
                                  )
                                  || (x.LeadSource.SourceInfoCode.Contains(k))
                                  || (x.Addresses.Any(y => y.Address1.Contains(k) || y.Address2.Contains(k)))
                                  || (x.Notes.Any(y => y.Note.Contains(k) || y.Note.Contains(k)))
                            );
        }
        #endregion

        #region 'Filter'
        public static IQueryable<Lead> Filter(this IQueryable<Lead> query, LeadFilter filter)
        {
            // TODO : Implement kendo grid filter
            return query;
        }
        #endregion

        #region 'Sort'
        public static IQueryable<Lead> Sort(this IQueryable<Lead> query, LeadFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.OrderByColumn))
            {
                query = query.SortBy(
                   sortExpression: filter.OrderByColumn.GetExpression<Lead>(),
                   sortOrder: filter.OrderBy
                );
            }

            return query;
        }
        #endregion

        #region 'Pagging'
        public static IQueryable<Lead> Paging(this IQueryable<Lead> query, LeadFilter filter) =>
             query.Skip((filter.Page - 1) * filter.PageSize)
                  .Take(filter.PageSize);
        #endregion

        #endregion
    }
}
