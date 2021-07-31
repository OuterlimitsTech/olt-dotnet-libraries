using OLT.Libraries.UnitTest.Assests.LocalServices;
using OLT.Libraries.UnitTest.Assests.Models;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    public class OltEntityServiceTest
    {
        private readonly IPersonService _personService;

        public OltEntityServiceTest(IPersonService personService)
        {
            _personService = personService;
        }


        [Fact]
        public void Add1()
        {
            var model = new OltPersonTestModel
            {
                Name = {
                    First = Faker.Name.First(), 
                    Middle = Faker.Name.Middle(), 
                    Last = Faker.Name.Last()
                }
            };

            var dto = _personService.Add(model);

            Assert.True(dto.Name.First.Equals(model.Name.First));
        }

        [Fact]
        public void Add2()
        {
            var model = new OltPersonTestModel
            {
                Name = {
                    First = Faker.Name.First(),
                    Middle = Faker.Name.Middle(),
                    Last = Faker.Name.Last()
                }
            };

            var dto = _personService.Add<OltPersonTestDto, OltPersonTestModel>(model);

            Assert.True(dto.First.Equals(model.Name.First));
        }



        [Fact]
        public void Update()
        {
            var addModel = new OltPersonTestDto
            {
                First = Faker.Name.First(),
                Middle = Faker.Name.Middle(),
                Last = Faker.Name.Last()
            };

            var model = _personService.Add(addModel);

            model.First = Faker.Name.First();

            var updated = _personService.Update(model.PersonId.Value, model);

            Assert.True(updated.First.Equals(model.First) && !addModel.First.Equals(updated.First));
        }
    }
}