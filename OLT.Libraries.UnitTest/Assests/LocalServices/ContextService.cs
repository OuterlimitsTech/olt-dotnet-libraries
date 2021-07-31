using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity;
using OLT.Libraries.UnitTest.Assests.Entity.Models;
using OLT.Libraries.UnitTest.Assests.Models;

namespace OLT.Libraries.UnitTest.Assests.LocalServices
{
    public class ContextService : BaseEntityService, IContextService
    {
        public ContextService(IOltServiceManager serviceManager, SqlDatabaseContext context) : base(serviceManager, context)
        {
        }

        public PersonAutoMapperModel Get()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameLast = Faker.Name.Last()
            };

            Context.People.Add(entity);
            SaveChanges();

            var queryable = Get(new OltSearcherGetAll<PersonEntity>());

            return Get<PersonEntity, PersonAutoMapperModel>(queryable);
        }
    }
}