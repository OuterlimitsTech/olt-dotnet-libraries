using Microsoft.Extensions.DependencyInjection;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.FileBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Builder.File
{
    public class OltFileBuilderTests
    {
        private readonly List<IOltFileBuilder> _builders;
        private readonly IOltFileBuilderManager _fileBuilderManager;

        public OltFileBuilderTests(
            IServiceProvider serviceProvider, 
            IOltFileBuilderManager fileBuilderManager)
        {
            _builders = serviceProvider.GetServices<IOltFileBuilder>().ToList();
            _fileBuilderManager = fileBuilderManager;
        }

        [Fact]
        public void BuildFile()
        {
            var builder1 = new TestCsvBuilder();
            var expectedText = builder1.Data.CsvString.ToString();
            var expected = Convert.ToBase64String(Encoding.ASCII.GetBytes(expectedText));
            var result = builder1.Build(new TestCsvBuilderRequest());
            var csvText = Encoding.ASCII.GetString(Convert.FromBase64String(result.FileBase64));
            Assert.Equal(expectedText, csvText);
            Assert.Equal(expected, result.FileBase64);

            var builder2 = new TestCsvBuilderTyped();
            expectedText = builder2.Data.CsvString.ToString();
            expected = Convert.ToBase64String(Encoding.ASCII.GetBytes(expectedText));
            result = builder2.Build(new TestCsvBuilderRequest());
            csvText = Encoding.ASCII.GetString(Convert.FromBase64String(result.FileBase64));
            Assert.Equal(expectedText, csvText);
            Assert.Equal(expected, result.FileBase64);

        }


        [Fact]
        public void FileBuilderManager()
        {
            var builderName1 = nameof(TestCsvBuilder);
            var builder1 = _builders.FirstOrDefault(p => p.BuilderName == builderName1) as TestCsvBuilder;
            var result = _fileBuilderManager.Generate(new TestCsvBuilderRequest(), builderName1);
            var expectedText = builder1.Data.CsvString.ToString();
            var expected = Convert.ToBase64String(Encoding.ASCII.GetBytes(expectedText));            
            var csvText = Encoding.ASCII.GetString(Convert.FromBase64String(result.FileBase64));

            Assert.Equal(expectedText, csvText);
            Assert.Equal(expected, result.FileBase64);

            var builderName2 = nameof(TestCsvBuilderTyped);
            var builder2 = _builders.FirstOrDefault(p => p.BuilderName == builderName2) as TestCsvBuilderTyped;
            result = _fileBuilderManager.Generate(new TestCsvBuilderRequest(), builderName2);
            expectedText = builder2.Data.CsvString.ToString();
            expected = Convert.ToBase64String(Encoding.ASCII.GetBytes(expectedText));
            csvText = Encoding.ASCII.GetString(Convert.FromBase64String(result.FileBase64));

            Assert.Equal(expectedText, csvText);
            Assert.Equal(expected, result.FileBase64);

            var builderName3 = nameof(TestCsvServiceBuilder);
            var builder3 = _fileBuilderManager.GetBuilders().FirstOrDefault(p => p.BuilderName == builderName3) as TestCsvServiceBuilder;
            result = _fileBuilderManager.Generate(new TestCsvBuilderRequest(), builderName3);
            expectedText = builder3.Data.CsvString.ToString();
            expected = Convert.ToBase64String(Encoding.ASCII.GetBytes(expectedText));
            csvText = Encoding.ASCII.GetString(Convert.FromBase64String(result.FileBase64));


            Assert.Equal(expectedText, csvText);
            Assert.Equal(expected, result.FileBase64);

            Assert.Throws<OltFileBuilderNotFoundException>(() => _fileBuilderManager.Generate(new TestCsvBuilderRequest(), Faker.Lorem.GetFirstWord()));
        }
    }
}
