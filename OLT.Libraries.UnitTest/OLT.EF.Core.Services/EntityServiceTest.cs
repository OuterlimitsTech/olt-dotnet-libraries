using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Libraries.UnitTest.Assets.Searchers;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    public class EntityServiceTest : BaseTest
    {
        private readonly IUserService _service;

        public EntityServiceTest(
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

        [Fact]
        public void AddList()
        {
            var list = new List<UserModel>
            {
                UnitTestHelper.CreateUserModel(),
                UnitTestHelper.CreateUserModel(),
                UnitTestHelper.CreateUserModel()
            };
            _service.Add(list).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.UserId));
            _service.Add(list.ToArray()).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.UserId));
            _service.Add(list.AsEnumerable()).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.UserId));

            var dtoList = list.Select(s => new UserDto
            {
                UserGuid = s.UserGuid,
                First = s.Name.First,
                Middle = s.Name.Middle,
                Last = s.Name.Last,
                Suffix = s.Name.Suffix,
            }).ToList();

            _service.Add<UserDto, UserModel>(list).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.UserId));
            _service.Add<UserDto, UserModel>(list.ToArray()).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.UserId));
        }

        [Fact]
        public async Task AddListAsync()
        {
            var list = new List<UserModel>
            {
                UnitTestHelper.CreateUserModel(),
                UnitTestHelper.CreateUserModel(),
                UnitTestHelper.CreateUserModel()
            };
            (await _service.AddAsync(list)).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.UserId));
            (await _service.AddAsync(list.ToArray())).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.UserId));
            (await _service.AddAsync(list.AsEnumerable())).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.UserId));

            var dtoList = list.Select(s => new UserDto
            {
                UserGuid = s.UserGuid,
                First = s.Name.First,
                Middle = s.Name.Middle,
                Last = s.Name.Last,
                Suffix = s.Name.Suffix,
            }).ToList();

            (await _service.AddAsync<UserDto, UserModel>(list)).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.UserId));
            (await _service.AddAsync<UserDto, UserModel>(list.ToArray())).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.UserId));
        }

        [Fact]
        public void Get()
        {
            var model = _service.Add(UnitTestHelper.CreateUserModel());
            _service.Get<UserModel>(p => p.Id == model.UserId.Value).Should().BeEquivalentTo(model);
            _service.Get<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid)).Should().BeEquivalentTo(model);
            _service.Get<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid), new OltSearcherGetById<UserEntity>(model.UserId.Value)).Should().BeEquivalentTo(model);
        }

        [Fact]
        public async Task GetAsync()
        {
            var model = await _service.AddAsync(UnitTestHelper.CreateUserModel());
            (await _service.GetAsync<UserModel>(p => p.Id == model.UserId.Value)).Should().BeEquivalentTo(model);
            (await _service.GetAsync<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid))).Should().BeEquivalentTo(model);
            (await _service.GetAsync<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid), new OltSearcherGetById<UserEntity>(model.UserId.Value))).Should().BeEquivalentTo(model);
        }


        [Fact]
        public void GetAll()
        {
            var model = _service.Add(UnitTestHelper.CreateUserModel());
            _service.GetAll<UserModel>(p => p.Id == model.UserId.Value).FirstOrDefault().Should().BeEquivalentTo(model);
            _service.GetAll<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid)).FirstOrDefault().Should().BeEquivalentTo(model);
            _service.GetAll<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid), new OltSearcherGetById<UserEntity>(model.UserId.Value)).FirstOrDefault().Should().BeEquivalentTo(model);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var model = await _service.AddAsync(UnitTestHelper.CreateUserModel());
            (await _service.GetAllAsync<UserModel>(p => p.Id == model.UserId.Value)).FirstOrDefault().Should().BeEquivalentTo(model);
            (await _service.GetAllAsync<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid))).FirstOrDefault().Should().BeEquivalentTo(model);
            (await _service.GetAllAsync<UserModel>(new OltSearcherGetByUid<UserEntity>(model.UserGuid), new OltSearcherGetById<UserEntity>(model.UserId.Value))).FirstOrDefault().Should().BeEquivalentTo(model);
        }

        [Fact]
        public void GetPaged()
        {
            for (var idx = 0; idx <= 500; idx++)
            {
                _service.Add(UnitTestHelper.CreateUserModel());
            }
            var pagedParams = new OltPagingParams { Page = 1, Size = 25 };
            var paged = _service.GetPaged<UserModel>(new OltSearcherGetAll<UserEntity>(), pagedParams);
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);

            pagedParams = new OltPagingParams { Page = 3, Size = 50 };
            paged = _service.GetPaged<UserModel>(new OltSearcherGetAll<UserEntity>(), pagedParams, queryable => queryable.OrderBy(p => p.LastName).ThenBy(p => p.Id));
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);
        }


        [Fact]
        public async Task GetPagedAsync()
        {
            for (var idx = 0; idx <= 500; idx++)
            {
                await _service.AddAsync(UnitTestHelper.CreateUserModel());
            }
            var pagedParams = new OltPagingParams { Page = 2, Size = 25 };
            var paged = await _service.GetPagedAsync<UserModel>(new OltSearcherGetAll<UserEntity>(), pagedParams);
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);

            pagedParams = new OltPagingParams { Page = 3, Size = 50 };
            paged = _service.GetPaged<UserModel>(new OltSearcherGetAll<UserEntity>(), pagedParams, queryable => queryable.OrderBy(p => p.LastName).ThenBy(p => p.Id));
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);
        }
    }
}