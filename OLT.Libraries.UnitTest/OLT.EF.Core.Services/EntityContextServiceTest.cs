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
        public void Get()
        {
            var model = _contextService.CreatePerson();
            Assert.True(model.PersonId > 0);
        }

        [Fact]
        public void QueryableSearchers()
        {
            var model = _contextService.CreatePerson();
            var result = _contextService.Get(new PersonFirstNameStartsWithSearcher(model.Name.First), new PersonLastNameStartsWithSearcher(model.Name.Last));
            Assert.Equal(model.PersonId, result.Id);
        }

        [Fact]
        public void QueryableSearcher()
        {
            var model = _contextService.CreatePerson();
            var result = _contextService.Get(new OltSearcherGetById<PersonEntity>(model.PersonId.Value));
            Assert.Equal(model.PersonId, result.Id);
        }

        [Fact]
        public void GetAll()
        {
            _contextService.CreatePerson();
            _contextService.CreateUser();
            Assert.True(_contextService.GetAllPeople().Any());
            Assert.True(_contextService.GetAllUsers().Any());
        }

        [Fact]
        public async Task GetAllAsync()
        {
            _contextService.CreatePerson();
            _contextService.CreateUser();
            Assert.True((await _contextService.GetAllPeopleAsync()).Any());
            Assert.True((await _contextService.GetAllUsersAsync()).Any());
        }

        [Fact]
        public void DeletedTest()
        {
            var model = _contextService.CreatePerson();
            Assert.True(_contextService.Delete<PersonEntity>(model.PersonId.Value));
        }

        [Fact]
        public async Task DeletedTestAsync()
        {
            var model = _contextService.CreatePerson();
            Assert.True(await _contextService.DeleteAsync<PersonEntity>(model.PersonId.Value));
        }


        [Fact]
        public void DeletedFailureTest()
        {
            var model = _contextService.CreateUser();
            Assert.Throws<InvalidCastException>(() => _contextService.Delete<UserEntity>(model.Id));
        }

        [Fact]
        public void DeletedFailureTestAsync()
        {
            var model = _contextService.CreateUser();
            Assert.ThrowsAsync<InvalidCastException>(() => _contextService.DeleteAsync<UserEntity>(model.Id));
        }
    }
}