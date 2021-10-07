﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    // ReSharper disable once InconsistentNaming
    public class EntityIdServiceTest : BaseTest
    {
        private readonly IPersonService _personService;
        
        public EntityIdServiceTest(
            IPersonService personService, 
            ITestOutputHelper output) : base(output)
        {
            _personService = personService;
        }

        [Fact]
        public void Add1()
        {
            var model = UnitTestHelper.CreateTestAutoMapperModel();
            var dto = UnitTestHelper.AddPerson(_personService, model);
            dto.Should().BeEquivalentTo(model, opt => opt.Excluding(t => t.PersonId));
        }

        [Fact]
        public async Task AddAsync()
        {
            var model = UnitTestHelper.CreateTestAutoMapperModel();
            var dto = await _personService.AddAsync(model);
            dto.Should().BeEquivalentTo(model, opt => opt.Excluding(t => t.PersonId));
        }

        [Fact]
        public void Add2()
        {
            var model = UnitTestHelper.CreateTestAutoMapperModel();
            var dto = _personService.Add<PersonDto, PersonAutoMapperModel>(model);
            Assert.True(dto.First.Equals(model.Name.First));
        }

        [Fact]
        public async Task Add2Async()
        {
            var model = UnitTestHelper.CreateTestAutoMapperModel();
            var dto = await _personService.AddAsync<PersonDto, PersonAutoMapperModel>(model);
            Assert.True(dto.First.Equals(model.Name.First));
        }

        [Fact]
        public void UpdateAutoMapper()
        {
            var addModel = UnitTestHelper.CreatePersonDto();
            var model = _personService.Add(addModel);
            model.First = Faker.Name.First();
            var updated = _personService.Update(model.PersonId.GetValueOrDefault(), model);
            updated.Should().BeEquivalentTo(model);
            Assert.True(updated.First.Equals(model.First) && !addModel.First.Equals(updated.First));
        }


        [Fact]
        public void Update()
        {
            var addModel = UnitTestHelper.CreatePersonDto();
            var model = _personService.Add(addModel);
            model.First = Faker.Name.First();
            var updated = _personService.Update<PersonAutoMapperModel, PersonDto>(model.PersonId.GetValueOrDefault(), model);
            Assert.True(updated.Name.First.Equals(model.First) && !addModel.First.Equals(updated.Name.First));
        }
        
        [Fact]
        public async Task UpdateAsync()
        {
            var addModel = UnitTestHelper.CreatePersonDto();
            var model = await _personService.AddAsync(addModel);
            model.First = Faker.Name.First();
            var updated = await _personService.UpdateAsync<PersonAutoMapperModel, PersonDto>(model.PersonId.GetValueOrDefault(), model);
            Assert.True(updated.Name.First.Equals(model.First) && !addModel.First.Equals(updated.Name.First));
        }

        [Fact]
        public void SoftDelete()
        {
            var model = _personService.Add(UnitTestHelper.CreatePersonDto());
            Assert.True(_personService.SoftDelete(model.PersonId.Value));
        }


        [Fact]
        public async Task SoftDeleteAsync()
        {
            var model = await _personService.AddAsync(UnitTestHelper.CreatePersonDto());
            Assert.True(await _personService.SoftDeleteAsync(model.PersonId.Value));
        }

        [Fact]
        public void Get()
        {
            var expected = _personService.Add(UnitTestHelper.CreatePersonDto());
            var subject = _personService.Get<PersonDto>(expected.PersonId.GetValueOrDefault());
            subject.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync()
        {
            var expected = _personService.Add(UnitTestHelper.CreatePersonDto());
            var subject = await _personService.GetAsync<PersonDto>(expected.PersonId.GetValueOrDefault());
            subject.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void GetAll()
        {
            var expected = new List<PersonAutoMapperModel>();
            var filterIds = new List<int>();
            for (var idx = 0; idx <= 5; idx++)
            {
                var model = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
                expected.Add(model);
                filterIds.Add(model.PersonId.GetValueOrDefault());
            }
            var records = _personService.GetAll<PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>()).Where(p => filterIds.Contains(p.PersonId.GetValueOrDefault())).OrderBy(p => p.PersonId).ToList();
            records.Should().BeEquivalentTo(expected.OrderBy(p => p.PersonId));
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var expected = new List<PersonAutoMapperModel>();
            var filterIds = new List<int>();
            for (var idx = 0; idx <= 5; idx++)
            {
                var model = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
                expected.Add(model);
                filterIds.Add(model.PersonId.GetValueOrDefault());
            }

            var records = await _personService.GetAllAsync<PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>());

            records
                .Where(p => filterIds.Contains(p.PersonId.GetValueOrDefault()))
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
            var pagedParams = new OltPagingParams {Page = 2, Size = 25};
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
            _personService.SoftDelete(newDto.PersonId.GetValueOrDefault());
            var result = _personService.Get<PersonDto>(new OltSearcherGetById<PersonEntity>(newDto.PersonId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public async Task GetByIdSearcherDeletedAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            await _personService.SoftDeleteAsync(newDto.PersonId.GetValueOrDefault());
            var result = await _personService.GetAsync<PersonDto>(new OltSearcherGetById<PersonEntity>(newDto.PersonId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByIdSearcher()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            var result = _personService.Get<PersonDto>(new OltSearcherGetById<PersonEntity>(newDto.PersonId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public async Task GetByIdSearcherAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            var result = await _personService.GetAsync<PersonDto>(new OltSearcherGetById<PersonEntity>(newDto.PersonId.GetValueOrDefault()));
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByAllSearcherIncludeDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.PersonId.GetValueOrDefault());
            var result = _personService.GetAll<PersonDto>(new OltSearcherGetAll<PersonEntity>(true)).FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public async Task GetByAllSearcherIncludeDeletedAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            await _personService.SoftDeleteAsync(newDto.PersonId.GetValueOrDefault());
            var results = await _personService.GetAllAsync<PersonDto>(new OltSearcherGetAll<PersonEntity>(true));
            var result = results.FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByAllSearcherExcludeDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.PersonId.GetValueOrDefault());
            var result = _personService.GetAll<PersonDto>(new OltSearcherGetAll<PersonEntity>()).FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByAllSearcherExcludeDeletedAsync()
        {
            var newDto = await _personService.AddAsync(UnitTestHelper.CreateTestAutoMapperModel());
            await _personService.SoftDeleteAsync(newDto.PersonId.GetValueOrDefault());
            var results = await _personService.GetAllAsync<PersonDto>(new OltSearcherGetAll<PersonEntity>());
            var result = results.FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Null(result);
        }
    }
}