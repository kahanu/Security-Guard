using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityGuard.Core.Pagination
{
    public interface IPaginatedList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }

        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
