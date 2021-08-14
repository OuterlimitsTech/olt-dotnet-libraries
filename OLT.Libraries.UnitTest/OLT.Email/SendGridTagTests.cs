using Microsoft.Extensions.Configuration;
using OLT.Email;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Email.SendGrid;
using OLT.Libraries.UnitTest.Assets.Models;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using OLT.Email.SendGrid;

namespace OLT.Libraries.UnitTest.OLT.Email
{
    public class SendGridTagTests : BaseTest
    {
        private readonly IConfiguration _configuration;
        private readonly IOltEmailService _emailService;
        private readonly IOltEmailConfigurationSendGrid _sendGridConfig;

        public SendGridTagTests(
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
        public void Tags()
        {
            var templateDto = UnitTestHelper.CreateTagEmailTemplate(UnitTestHelper.CreatePersonDto());
            Assert.Collection(templateDto.Tags,
                item => item.Should().BeEquivalentTo(new OltEmailTag(nameof(TagEmailTemplate.To.ToName.First), templateDto.To.ToName.First)),
                item => item.Should().BeEquivalentTo(new OltEmailTag(nameof(TagEmailTemplate.To.Name), templateDto.To.Name)),
                item => item.Should().BeEquivalentTo(new OltEmailTag(nameof(TagEmailTemplate.Body1), templateDto.Body1)),
                item => item.Should().BeEquivalentTo(new OltEmailTag(nameof(TagEmailTemplate.Body2), templateDto.Body2))
                );            
        }


        [Fact]
        public void ToEmail()
        {
            var templateDto = UnitTestHelper.CreateTagEmailTemplate(UnitTestHelper.CreatePersonDto());
            var compareTo = new OltEmailAddress { Name = templateDto.To.Name, Email = templateDto.To.Email };
            Assert.Equal(templateDto.To.Email, compareTo.Email);
        }

        [Fact]
        public void ToNmae()
        {
            var templateDto = UnitTestHelper.CreateTagEmailTemplate(UnitTestHelper.CreatePersonDto());
            var compareTo = new OltEmailAddress { Name = templateDto.To.Name, Email = templateDto.To.Email };
            Assert.Equal(templateDto.To.ToName.FullName, compareTo.Name);
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
            var templateDto = UnitTestHelper.CreateTagEmailTemplate(UnitTestHelper.CreatePersonDto());
            templateDto.To.Email = _configuration.GetValue<string>("SMTP_TO_ADDRESS") ?? Environment.GetEnvironmentVariable("SMTP_TO_ADDRESS");
            


        }
    }
}