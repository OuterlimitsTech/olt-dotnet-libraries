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
    public class ContextService : BaseContextService, IContextService
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

        public PersonEntity Get(bool includeDeleted, params IOltSearcher<PersonEntity>[] searchers) => GetQueryable(includeDeleted, searchers).FirstOrDefault();
        public PersonEntity Get(IOltSearcher<PersonEntity> searcher) => GetQueryable(searcher).FirstOrDefault();
        public async Task<PersonEntity> GetAsync(IOltSearcher<PersonEntity> searcher) => await GetQueryable(searcher).FirstOrDefaultAsync();

        public List<PersonEntity> Get(bool includeDeleted) => GetQueryable<PersonEntity>(includeDeleted).ToList();
        public List<PersonEntity> GetNonDeleted() => InitializeQueryable<PersonEntity>().ToList();

        public List<PersonAutoMapperModel> GetAllPeople() => GetAll<PersonEntity, PersonAutoMapperModel>(Context.People).ToList();
        public List<PersonAutoMapperModel> GetAllPeopleSearcher() => GetAll<PersonEntity, PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>()).ToList();
        public async Task<List<PersonAutoMapperModel>> GetAllPeopleAsync() => (await GetAllAsync<PersonEntity, PersonAutoMapperModel>(Context.People)).ToList();
        public async Task<List<PersonAutoMapperModel>> GetAllPeopleSearcherAsync() => (await GetAllAsync<PersonEntity, PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>())).ToList();

        public List<UserModel> GetAllUsers() => GetAll<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>()).ToList();
        public List<UserModel> GetAllUsersSearcher() => GetAll<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>()).ToList();
        public async Task<List<UserModel>> GetAllUsersAsync() => (await GetAllAsync<UserEntity, UserModel>(Context.Users)).ToList();
        public async Task<List<UserModel>> GetAllUsersSearcherAsync() => (await GetAllAsync<UserEntity, UserModel>(new OltSearcherGetAll<UserEntity>())).ToList();
        public UserDto GetDtoUser(int id) => Get<UserEntity, UserDto>(Context.Users.Where(p => p.Id == id));
        public async Task<UserDto> GetDtoUserAsync(int id) => await GetAsync<UserEntity, UserDto>(Context.Users.Where(p => p.Id == id));

        public List<UserDto> GetAllDtoUsers() => GetAll<UserEntity, UserDto>(new OltSearcherGetAll<UserEntity>()).ToList();
        public List<UserDto> GetAllDtoUsersSearcher() => GetAll<UserEntity, UserDto>(new OltSearcherGetAll<UserEntity>()).ToList();
        public async Task<List<UserDto>> GetAllDtoUsersAsync() => (await GetAllAsync<UserEntity, UserDto>(Context.Users)).ToList();
        public async Task<List<UserDto>> GetAllDtoUsersSearcherAsync() => (await GetAllAsync<UserEntity, UserDto>(new OltSearcherGetAll<UserEntity>())).ToList();
        
        public bool Delete<TEntity>(int id) where TEntity : class, IOltEntityId
        {
            var entity = GetQueryable(new OltSearcherGetById<TEntity>(id)).FirstOrDefault();
            return MarkDeleted(entity);
        }

        public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IOltEntityId
        {
            var entity = await GetQueryable(new OltSearcherGetById<TEntity>(id)).FirstOrDefaultAsync();
            return await MarkDeletedAsync(entity);
        }
    }
}