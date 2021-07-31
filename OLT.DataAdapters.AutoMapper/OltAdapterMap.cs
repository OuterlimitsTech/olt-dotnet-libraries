using System;
using System.Collections.Generic;
using AutoMapper;

namespace OLT.Core
{

    public abstract class OltAdapterMap : AutoMapper.Profile, IOltAdapterMap
    {
        protected OltAdapterMap()
        {
            CreateMaps();
        }

        public abstract void CreateMaps();
    }

    public abstract class OltAdapterMap<TEntity, TModel> : OltAdapterMap, IOltAdapterMap<TEntity, TModel>
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