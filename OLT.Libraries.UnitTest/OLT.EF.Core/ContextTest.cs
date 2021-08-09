using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core
{
    public class ContextTest : BaseTest
    {
        private readonly IOltIdentity _identity;
        private readonly SqlDatabaseContext _context;

        public ContextTest(
            IOltIdentity identity,
            SqlDatabaseContext context,
            ITestOutputHelper output) : base(output)
        {
            _identity = identity;
            _context = context;
        }



        [Fact]
        public void Get()
        {
            var auditUser = _context.AuditUser;
            Assert.True(_identity.GetDbUsername().Equals(auditUser));
        }
    }
}