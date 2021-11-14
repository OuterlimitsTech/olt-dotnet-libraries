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
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    // ReSharper disable once InconsistentNaming
    public class EntityUniqueIdServiceTest : BaseTest
    {
        private readonly IPersonUniqueIdService _personService;

        public EntityUniqueIdServiceTest(
            IPersonUniqueIdService personService,
            ITestOutputHelper output) : base(output)
        {
            _personService = personService;
        }

        [Fact]
        public void Add()
        {
            var model = UnitTestHelper.CreateTestAutoMapperModel();
            var dto = UnitTestHelper.AddPerson(_personService, model);
            dto.Should().BeEquivalentTo(model, opt => opt.Excluding(t => t.PersonId));

            var model2 = UnitTestHelper.CreateTestAutoMapperModel();
            var dto2 = _personService.Add<PersonDto, PersonAutoMapperModel>(model2);
            Assert.Equal(model2.Name.First, dto2.First);
        }

        [Fact]
        public async Task AddAsync()
        {
            var model = UnitTestHelper.CreateTestAutoMapperModel();
            var dto = await _personService.AddAsync(model);
            dto.Should().BeEquivalentTo(model, opt => opt.Excluding(t => t.PersonId));

            var model2 = UnitTestHelper.CreateTestAutoMapperModel();
            var dto2 = await _personService.AddAsync<PersonDto, PersonAutoMapperModel>(model2);
            Assert.Equal(model2.Name.First, dto2.First);
        }

        [Fact]
        public void AddList()
        {
            var list = new List<PersonAutoMapperModel>
            {
                UnitTestHelper.CreateTestAutoMapperModel(),
                UnitTestHelper.CreateTestAutoMapperModel(),
                UnitTestHelper.CreateTestAutoMapperModel()
            };
            _personService.Add(list).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.PersonId));
            _personService.Add(list.ToArray()).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.PersonId));
            _personService.Add(list.AsEnumerable()).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.PersonId));

            var dtoList = list.Select(s => new PersonDto
            {
                First = s.Name.First,
                Middle = s.Name.Middle,
                Last = s.Name.Last,
                Suffix = s.Name.Suffix,
            });

            _personService.Add<PersonDto, PersonAutoMapperModel>(list).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.PersonId));
            _personService.Add<PersonDto, PersonAutoMapperModel>(list.ToArray()).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.PersonId));
        }

        [Fact]
        public async Task AddListAsync()
        {
            var list = new List<PersonAutoMapperModel>
            {
                UnitTestHelper.CreateTestAutoMapperModel(),
                UnitTestHelper.CreateTestAutoMapperModel(),
                UnitTestHelper.CreateTestAutoMapperModel()
            };
            (await _personService.AddAsync(list)).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.PersonId));
            (await _personService.AddAsync(list.ToArray())).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.PersonId));
            (await _personService.AddAsync(list.AsEnumerable())).Should().BeEquivalentTo(list, opt => opt.Excluding(t => t.PersonId));

            var dtoList = list.Select(s => new PersonDto
            {
                First = s.Name.First,
                Middle = s.Name.Middle,
                Last = s.Name.Last,
                Suffix = s.Name.Suffix,
            });

            (await _personService.AddAsync<PersonDto, PersonAutoMapperModel>(list)).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.PersonId));
            (await _personService.AddAsync<PersonDto, PersonAutoMapperModel>(list.ToArray())).Should().BeEquivalentTo(dtoList, opt => opt.Excluding(t => t.PersonId));
        }

        [Fact]
        public void UpdateAutoMapper()
        {
            var newDto = UnitTestHelper.CreatePersonDto();
            newDto.UniqueId = Guid.NewGuid();
            var added = _personService.Add(newDto);
            added.First = Faker.Name.First();
            var updated = _personService.Update(added.UniqueId.GetValueOrDefault(), added);
            updated.Should().BeEquivalentTo(added);
            Assert.NotEqual(newDto.First, updated.First);
            Assert.Equal(newDto.UniqueId, updated.UniqueId);
        }


        [Fact]
        public void Update()
        {
            var addModel = UnitTestHelper.CreatePersonDto();
            var model = _personService.Add(addModel);
            model.First = Faker.Name.First();
            var updated = _personService.Update<PersonAutoMapperModel, PersonDto>(model.UniqueId.GetValueOrDefault(), model);
            Assert.Equal(model.First, updated.Name.First);
            Assert.NotEqual(addModel.First, updated.Name.First);

            model.First = Faker.Name.First();
            var dto = _personService.Update(model.UniqueId.GetValueOrDefault(), model);
            Assert.Equal(model.First, dto.First);

            model.First = Faker.Name.First();
            dto = _personService.Update(new OltSearcherGetByUid<PersonEntity>(model.UniqueId.GetValueOrDefault()), model);
            Assert.Equal(model.First, dto.First);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var newDto = UnitTestHelper.CreatePersonDto();
            newDto.UniqueId = Guid.NewGuid();

            var added = await _personService.AddAsync(newDto);
            added.First = Faker.Name.First();
            Assert.Equal(newDto.UniqueId, added.UniqueId);

            var updated = await _personService.UpdateAsync<PersonAutoMapperModel, PersonDto>(added.UniqueId.GetValueOrDefault(), added);
            updated.Should().BeEquivalentTo(added);

            Assert.Equal(newDto.UniqueId, updated.UniqueId);

            added.First = Faker.Name.First();
            var dto = await _personService.UpdateAsync(added.UniqueId.GetValueOrDefault(), added);
            Assert.Equal(added.First, dto.First);

            Assert.Equal(newDto.UniqueId, dto.UniqueId);

            added.First = Faker.Name.First();
            dto = await _personService.UpdateAsync(new OltSearcherGetByUid<PersonEntity>(added.UniqueId.GetValueOrDefault()), added);
            Assert.Equal(added.First, dto.First);

            Assert.Equal(newDto.UniqueId, dto.UniqueId);
        }


        [Fact]
        public void SoftDelete()
        {
            var model = _personService.Add(UnitTestHelper.CreatePersonDto());
            Assert.True(_personService.SoftDelete(model.UniqueId.Value));
            Assert.False(_personService.SoftDelete(Guid.NewGuid()));
            model = _personService.Add(UnitTestHelper.CreatePersonDto());
            Assert.True(_personService.SoftDelete(new OltSearcherGetByUid<PersonEntity>(model.UniqueId.Value)));
            Assert.False(_personService.SoftDelete(new OltSearcherGetByUid<PersonEntity>(Guid.NewGuid())));
        }


        [Fact]
        public async Task SoftDeleteAsync()
        {
            var model = await _personService.AddAsync(UnitTestHelper.CreatePersonDto());
            Assert.True(await _personService.SoftDeleteAsync(model.UniqueId.Value));
            Assert.False(await _personService.SoftDeleteAsync(Guid.NewGuid()));
            model = _personService.Add(UnitTestHelper.CreatePersonDto());
            Assert.True(await _personService.SoftDeleteAsync(new OltSearcherGetByUid<PersonEntity>(model.UniqueId.Value)));
            Assert.False(await _personService.SoftDeleteAsync(new OltSearcherGetByUid<PersonEntity>(Guid.NewGuid())));
        }

        [Fact]
        public void Get()
        {
            var expected = _personService.Add(UnitTestHelper.CreatePersonDto());
            var subject = _personService.Get<PersonDto>(expected.UniqueId.GetValueOrDefault());
            subject.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync()
        {
            var expected = _personService.Add(UnitTestHelper.CreatePersonDto());
            var subject = await _personService.GetAsync<PersonDto>(expected.UniqueId.GetValueOrDefault());
            subject.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void GetAll()
        {
            var expected = new List<PersonAutoMapperModel>();
            var filterIds = new List<Guid>();
            for (var idx = 0; idx <= 5; idx++)
            {
                var model = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
                expected.Add(model);
                filterIds.Add(model.UniqueId.GetValueOrDefault());
            }
            var records = _personService.GetAll<PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>()).Where(p => filterIds.Contains(p.UniqueId.GetValueOrDefault())).OrderBy(p => p.PersonId).ToList();
            records.Should().BeEquivalentTo(expected.OrderBy(p => p.PersonId));
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var expected = new List<PersonAutoMapperModel>();
            var filterIds = new List<Guid>();
            for (var idx = 0; idx <= 5; idx++)
            {
                var model = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
                expected.Add(model);
                filterIds.Add(model.UniqueId.GetValueOrDefault());
            }

            var records = await _personService.GetAllAsync<PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>());

            records
                .Where(p => filterIds.Contains(p.UniqueId.GetValueOrDefault()))
                .OrderBy(p => p.PersonId)
                .Should()
                .BeEquivalentTo(expected.OrderBy(p => p.PersonId));
        }

        [Fact]
        public void GetPaged()
        {
            for (var idx = 0; idx <= 500; idx++)
            {
                _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            }
            var pagedParams = new OltPagingParams { Page = 2, Size = 25 };
            var paged = _personService.GetPaged<PersonDto>(new OltSearcherGetAll<PersonEntity>(), pagedParams);
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);
        }

        [Fact]
        public async Task GetPagedAsync()
        {
            for (var idx = 0; idx <= 500; idx++)
            {
                _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            }
            var pagedParams = new OltPagingParams { Page = 2, Size = 25 };
            var paged = await _personService.GetPagedAsync<PersonDto>(new OltSearcherGetAll<PersonEntity>(), pagedParams);
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);
        }

        [Fact]
        public void GetPagedAutoMapper()
        {
            for (var idx = 0; idx <= 500; idx++)
            {
                _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            }
            var pagedParams = new OltPagingParams { Page = 4, Size = 50 };
            var paged = _personService.GetPaged<PersonAutoMapperPagedDto>(new OltSearcherGetAll<PersonEntity>(), pagedParams);
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);
        }

        [Fact]
        public async Task GetPagedAutoMapperAsync()
        {
            for (var idx = 0; idx <= 500; idx++)
            {
                _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            }
            var pagedParams = new OltPagingParams { Page = 4, Size = 50 };
            var paged = await _personService.GetPagedAsync<PersonAutoMapperPagedDto>(new OltSearcherGetAll<PersonEntity>(), pagedParams);
            Assert.True(paged.Data.Count() == pagedParams.Size && paged.Page == pagedParams.Page && paged.Size == pagedParams.Size);
        }

        [Fact]
        public void GetByIdSearcherDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.UniqueId.GetValueOrDefault());
            var result = _personService.Get<PersonDto>(new OltSearcherGetByUid<PersonEntity>(newDto.UniqueId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public async Task GetByIdSearcherDeletedAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            await _personService.SoftDeleteAsync(newDto.UniqueId.GetValueOrDefault());
            var result = await _personService.GetAsync<PersonDto>(new OltSearcherGetByUid<PersonEntity>(newDto.UniqueId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByIdSearcher()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            var result = _personService.Get<PersonDto>(new OltSearcherGetByUid<PersonEntity>(newDto.UniqueId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public async Task GetByIdSearcherAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            var result = await _personService.GetAsync<PersonDto>(new OltSearcherGetByUid<PersonEntity>(newDto.UniqueId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByAllSearcherIncludeDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.UniqueId.GetValueOrDefault());
            var result = _personService.GetAll<PersonDto>(new OltSearcherGetAll<PersonEntity>(true)).FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public async Task GetByAllSearcherIncludeDeletedAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            await _personService.SoftDeleteAsync(newDto.UniqueId.GetValueOrDefault());
            var results = await _personService.GetAllAsync<PersonDto>(new OltSearcherGetAll<PersonEntity>(true));
            var result = results.FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByAllSearcherExcludeDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.UniqueId.GetValueOrDefault());
            var result = _personService.GetAll<PersonDto>(new OltSearcherGetAll<PersonEntity>()).FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByAllSearcherExcludeDeletedAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            await _personService.SoftDeleteAsync(newDto.UniqueId.GetValueOrDefault());
            var results = await _personService.GetAllAsync<PersonDto>(new OltSearcherGetAll<PersonEntity>());
            var result = results.FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Null(result);
        }

        [Fact]
        public void Count()
        {
            var person = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            Assert.Equal(1, _personService.Count(new OltSearcherGetByUid<PersonEntity>(person.UniqueId.Value)));
        }

        [Fact]
        public async Task CountAsync()
        {
            var person = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            Assert.Equal(1, await _personService.CountAsync(new OltSearcherGetByUid<PersonEntity>(person.UniqueId.Value)));
        }
    }
}