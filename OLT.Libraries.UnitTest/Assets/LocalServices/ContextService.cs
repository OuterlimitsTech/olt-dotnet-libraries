using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Enums;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Libraries.UnitTest.Assets.Searchers;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public class ContextService : BaseEntityService, IContextService
    {
        public ContextService(IOltServiceManager serviceManager, SqlDatabaseContext context) : base(serviceManager, context)
        {
        }

        public PersonEntity CreatePerson()
        {
            var entity = UnitTestHelper.AddPerson(Context);
            SaveChanges();
            return entity;
        }

        public UserEntity CreateUser()
        {
            var entity = UnitTestHelper.AddUser(Context);
            SaveChanges();
            return entity;
        }

        public PersonEntity Get(params IOltSearcher<PersonEntity>[] searchers) => GetQueryable(searchers).FirstOrDefault();
        public PersonEntity Get(IOltSearcher<PersonEntity> searcher) => GetQueryable(searcher).FirstOrDefault();
        public List<PersonEntity> Get(bool includeDeleted) => GetQueryable<PersonEntity>(includeDeleted).ToList();
        public List<PersonEntity> GetNonDeleted() => base.NonDeletedQueryable(Context.People).ToList();


        
        public List<PersonAutoMapperModel> GetAllPeople() => base.GetAll<PersonEntity, PersonAutoMapperModel>(Context.People).ToList();
        public List<PersonAutoMapperModel> GetAllPeopleSearcher() => base.GetAll<PersonEntity, PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>()).ToList();
        public async Task<List<PersonAutoMapperModel>> GetAllPeopleAsync() => (await base.GetAllAsync<PersonEntity, PersonAutoMapperModel>(Context.People)).ToList();
        public async Task<List<PersonAutoMapperModel>> GetAllPeopleSearcherAsync() => (await base.GetAllAsync<PersonEntity, PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>())).ToList();

        public List<UserModel> GetAllUsers() => base.GetAll<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>()).ToList();
        public List<UserModel> GetAllUsersSearcher() => base.GetAll<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>()).ToList();
        public async Task<List<UserModel>> GetAllUsersAsync() => (await base.GetAllAsync<UserEntity, UserModel>(Context.Users)).ToList();
        public async Task<List<UserModel>> GetAllUsersSearcherAsync() => (await base.GetAllAsync<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>())).ToList();


        public bool Delete<TEntity>(int id) where TEntity : class, IOltEntityId
        {
            var entity = GetQueryable(new OltSearcherGetById<TEntity>(id)).FirstOrDefault();
            return base.MarkDeleted(entity);
        }

        public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IOltEntityId
        {
            var entity = await GetQueryable(new OltSearcherGetById<TEntity>(id)).FirstOrDefaultAsync();
            return await base.MarkDeletedAsync(entity);
        }
    }
}