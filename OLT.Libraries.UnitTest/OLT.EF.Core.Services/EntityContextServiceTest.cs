using System;
using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Searchers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    // ReSharper disable once InconsistentNaming
    public class EntityContextServiceTest : BaseTest
    {
        private readonly IContextService _contextService;
        
        public EntityContextServiceTest(
            IContextService contextService,
            ITestOutputHelper output) : base(output)
        {
            _contextService = contextService;
        }

        [Fact]
        public void QueryableSearchers()
        {
            var model = _contextService.CreatePerson();
            var result = _contextService.Get(new PersonFirstNameStartsWithSearcher(model.NameFirst), new PersonLastNameStartsWithSearcher(model.NameLast));
            Assert.Equal(model.Id, result.Id);
        }

        [Fact]
        public void QueryableSearcher()
        {
            var model = _contextService.CreatePerson();
            var result = _contextService.Get(new OltSearcherGetById<PersonEntity>(model.Id));
            Assert.Equal(model.Id, result.Id);
        }

        [Fact]
        public void Get()
        {
            _contextService.CreatePerson();
            var userEntity = _contextService.CreateUser();
            Assert.True(_contextService.GetAllPeople().Any());
            Assert.True(_contextService.GetAllPeopleSearcher().Any());
            Assert.True(_contextService.GetAllUsers().Any());
            Assert.True(_contextService.GetAllUsersSearcher().Any());
            Assert.True(_contextService.GetAllDtoUsers().Any());
            Assert.True(_contextService.GetAllDtoUsersSearcher().Any());
            Assert.NotNull(_contextService.GetDtoUser(userEntity.Id));
        }

        [Fact]
        public async Task GetAsync()
        {
            _contextService.CreatePerson();
            var userEntity = _contextService.CreateUser();
            Assert.True((await _contextService.GetAllPeopleAsync()).Any());
            Assert.True((await _contextService.GetAllPeopleSearcherAsync()).Any());
            Assert.True((await _contextService.GetAllUsersAsync()).Any());
            Assert.True((await _contextService.GetAllUsersSearcherAsync()).Any());
            Assert.True((await _contextService.GetAllDtoUsersAsync()).Any());
            Assert.True((await _contextService.GetAllDtoUsersSearcherAsync()).Any());
            Assert.NotNull(await _contextService.GetDtoUserAsync(userEntity.Id));
        }

        [Fact]
        public void DeletedTest()
        {
            var personEntity = _contextService.CreatePerson();
            Assert.NotNull(_contextService.Get(false).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.NotNull(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.True(_contextService.Delete<PersonEntity>(personEntity.Id));
            Assert.Null(_contextService.Get(false).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.Null(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == personEntity.Id));

            var userEntity = _contextService.CreateUser();
            Assert.Equal(userEntity.Id, _contextService.GetDtoUser(userEntity.Id)?.UserId);
            Assert.Throws<InvalidCastException>(() => _contextService.Delete<UserEntity>(userEntity.Id));
        }

        [Fact]
        public async Task DeletedTestAsync()
        {
            var personEntity = _contextService.CreatePerson();
            Assert.NotNull(_contextService.Get(false).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.NotNull(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.True(await _contextService.DeleteAsync<PersonEntity>(personEntity.Id));
            Assert.Null(_contextService.Get(false).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == personEntity.Id));
            Assert.Null(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == personEntity.Id));

            var userEntity = _contextService.CreateUser();
            Assert.Equal(userEntity.Id, (await _contextService.GetDtoUserAsync(userEntity.Id))?.UserId);
            await Assert.ThrowsAsync<InvalidCastException>(() => _contextService.DeleteAsync<UserEntity>(userEntity.Id));
        }

    }
}