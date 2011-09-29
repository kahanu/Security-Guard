using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityGuard.Core.Pagination
{
    public class PaginatedList<T> : List<T>
    {

        #region IPagination<MembershipUser> Members

        public int PageIndex
        {
            get;
            private set;
        }

        public int PageSize
        {
            get;
            private set;
        }

        public int TotalCount
        {
            get;
            private set;
        }

        public int TotalPages
        {
            get;
            private set;
        }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

        public string searchterm
        {
            get;
            private set;
        }

        public string filterby
        {
            get;
            private set;
        }

        #endregion

        public PaginatedList(IList<T> source, int count, int pageIndex, int pageSize, string searchterm, string filterby)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = count;
            this.TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            this.searchterm = searchterm;
            this.filterby = filterby;

            this.AddRange(source);
        }
    }
}
