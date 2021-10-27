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

        public PersonAutoMapperModel CreatePerson()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameLast = Faker.Name.Last()
            };

            Context.People.Add(entity);
            SaveChanges();

            return Get<PersonEntity, PersonAutoMapperModel>(GetQueryable(new OltSearcherGetAll<PersonEntity>()));
        }

        public UserEntity CreateUser()
        {
            var entity = UnitTestHelper.AddUser(Context);
            SaveChanges();
            return entity;
        }

        public PersonEntity Get(params IOltSearcher<PersonEntity>[] searchers)
        {
            return GetQueryable(searchers).FirstOrDefault();
        }

        public PersonEntity Get(IOltSearcher<PersonEntity> searcher)
        {
            return GetQueryable(searcher).FirstOrDefault();
        }

        public List<PersonAutoMapperModel> GetAllPeople()
        {
            return base.GetAll<PersonEntity, PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>()).ToList();
        }

        public List<UserModel> GetAllUsers()
        {
            return base.GetAll<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>()).ToList();
        }

        public async Task<List<PersonAutoMapperModel>> GetAllPeopleAsync()
        {
            var data = await base.GetAllAsync<PersonEntity, PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>());
            return data.ToList();
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var data = await base.GetAllAsync<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>());
            return data.ToList();
        }

        public List<PersonEntity> Get(bool includeDeleted)
        {
            return GetQueryable<PersonEntity>(includeDeleted).ToList();
        }

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