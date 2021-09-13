using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using Serilog;
using Serilog.Events;
using Xunit.Abstractions;


namespace OLT.Libraries.UnitTest.Abstract
{
    // ReSharper disable once InconsistentNaming
    public class BaseTest : OltDisposable
    {
        protected ILogger Logger { get; }
        protected ITestOutputHelper TestOutputHelper { get; }

        public BaseTest(ITestOutputHelper output)
        {
            TestOutputHelper = output;

            Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output, LogEventLevel.Verbose)
                .CreateLogger()
                .ForContext<BaseTest>();

        }


        protected void SeedPeople(SqlDatabaseContext context)
        {
            for (int i = 0; i < Faker.RandomNumber.Next(50, 70); i++)
            {
                UnitTestHelper.AddPersonWithAddress(context);
            }
            context.SaveChanges();
        }

        protected void SeedUsers(SqlDatabaseContext context)
        {
            for (int i = 0; i < Faker.RandomNumber.Next(50, 70); i++)
            {
                UnitTestHelper.AddUser(context);
            }
            context.SaveChanges();
        }

        protected void SeedBogus(SqlDatabaseContext context)
        {
            for (int i = 0; i < Faker.RandomNumber.Next(5, 15); i++)
            {
                context.BogusNoString.Add(new NoStringPropertiesEntity
                {
                    Value1 = Faker.RandomNumber.Next(10, 5000), 
                    Value2 = Faker.RandomNumber.Next(1, 10000)
                });
            }
            context.SaveChanges();
        }

    }
}