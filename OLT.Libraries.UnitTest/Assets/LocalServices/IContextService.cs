using System.Collections.Generic;
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

        PersonEntity Get(bool includeDeleted, params IOltSearcher<PersonEntity>[] searchers);
        PersonEntity Get(IOltSearcher<PersonEntity> searcher);
        Task<PersonEntity> GetAsync(IOltSearcher<PersonEntity> searcher);
        List<PersonEntity> Get(bool includeDeleted);
        List<PersonEntity> GetPeopleOrdered(bool includeDeleted);
        List<PersonEntity> GetPeopleOrdered(IOltSearcher<PersonEntity> searcher);
        List<PersonEntity> GetPeopleOrdered(params IOltSearcher<PersonEntity>[] searchers);
        List<PersonEntity> GetNonDeleted();

        List<PersonAutoMapperModel> GetAllPeople();
        List<PersonAutoMapperModel> GetAllPeopleSearcher();
        List<PersonAutoMapperModel> GetAllPeopleOrdered();
        Task<List<PersonAutoMapperModel>> GetAllPeopleAsync();
        Task<List<PersonAutoMapperModel>> GetAllPeopleSearcherAsync();

        List<UserModel> GetAllUsers();
        List<UserModel> GetAllUsersSearcher();
        Task<List<UserModel>> GetAllUsersAsync();
        Task<List<UserModel>> GetAllUsersOrderedAsync();
        Task<List<UserModel>> GetAllUsersSearcherAsync();

        UserDto GetDtoUser(int id);
        Task<UserDto> GetDtoUserAsync(int id);

        List<UserDto> GetAllDtoUsers();
        List<UserDto> GetAllDtoUsersSearcher();
        Task<List<UserDto>> GetAllDtoUsersAsync();
        Task<List<UserDto>> GetAllDtoUsersSearcherAsync();


        bool Delete<TEntity>(int id) where TEntity: class, IOltEntityId;
        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IOltEntityId;
    }
}