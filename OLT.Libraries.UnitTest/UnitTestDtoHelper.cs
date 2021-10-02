using System;
using System.Collections.Generic;
using System.IO;
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

        public static PersonEntity AddPerson(ISqlDatabaseContext context)
        {
            var entity = PersonEntity.FakerEntity();
            context.People.Add(entity);
            return entity;
        }

        public static PersonEntity AddPersonWithAddress(ISqlDatabaseContext context)
        {
            var entity = PersonEntity.FakerEntity();
            entity.Addresses = new List<AddressEntity>
            {
                AddressEntity.FakerEntity(),
                AddressEntity.FakerEntity(),
                AddressEntity.FakerEntity()
            };

            context.People.Add(entity);
            return entity;
        }

        public static UserEntity AddUser(ISqlDatabaseContext context)
        {
            var entity = UserEntity.FakerEntity();
            context.Users.Add(entity);
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

        public static UserModel CreateUserModel()
        {
            return new UserModel
            {
                UserGuid = Guid.NewGuid(),
                Name = 
                {
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

        public static string BuildTempPath(string rootDir)
        {
            var tempDir = Path.Combine(Path.GetTempPath(), rootDir, $"OLT_UnitTest_{Guid.NewGuid()}");
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            return tempDir;
        }

        public static string BuildTempPath()
        {
            var tempDir = Path.Combine(Path.GetTempPath(), $"OLT_UnitTest_{Guid.NewGuid()}");
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            return tempDir;
        }
    }
}
