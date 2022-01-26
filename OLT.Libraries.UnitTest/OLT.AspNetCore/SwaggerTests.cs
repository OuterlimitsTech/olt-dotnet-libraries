using OLT.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{
    public class SwaggerTests
    {

        [Fact]
        public void Options()
        {
            var options = new OltOptionsAspNetSwagger();

            var title = Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ?? 
                        Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product;

            var description = Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description ?? 
                              Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description;

            Assert.False(options.Enabled);
            Assert.Equal(title, options.Title);
            Assert.Equal(description, options.Description);
            Assert.NotNull(options.XmlSettings);
            Assert.False(options.XmlSettings.IncludeControllerXmlComments);
            Assert.Null(options.XmlSettings.CommentsFilePath);

            title = Faker.Lorem.GetFirstWord();
            description = Faker.Lorem.Sentence();

            options = new OltOptionsAspNetSwagger
            {
                Enabled = true,
                Title = title,
                Description = description,
                XmlSettings = new OltOptionsAspNetSwaggerXml
                {
                    IncludeControllerXmlComments = true,
                    CommentsFilePath = AppContext.BaseDirectory
                }
            };

            Assert.Equal(title, options.Title);
            Assert.Equal(description, options.Description);
            Assert.True(options.XmlSettings.IncludeControllerXmlComments);
            Assert.Equal(AppContext.BaseDirectory, options.XmlSettings.CommentsFilePath);
        }      
     }
}
