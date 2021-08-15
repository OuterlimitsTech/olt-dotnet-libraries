using AutoMapper;
using System;
using System.Linq;

namespace OLT.Core
{
    public abstract class OltAdapterPagedMap<TEntity, TModel> : AutoMapper.Profile, IOltAdapterPagedMap<TEntity, TModel>
        where TEntity : class, IOltEntity
    {

        protected OltAdapterPagedMap() => CreateMaps();

        public string Name => $"{typeof(TEntity).FullName}->{typeof(TModel).FullName}";

        public void CreateMaps()
        {
            BuildMap(CreateMap<TEntity, TModel>());
        }

        public abstract void BuildMap(IMappingExpression<TEntity, TModel> mappingExpression);
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