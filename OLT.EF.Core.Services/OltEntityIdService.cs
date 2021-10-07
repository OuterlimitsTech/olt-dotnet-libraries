using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltEntityIdService<TContext, TEntity> : OltEntityService<TContext, TEntity>, IOltEntityIdService<TEntity>
        where TEntity : class, IOltEntityId, IOltEntity, new()
        where TContext : DbContext, IOltDbContext
    {
        protected OltEntityIdService(
            IOltServiceManager serviceManager,
            TContext context) : base(serviceManager, context)
        {
        }

        
        public virtual TModel Get<TModel>(int id) where TModel : class, new() => base.Get<TModel>(GetQueryable(id));

        protected virtual IQueryable<TEntity> GetQueryable(int id) => GetQueryable().Where(p => p.Id == id);


        public override TModel Add<TModel>(TModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            return Get<TModel>(entity.Id);
        }

        public override TResponseModel Add<TResponseModel, TSaveModel>(TSaveModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            return Get<TResponseModel>(entity.Id);
        }

        public override IEnumerable<TResponseModel> Add<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> list)
        {
            var entities = new List<TEntity>();
            list.ToList().ForEach(model =>
            {
                var entity = new TEntity();
                ServiceManager.AdapterResolver.Map(model, entity);
                Repository.Add(entity);
                entities.Add(entity);
            });

            SaveChanges();

            var returnList = new List<TResponseModel>();
            entities.ForEach(entity =>
            {
                returnList.Add(Get<TResponseModel>(entity.Id));
            });
            return returnList;
        }


        public virtual TModel Update<TModel>(int id, TModel model)
            where TModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.Include<TEntity, TModel>(GetQueryable(id)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            return Get<TModel>(id);
        }


        public virtual TResponseModel Update<TResponseModel, TModel>(int id, TModel model)
            where TModel : class, new()
            where TResponseModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.Include<TEntity, TModel>(GetQueryable(id)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            return Get<TResponseModel>(id);
        }

        public virtual bool SoftDelete(int id)
        {
            var entity = GetQueryable(id).FirstOrDefault();
            return entity != null && MarkDeleted(entity);
        }

        public int Count(IOltSearcher<TEntity> searcher)
        {
            return GetQueryable(searcher).Count();
        }
    }
}