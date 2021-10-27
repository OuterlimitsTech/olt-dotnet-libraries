using System.Collections.Generic;
using System.Threading.Tasks;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public interface IContextService : IOltCoreService
    {
        PersonAutoMapperModel CreatePerson();
        UserEntity CreateUser();
        PersonEntity Get(params IOltSearcher<PersonEntity>[] searchers);
        PersonEntity Get(IOltSearcher<PersonEntity> searcher);
        List<PersonAutoMapperModel> GetAllPeople();
        Task<List<PersonAutoMapperModel>> GetAllPeopleAsync();
        List<UserModel> GetAllUsers();
        Task<List<UserModel>> GetAllUsersAsync();
        List<PersonEntity> Get(bool includeDeleted);
        bool Delete<TEntity>(int id) where TEntity: class, IOltEntityId;
        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IOltEntityId;
    }
}