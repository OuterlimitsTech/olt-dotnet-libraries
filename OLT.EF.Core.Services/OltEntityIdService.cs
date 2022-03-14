using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        #region [ Get Queryable ]

        protected virtual IQueryable<TEntity> GetQueryable(int id) => GetQueryable(new OltSearcherGetById<TEntity>(id));

        #endregion

        #region [ Get ]
        
        public virtual TModel Get<TModel>(int id) where TModel : class, new() => Get<TModel>(GetQueryable(id));

        public virtual async Task<TModel> GetAsync<TModel>(int id) where TModel : class, new() => await GetAsync<TModel>(GetQueryable(id));

        #endregion

        #region [ Build Result List ]

        protected override List<TModel> BuildResultList<TModel>(List<TEntity> entities)
        {
            var returnList = new List<TModel>();
            entities.ForEach(entity =>
            {
                returnList.Add(Get<TModel>(entity.Id));
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


        public override async Task<TResponseModel> AddAsync<TResponseModel, TSaveModel>(TSaveModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            await Repository.AddAsync(entity);
            await SaveChangesAsync();
            return await GetAsync<TResponseModel>(entity.Id);
        }

        public override async Task<TModel> AddAsync<TModel>(TModel model)
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            await Repository.AddAsync(entity);
            await SaveChangesAsync();
            return await GetAsync<TModel>(entity.Id);
        }

        #endregion

        #region [ Update ]

        
        public virtual TModel Update<TModel>(int id, TModel model)
            where TModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(id)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            return Get<TModel>(id);
        }


        public virtual TResponseModel Update<TResponseModel, TModel>(int id, TModel model)
            where TModel : class, new()
            where TResponseModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(id)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            return Get<TResponseModel>(id);
        }

        public virtual async Task<TModel> UpdateAsync<TModel>(int id, TModel model)
            where TModel : class, new()
        {
            var entity = await ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(id)).FirstOrDefaultAsync();
            ServiceManager.AdapterResolver.Map(model, entity);
            await SaveChangesAsync();
            return await GetAsync<TModel>(id);
        }

        public virtual async Task<TResponseModel> UpdateAsync<TResponseModel, TModel>(int id, TModel model)
            where TModel : class, new()
            where TResponseModel : class, new()
        {
            var entity = await ServiceManager.AdapterResolver.ApplyBeforeMaps<TEntity, TModel>(GetQueryable(id)).FirstOrDefaultAsync();
            ServiceManager.AdapterResolver.Map(model, entity);
            await SaveChangesAsync();
            return await GetAsync<TResponseModel>(id);
        }

        #endregion

        #region [ Soft Delete ]

        public virtual bool SoftDelete(int id)
        {
            var entity = GetQueryable(id).FirstOrDefault();
            return entity != null && MarkDeleted(entity);
        }

        public virtual async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await GetQueryable(id).FirstOrDefaultAsync();
            return entity != null && await MarkDeletedAsync(entity);
        }

        #endregion

    }
}