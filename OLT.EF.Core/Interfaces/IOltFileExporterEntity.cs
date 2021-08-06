////using System.Linq;
////using Microsoft.EntityFrameworkCore;

////namespace OLT.Core
////{
////    public interface IOltFileExportBuilderEntity<in TRequest, TContext, in TEntity> : IOltFileExportBuilder
////        where TRequest : IOltRequest<TContext>
////        where TContext : class, IOltDbContext
////        where TEntity : class, IOltEntity
////    {
////        IOltFileBase64 Build(TRequest request, IQueryable<TEntity> queryable);
////    }
////}