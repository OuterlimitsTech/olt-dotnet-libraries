using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    // ReSharper disable once InconsistentNaming
    public class PersonService : BaseDbEntityIdService<PersonEntity>, IPersonService
    {
        public PersonService(
            IOltServiceManager serviceManager, 
            SqlDatabaseContext context) : base(serviceManager, context)
        {
        }
    }


    public class PersonService2 : OltEntityIdService<SqlDatabaseContext2, PersonEntity>, IPersonService2
    {
        public PersonService2(
            IOltServiceManager serviceManager,
            SqlDatabaseContext2 context) : base(serviceManager, context)
        {
        }
    }
}