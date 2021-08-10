using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using OLT.Core;
using OLT.Email;
using OLT.Libraries.UnitTest.Abstract;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.Email
{
    public class SmtpTests : BaseTest
    {
        private readonly IConfiguration _configuration;

        public SmtpTests(
            IConfiguration configuration,
            ITestOutputHelper output) : base(output)
        {
            _configuration = configuration;
        }

        [Fact]
        public void EnvironmentSmtpPasswordVariable()
        {
            var value = _configuration.GetValue<string>("SMTP_PASSWORD") ?? Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            Assert.True(value?.Length > 0, "SMTP Password Missing");
        }

        [Fact]
        public void EnvironmentSmtpUserVariable()
        {
            var value = _configuration.GetValue<string>("SMTP_USERNAME") ?? Environment.GetEnvironmentVariable("SMTP_USERNAME");
            Assert.True(value?.Length > 0, "SMTP Username Missing");
        }


        [Fact]
        public void SendTestEmail()
        {
            var smtpEmail = new OltSmtpEmail
            {
                Subject = "Unit Test Email",
                To = new List<IOltEmailAddress>
                {
                    new OltEmailAddress
                    {
                        Name = "Chris Straw",
                        Email = _configuration.GetValue<string>("SMTP_TO_ADDRESS") ?? Environment.GetEnvironmentVariable("SMTP_TO_ADDRESS")
                    }
                },
                From = new OltEmailAddress
                {
                    Name = "Unit Test",
                    Email = _configuration.GetValue<string>("SMTP_FROM_ADDRESS") ?? Environment.GetEnvironmentVariable("SMTP_FROM_ADDRESS")
                },
                SmtpConfiguration = new OltSmtpConfiguration
                {
                    EnableSsl = true,
                    Server = _configuration.GetValue<string>("SMTP_HOST") ?? Environment.GetEnvironmentVariable("SMTP_HOST"),
                    Port = (_configuration.GetValue<string>("SMTP_PORT") ?? Environment.GetEnvironmentVariable("SMTP_PORT")).ToInt(587),
                    Username = _configuration.GetValue<string>("SMTP_USERNAME") ?? Environment.GetEnvironmentVariable("SMTP_USERNAME"),
                    Password = _configuration.GetValue<string>("SMTP_PASSWORD") ?? Environment.GetEnvironmentVariable("SMTP_PASSWORD")
                }
            };


            try
            {
                Assert.True(smtpEmail.OltEmail("This is a test email", true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}