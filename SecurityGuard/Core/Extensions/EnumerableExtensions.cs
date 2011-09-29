using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Security;
using SecurityGuard.Core.Pagination;

namespace SecurityGuard.Core.Extensions
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        [DebuggerStepThrough]
        public static List<System.Web.Security.MembershipUser> ToList(this MembershipUserCollection collection)
        {

            List<System.Web.Security.MembershipUser> list = new List<System.Web.Security.MembershipUser>();
            foreach (System.Web.Security.MembershipUser user in collection)
            {
                list.Add(user);
            }

            return list;
        }

        public static PaginatedList<System.Web.Security.MembershipUser> ToPaginatedList(this MembershipUserCollection collection, int pageIndex, int pageSize, int totalRecords, string searchterm, string filterby)
        {
            var list = collection.ToList();
            return new PaginatedList<System.Web.Security.MembershipUser>(list, totalRecords, pageIndex, pageSize, searchterm, filterby);
        }
    }
}
