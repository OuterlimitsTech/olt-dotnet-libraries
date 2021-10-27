﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public interface IContextService : IOltCoreService
    {
        PersonEntity CreatePerson();
        UserEntity CreateUser();

        PersonEntity Get(params IOltSearcher<PersonEntity>[] searchers);
        PersonEntity Get(IOltSearcher<PersonEntity> searcher);
        List<PersonEntity> Get(bool includeDeleted);
        List<PersonEntity> GetNonDeleted();

        List<PersonAutoMapperModel> GetAllPeople();
        List<PersonAutoMapperModel> GetAllPeopleSearcher();
        Task<List<PersonAutoMapperModel>> GetAllPeopleAsync();
        Task<List<PersonAutoMapperModel>> GetAllPeopleSearcherAsync();

        List<UserModel> GetAllUsers();
        List<UserModel> GetAllUsersSearcher();
        Task<List<UserModel>> GetAllUsersAsync();
        Task<List<UserModel>> GetAllUsersSearcherAsync();


        bool Delete<TEntity>(int id) where TEntity: class, IOltEntityId;
        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IOltEntityId;
    }
}