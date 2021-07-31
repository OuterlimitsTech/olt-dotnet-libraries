using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity;
using OLT.Libraries.UnitTest.Assests.Entity.Models;

namespace OLT.Libraries.UnitTest.Assests.LocalServices
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
}