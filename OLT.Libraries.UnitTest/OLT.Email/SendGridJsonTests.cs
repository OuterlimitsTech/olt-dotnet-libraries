using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OLT.Email;
using OLT.Email.SendGrid;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Email.SendGrid;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.Email
{
    public class SendGridJsonTests : BaseTest
    {
        private readonly IConfiguration _configuration;
        private readonly IOltEmailService _emailService;
        private readonly IOltEmailConfigurationSendGrid _sendGridConfig;

        public SendGridJsonTests(
            IOltEmailConfigurationSendGrid sendGridConfig,
            IOltEmailService emailService,
            IConfiguration configuration,
            ITestOutputHelper output) : base(output)
        {
            _sendGridConfig = sendGridConfig;
            _configuration = configuration;
            _emailService = emailService;
        }


        [Fact]
        public void CompareRecipients()
        {
            var person = UnitTestHelper.CreatePersonDto();
            var templateDto = UnitTestHelper.CreateJsonEmailTemplate(person, _configuration.GetValue<string>("BUILD_VERSION"));
            var compareTo = new OltEmailAddress { Name = person.FullName, Email = person.Email };
            Assert.Collection(templateDto.To, item => item.Should().BeEquivalentTo(compareTo));
        }


        [Fact]
        public void ApiKey()
        {
            var eval = _configuration.GetValue<string>("SENDGRID_TOKEN") ?? Environment.GetEnvironmentVariable("SENDGRID_TOKEN");
            Assert.Equal(_sendGridConfig.ApiKey, eval);
        }

        [Fact]
        public void SendEmail()
        {
            var templateDto = UnitTestHelper.CreateJsonEmailTemplate(UnitTestHelper.CreatePersonDto(), _configuration.GetValue<string>("BUILD_VERSION"));

            var json = JsonConvert.SerializeObject(templateDto.TemplateData);

            var recipients = 
                templateDto.To.Select(s => new OltEmailAddress
                {
                    Name = s.Name,
                    Email = _configuration.GetValue<string>("SMTP_TO_ADDRESS") ?? Environment.GetEnvironmentVariable("SMTP_TO_ADDRESS")
                }).ToList();

            var result = _emailService.SendEmail(
                new OltEmailTemplateRequestSendGrid
                {
                    EmailUid = Guid.NewGuid(),
                    Recipients = new OltEmailRecipients
                    {
                        To = recipients
                    },
                    TemplateName = _configuration.GetValue<string>("SENDGRID_TMPL_JSON") ?? Environment.GetEnvironmentVariable("SENDGRID_TMPL_JSON"),
                    TemplateData = templateDto.TemplateData
                });

            Assert.True(result.Success);
        }
    }
}