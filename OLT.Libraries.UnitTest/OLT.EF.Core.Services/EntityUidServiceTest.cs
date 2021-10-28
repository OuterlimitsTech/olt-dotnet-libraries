using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    public class EntityUidServiceTest : BaseTest
    {
        private readonly IPersonService _personService;

        public EntityUidServiceTest(
            IPersonService personService,
            ITestOutputHelper output) : base(output)
        {
            _personService = personService;
        }


        [Fact]
        public void GetByUidSearcher()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            var result = _personService.Get<PersonDto>(new OltSearcherGetByUid<PersonEntity>(newDto.UniqueId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByUidSearcherDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(new OltSearcherGetByUid<PersonEntity>(newDto.UniqueId.GetValueOrDefault()));
            var result = _personService.Get<PersonDto>(new OltSearcherGetByUid<PersonEntity>(newDto.UniqueId.GetValueOrDefault()));
            Assert.Null(result);
        }
    }
}