using System;
using System.Collections.Generic;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Email.SendGrid;
using OLT.Libraries.UnitTest.Assets.Email.SendGrid.Json;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest
{
    public static class UnitTestHelper
    {

        public static PersonEntity AddPerson(SqlDatabaseContext context)
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameMiddle = Faker.Name.Middle(),
                NameLast = Faker.Name.Last()
            };
            context.People.Add(entity);
            return entity;
        }

        public static PersonAutoMapperModel AddPerson(IPersonService personService, PersonAutoMapperModel dto)
        {
            return personService.Add(dto);
        }

        public static PersonAutoMapperModel CreateTestAutoMapperModel()
        {
            return new PersonAutoMapperModel
            {
                Name = {
                    First = Faker.Name.First(),
                    Middle = Faker.Name.Middle(),
                    Last = Faker.Name.Last()
                }
            };
        }

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

        public static TagEmailTemplate CreateTagEmailTemplate(PersonDto person, string builderVersion)
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
                BuildVersion = builderVersion ?? Environment.GetEnvironmentVariable("BUILD_VERSION") ?? "[No Build Version]",
                Body1 = Faker.Lorem.Sentence(50),
                Body2 = Faker.Lorem.Sentence(100)
            };
        }

        public static JsonEmailTemplate CreateJsonEmailTemplate(PersonDto person, string builderVersion)
        {
            return new JsonEmailTemplate
            {
                To = new List<EmailTemplateAddress>
                {
                    new EmailTemplateAddress
                    {
                        ToName = new Core.OltPersonName
                        {
                            First = person.First,
                            Middle = person.Middle,
                            Last = person.Last,
                        },
                        Email = person.Email,
                    }
                },
                TemplateData = new EmailDataJson
                {
                    Recipient = new OltPersonName
                    {
                        First = person.First,
                        Middle = person.Middle,
                        Last = person.Last,
                    },
                    Communication = new EmailDataCommunicationJson
                    {
                        Body1 = Faker.Lorem.Sentence(50),
                        Body2 = Faker.Lorem.Sentence(100)
                    },
                    Build = new EmailDataBuildVersionJson
                    {
                        Version = builderVersion ?? Environment.GetEnvironmentVariable("BUILD_VERSION") ?? "[No Build Version]",
                    }
                }
            };
        }
    }
}
