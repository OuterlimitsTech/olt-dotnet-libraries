using System;
using AutoMapper;

namespace OLT.Core
{
    public abstract class OltAdapterMap<TObj1, TObj2> : AutoMapper.Profile, IOltAdapterMap
    {
        protected IMappingExpression<TObj1, TObj2> MapExpression { get; }

        protected OltAdapterMap()
        {
            MapExpression = CreateMap<TObj1, TObj2>();
            BuildMap(MapExpression);
        }

        public virtual string Name => $"{typeof(TObj1).FullName}->{typeof(TObj2).FullName}";

        public abstract void BuildMap(IMappingExpression<TObj1, TObj2> expression);

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