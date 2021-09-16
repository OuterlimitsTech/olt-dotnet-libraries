using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.True(dto.Name.First.Equals(model.Name.First));
        }

        [Fact]
        public void Add2()
        {
            var model = UnitTestHelper.CreateTestAutoMapperModel();
            var dto = _personService.Add<PersonDto, PersonAutoMapperModel>(model);
            Assert.True(dto.First.Equals(model.Name.First));
        }


        [Fact]
        public void UpdateAutoMapper()
        {
            var addModel = UnitTestHelper.CreatePersonDto();
            var model = _personService.Add(addModel);
            model.First = Faker.Name.First();
            var updated = _personService.Update(model.PersonId.Value, model);
            Assert.True(updated.First.Equals(model.First) && !addModel.First.Equals(updated.First));
        }


        [Fact]
        public void Update()
        {
            var addModel = UnitTestHelper.CreatePersonDto();
            var model = _personService.Add(addModel);
            model.First = Faker.Name.First();
            var updated = _personService.Update<PersonAutoMapperModel, PersonDto>(model.PersonId.Value, model);
            Assert.True(updated.Name.First.Equals(model.First) && !addModel.First.Equals(updated.Name.First));
        }

        [Fact]
        public void SoftDelete()
        {
            var model = _personService.Add(UnitTestHelper.CreatePersonDto());
            Assert.True(_personService.SoftDelete(model.PersonId.Value));
        }

        [Fact]
        public void Get()
        {
            var model = _personService.Add(UnitTestHelper.CreatePersonDto());
            var rec = _personService.Get<PersonAutoMapperModel>(model.PersonId.Value);
            Assert.True(rec.PersonId.Equals(model.PersonId) && rec.Name.First.Equals(model.First));
        }


        [Fact]
        public void GetAll()
        {
            for (var idx = 0; idx <= 5; idx++)
            {
                _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            }
            var records = _personService.GetAll<PersonAutoMapperModel>(new OltSearcherGetAll<PersonEntity>()).ToList();
            Assert.True(records.Any());
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
        public void GetByIdSearcherDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.PersonId.GetValueOrDefault());
            var result = _personService.Get<PersonDto>(new OltSearcherGetById<PersonEntity>(newDto.PersonId.GetValueOrDefault()));
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
        public void GetByAllSearcherIncludeDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.PersonId.GetValueOrDefault());
            var result = _personService.GetAll<PersonDto>(new OltSearcherGetAll<PersonEntity>(true)).FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.Equal(newDto.PersonId, result?.PersonId);
        }

        [Fact]
        public void GetByAllSearcherExcludeDeleted()
        {
            var newDto = _personService.Add(UnitTestHelper.CreateTestAutoMapperModel());
            _personService.SoftDelete(newDto.PersonId.GetValueOrDefault());
            var result = _personService.GetAll<PersonDto>(new OltSearcherGetAll<PersonEntity>()).FirstOrDefault(p => p.PersonId == newDto.PersonId);
            Assert.True(result == null);
        }
    }
}