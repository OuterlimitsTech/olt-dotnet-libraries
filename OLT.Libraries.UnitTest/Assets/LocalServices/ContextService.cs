using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
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