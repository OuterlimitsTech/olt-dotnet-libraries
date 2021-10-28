using System.Threading.Tasks;
using FluentAssertions;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    public class EntityEntityServiceTest : BaseTest
    {
        private readonly IUserService _service;

        public EntityEntityServiceTest(
            IUserService service,
            ITestOutputHelper output) : base(output)
        {
            _service = service;
        }


        [Fact]
        public void Add()
        {
            var model = UnitTestHelper.CreateUserModel();
            _service.Add(model).Should().BeEquivalentTo(model, opt => opt.Excluding(t => t.UserId));
            model = UnitTestHelper.CreateUserModel();
            var result = _service.Add<UserDto, UserModel>(model);
            Assert.Equal(model.UserGuid, result.UserGuid);

            
        }

        [Fact]
        public async Task AddAsync()
        {
            var model = UnitTestHelper.CreateUserModel();
            (await _service.AddAsync(model)).Should().BeEquivalentTo(model, opt => opt.Excluding(t => t.UserId));

            model = UnitTestHelper.CreateUserModel();
            var result = await _service.AddAsync<UserDto, UserModel>(model);
            Assert.Equal(model.UserGuid, result.UserGuid);
        }
    }
}