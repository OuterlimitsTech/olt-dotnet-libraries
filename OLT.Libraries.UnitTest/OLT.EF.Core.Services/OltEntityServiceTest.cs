using System.Collections.Generic;
using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity.Models;
using OLT.Libraries.UnitTest.Assests.LocalServices;
using OLT.Libraries.UnitTest.Assests.Models;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    public class OltEntityServiceTest
    {
        private readonly IContextService _contextService;
        private readonly IPersonService _personService;
        
        public OltEntityServiceTest(
            IContextService contextService,
            IPersonService personService)
        {
            _contextService = contextService;
            _personService = personService;
        }

        private OltPersonTestDto createDtoModel()
        {
            return new OltPersonTestDto
            {
                First = Faker.Name.First(),
                Middle = Faker.Name.Middle(),
                Last = Faker.Name.Last()
            };
        }

        private OltPersonTestModel createTestModel()
        {
            return new OltPersonTestModel
            {
                Name = {
                    First = Faker.Name.First(),
                    Middle = Faker.Name.Middle(),
                    Last = Faker.Name.Last()
                }
            };
        }

        [Fact]
        public void Add1()
        {
            var model = createTestModel();
            var dto = _personService.Add(model);
            Assert.True(dto.Name.First.Equals(model.Name.First));
        }

        [Fact]
        public void Add2()
        {
            var model = createTestModel();
            var dto = _personService.Add<OltPersonTestDto, OltPersonTestModel>(model);
            Assert.True(dto.First.Equals(model.Name.First));
        }



        [Fact]
        public void Update()
        {
            var addModel = createDtoModel();
            var model = _personService.Add(addModel);
            model.First = Faker.Name.First();
            var updated = _personService.Update(model.PersonId.Value, model);
            Assert.True(updated.First.Equals(model.First) && !addModel.First.Equals(updated.First));
        }


        [Fact]
        public void Update2()
        {
            var addModel = createDtoModel();
            var model = _personService.Add(addModel);
            model.First = Faker.Name.First();
            var updated = _personService.Update<OltPersonTestModel, OltPersonTestDto>(model.PersonId.Value, model);
            Assert.True(updated.Name.First.Equals(model.First) && !addModel.First.Equals(updated.Name.First));
        }

        [Fact]
        public void SoftDelete()
        {
            var model = _personService.Add(createDtoModel());
            Assert.True(_personService.SoftDelete(model.PersonId.Value));
        }

        [Fact]
        public void Get()
        {
            var model = _personService.Add(createDtoModel());
            var rec = _personService.Get<OltPersonTestModel>(model.PersonId.Value);
            Assert.True(rec.PersonId.Equals(model.PersonId) && rec.Name.First.Equals(model.First));
        }


        [Fact]
        public void GetAll()
        {
            for (var idx = 0; idx <= 5; idx++)
            {
                Add1();
            }
            var recs = _personService.GetAll<OltPersonTestModel>(new OltSearcherGetAll<PersonEntity>()).ToList();
            Assert.True(recs.Count() > 0);
        }

    }
}