using System.Collections.Generic;
using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity.Models;
using OLT.Libraries.UnitTest.Assests.LocalServices;
using OLT.Libraries.UnitTest.Assests.Models;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    public class OltEntityContextServiceTest
    {
        private readonly IContextService _contextService;
        
        public OltEntityContextServiceTest(IContextService contextService)
        {
            _contextService = contextService;
        }

        //private OltPersonTestDto createDtoModel()
        //{
        //    return new OltPersonTestDto
        //    {
        //        First = Faker.Name.First(),
        //        Middle = Faker.Name.Middle(),
        //        Last = Faker.Name.Last()
        //    };
        //}

        //private OltPersonTestModel createTestModel()
        //{
        //    return new OltPersonTestModel
        //    {
        //        Name = {
        //            First = Faker.Name.First(),
        //            Middle = Faker.Name.Middle(),
        //            Last = Faker.Name.Last()
        //        }
        //    };
        //}


        [Fact]
        public void Get()
        {
            var model = _contextService.Get();
            Assert.True(model.PersonId > 0);
        }


    }
}