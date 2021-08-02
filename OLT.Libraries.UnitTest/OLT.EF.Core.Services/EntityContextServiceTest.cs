using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assests.LocalServices;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.Services
{
    // ReSharper disable once InconsistentNaming
    public class EntityContextServiceTest : BaseTest
    {
        private readonly IContextService _contextService;
        
        public EntityContextServiceTest(IContextService contextService, ITestOutputHelper output) : base(output)
        {
            _contextService = contextService;
        }


        [Fact]
        public void Get()
        {
            var model = _contextService.Get();
            Logger.Information("Testing Log");
            Assert.True(model.PersonId > 0);
        }


    }
}