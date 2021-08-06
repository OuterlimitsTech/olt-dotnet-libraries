////using System.Linq;
////using Microsoft.EntityFrameworkCore;

////namespace OLT.Core
////{
////    public abstract class OltFileExportBuilderEntity<TRequest, TContext, TEntity> : OltDisposable, IOltFileExportBuilderEntity<TRequest, TContext, TEntity>
////        where TRequest : IOltRequest<TContext>
////        where TContext : class, IOltDbContext
////        where TEntity : class, IOltEntity
////    {
////        public abstract string BuilderName { get; }
////        public abstract IOltFileBase64 Build(TRequest request, IQueryable<TEntity> queryable);
////    }
////}