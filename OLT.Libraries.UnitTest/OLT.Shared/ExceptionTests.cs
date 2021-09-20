using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public class ExceptionTests : BaseTest
    {
        public ExceptionTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void OltNgxLoggerDetail()
        {
            var dt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Dictionary<string, string> result = new Dictionary<string, string>
            {
                {"Name", "Test Name"},
                {"AppId", "Test App"},
                {"User", "Test User"},
                {"Time", DateTimeOffset.FromUnixTimeMilliseconds(dt).ToISO8601()},
                {"Url", "http://test.com"},
                {"Status", "Test 401"}
            };

            
            var msg = new OltNgxLoggerDetail
            {
                Id = "Test Id",
                AppId = result["AppId"],
                Message = "Test Message",
                Name = result["Name"],
                Status = result["Status"],
                Time = dt,
                Url = result["Url"],
                User = result["User"],
                Stack = new List<OltNgxLoggerStack>
                {
                    new OltNgxLoggerStack
                    {
                        ColumnNumber = 100,
                        LineNumber = 1000,
                        FileName = "Test File Name",
                        FunctionName = "Test Function Name",
                        Source = "Test Source"
                    }
                }
            };

            result.Add("Stack", string.Join(Environment.NewLine, msg.Stack.Select(s => $"{s}{Environment.NewLine}{Environment.NewLine}").ToList()));

            var exception = msg.ToException();
            Assert.Equal(msg.Message, exception.Message);
            Assert.Equal(msg.Id, exception.Source);


            var dict = new Dictionary<string, string>();
            foreach (DictionaryEntry dictionaryEntry in exception.Data)
            {
                dict.Add(dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
                Logger.Debug("{key} = {value}", dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
            }

            Assert.Collection(dict,
                item => Assert.Equal(result["Name"], item.Value),
                item => Assert.Equal(result["AppId"], item.Value),
                item => Assert.Equal(result["User"], item.Value),
                item => Assert.Equal(result["Time"], item.Value),
                item => Assert.Equal(result["Url"], item.Value),
                item => Assert.Equal(result["Status"], item.Value),
                item => Assert.Equal(result["Stack"], item.Value)
            );
            
        }


    }
}