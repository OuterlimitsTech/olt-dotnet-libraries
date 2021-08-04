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
}