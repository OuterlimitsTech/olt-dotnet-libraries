using OLT.Libraries.UnitTest.Assets.Email.SendGrid;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest
{
    public static partial class UnitTestHelper
    {
        public static PersonDto CreatePersonDto()
        {
            return new PersonDto
            {
                First = Faker.Name.First(),
                Middle = Faker.Name.Middle(),
                Last = Faker.Name.Last(),
                Email = Faker.Internet.Email()
            };
        }

        public static TagEmailTemplate CreateTagEmailTemplate(PersonDto person)
        {

            return new TagEmailTemplate
            {
                To = new EmailTemplateAddress
                {
                    ToName = new Core.OltPersonName
                    {
                        First = person.First,
                        Middle = person.Middle,
                        Last = person.Last,

                    },
                    Email = person.Email,
                },
                Body1 = Faker.Lorem.Sentence(50),
                Body2 = Faker.Lorem.Sentence(100)
            };
        }

    }
}
