using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public static class QueryableExtensions
    {
        public static async Task<IOltPaged<TDestination>> ToPagedAsync<TDestination>(this IQueryable<TDestination> queryable, IOltPagingParams pagingParams)
        {
            var cnt = await queryable.CountAsync();

            var pagedQueryable = queryable
                .Skip((pagingParams.Page - 1) * pagingParams.Size)
                .Take(pagingParams.Size);


            return new OltPagedJson<TDestination>
            {
                Count = cnt,
                Page = pagingParams.Page,
                Size = pagingParams.Size,
                Data = await pagedQueryable.ToListAsync()
            };

        }
    }
}