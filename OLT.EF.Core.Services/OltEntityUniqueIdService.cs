using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltEntityUniqueIdService<TContext, TEntity> : OltEntityService<TContext, TEntity>, IOltEntityUniqueIdService<TEntity>
        where TEntity : class, IOltEntityUniqueId, IOltEntity, new()
        where TContext : DbContext, IOltDbContext
    {
        protected OltEntityUniqueIdService(
            IOltServiceManager serviceManager, 
            TContext context) : base(serviceManager, context)
        {
        }
      

        #region [ Get Queryable ]

        protected virtual IQueryable<TEntity> GetQueryable(Guid uid) => GetQueryable(new OltSearcherGetByUid<TEntity>(uid));

        #endregion

        #region [ Get ]

        public virtual TModel Get<TModel>(Guid uid) where TModel : class, new() => Get<TModel>(GetQueryable(uid));

        public virtual async Task<TModel> GetAsync<TModel>(Guid uid) where TModel : class, new() => await GetAsync<TModel>(GetQueryable(uid));

        #endregion

        #region [ Build Result List ]

        protected override List<TModel> BuildResultList<TModel>(List<TEntity> entities)
        {
            var returnList = new List<TModel>();
            entities.ForEach(entity =>
            {
                returnList.Add(Get<TModel>(entity.UniqueId));
            });
            return returnList;
        }

        #endregion

        #region [ Add  ]

        public override TModel Add<TModel>(TModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            return Get<TModel>(entity.UniqueId);
        }

        public override TResponseModel Add<TResponseModel, TSaveModel>(TSaveModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            return Get<TResponseModel>(entity.UniqueId);
        }


        public override async Task<TResponseModel> AddAsync<TResponseModel, TSaveModel>(TSaveModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            await Repository.AddAsync(entity);
            await SaveChangesAsync();
            return await GetAsync<TResponseModel>(entity.UniqueId);
        }

        public override async Task<TModel> AddAsync<TModel>(TModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            await Repository.AddAsync(entity);
            await SaveChangesAsync();
            return await GetAsync<TModel>(entity.UniqueId);
        }

        #endregion

        #region [ Update ]


        public virtual TModel Update<TModel>(Guid uid, TModel model)
            where TModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(uid)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            return Get<TModel>(uid);
        }


        public virtual TResponseModel Update<TResponseModel, TModel>(Guid uid, TModel model)
            where TModel : class, new()
            where TResponseModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(uid)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            return Get<TResponseModel>(uid);
        }

        public virtual async Task<TModel> UpdateAsync<TModel>(Guid uid, TModel model)
            where TModel : class, new()
        {
            var entity = await ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(uid)).FirstOrDefaultAsync();
            ServiceManager.AdapterResolver.Map(model, entity);
            await SaveChangesAsync();
            return await GetAsync<TModel>(uid);
        }

        public virtual async Task<TResponseModel> UpdateAsync<TResponseModel, TModel>(Guid uid, TModel model)
            where TModel : class, new()
            where TResponseModel : class, new()
        {
            var entity = await ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(uid)).FirstOrDefaultAsync();
            ServiceManager.AdapterResolver.Map(model, entity);
            await SaveChangesAsync();
            return await GetAsync<TResponseModel>(uid);
        }

        #endregion

        #region [ Soft Delete ]

        public virtual bool SoftDelete(Guid uid)
        {
            var entity = GetQueryable(uid).FirstOrDefault();
            return entity != null && MarkDeleted(entity);
        }

        public virtual async Task<bool> SoftDeleteAsync(Guid uid)
        {
            var entity = await GetQueryable(uid).FirstOrDefaultAsync();
            return entity != null && await MarkDeletedAsync(entity);
        }

        #endregion

    }
}