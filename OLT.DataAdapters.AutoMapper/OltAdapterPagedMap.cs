using System;
using System.Linq;

namespace OLT.Core
{
    public abstract class OltAdapterPagedMap<TEntity, TModel> : OltAdapterMap<TEntity, TModel>, IOltAdapterPagedMap<TEntity, TModel>
        where TEntity : class, IOltEntity
    {
        public string Name => $"{typeof(TEntity).FullName}->{typeof(TModel).FullName}";

        public abstract IQueryable<TEntity> DefaultOrderBy(IQueryable<TEntity> queryable);

        #region [ Dispose ]

        /// <summary>
        /// The disposed
        /// </summary>
        protected bool Disposed { get; set; } = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }

        #endregion


    }
}