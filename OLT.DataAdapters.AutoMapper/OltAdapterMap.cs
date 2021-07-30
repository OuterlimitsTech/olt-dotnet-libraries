using System;
using System.Collections.Generic;
using AutoMapper;

namespace OLT.Core
{

    public abstract class OltAdapterMap : AutoMapper.Profile, IOltAdapterMap2
    {
        protected OltAdapterMap()
        {
            CreateMaps();
        }

        public abstract void CreateMaps();

        //public virtual string Name => $"{typeof(TModel).FullName}.Maps";

        ////#region [ Dispose ] 

        /////// <summary>
        /////// The disposed
        /////// </summary>
        ////protected bool Disposed { get; set; } = false;

        /////// <summary>
        /////// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /////// </summary>
        ////public void Dispose()
        ////{
        ////    Dispose(true);
        ////    GC.SuppressFinalize(this);
        ////}


        /////// <summary>
        /////// Releases unmanaged and - optionally - managed resources.
        /////// </summary>
        /////// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        ////protected virtual void Dispose(bool disposing)
        ////{
        ////    Disposed = true;
        ////}

        ////#endregion

        
    }

    public abstract class OltAdapterMap<TEntity, TModel> : OltAdapterMap, IOltAdapterMap2<TEntity, TModel>
    {
        protected IMappingExpression<TEntity, TModel> MapExpression { get; set; }

        protected OltAdapterMap()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            CreateMaps();
        }

        public override void CreateMaps()
        {
            MapExpression = CreateMap<TEntity, TModel>();
            BuildMap(MapExpression);
        }

        public abstract void BuildMap(IMappingExpression<TEntity, TModel> mappingExpression);

    }
}