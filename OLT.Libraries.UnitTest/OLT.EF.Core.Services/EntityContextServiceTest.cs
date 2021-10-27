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
            Assert.True(model.Id > 0);
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
        public void GetAll()
        {
            _contextService.CreatePerson();
            _contextService.CreateUser();
            Assert.True(_contextService.GetAllPeople().Any());
            Assert.True(_contextService.GetAllPeopleSearcher().Any());
            Assert.True(_contextService.GetAllUsers().Any());
            Assert.True(_contextService.GetAllUsersSearcher().Any());
        }

        [Fact]
        public async Task GetAllAsync()
        {
            _contextService.CreatePerson();
            _contextService.CreateUser();
            Assert.True((await _contextService.GetAllPeopleAsync()).Any());
            Assert.True((await _contextService.GetAllPeopleSearcherAsync()).Any());
            Assert.True((await _contextService.GetAllUsersAsync()).Any());
            Assert.True((await _contextService.GetAllUsersSearcherAsync()).Any());
        }

        [Fact]
        public void DeletedTest()
        {
            var model = _contextService.CreatePerson();
            Assert.NotNull(_contextService.Get(false).FirstOrDefault(p => p.Id == model.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == model.Id));
            Assert.NotNull(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == model.Id));
            Assert.True(_contextService.Delete<PersonEntity>(model.Id));
            Assert.Null(_contextService.Get(false).FirstOrDefault(p => p.Id == model.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == model.Id));
            Assert.Null(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == model.Id));
        }

        [Fact]
        public async Task DeletedTestAsync()
        {
            var model = _contextService.CreatePerson();
            Assert.NotNull(_contextService.Get(false).FirstOrDefault(p => p.Id == model.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == model.Id));
            Assert.NotNull(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == model.Id));
            Assert.True(await _contextService.DeleteAsync<PersonEntity>(model.Id));
            Assert.Null(_contextService.Get(false).FirstOrDefault(p => p.Id == model.Id));
            Assert.NotNull(_contextService.Get(true).FirstOrDefault(p => p.Id == model.Id));
            Assert.Null(_contextService.GetNonDeleted().FirstOrDefault(p => p.Id == model.Id));
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