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
    public class NgxExceptionTests : BaseTest
    {
        private readonly long _unixTime;
        private readonly Dictionary<string, string> _result;
        private readonly List<OltNgxLoggerStack> _stack = new List<OltNgxLoggerStack>();
        private readonly OltNgxLoggerDetail _detail;

        public NgxExceptionTests(ITestOutputHelper output) : base(output)
        {
            _unixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            _result = new Dictionary<string, string>
            {
                {"Name", "Test Name"},
                {"AppId", "Test App"},
                {"User", "Test User"},
                {"Time", DateTimeOffset.FromUnixTimeMilliseconds(_unixTime).ToISO8601()},
                {"Url", "http://test.com"},
                {"Status", "Test 401"}
            };
    
            _stack.Add(new OltNgxLoggerStack
            {
                ColumnNumber = 100,
                LineNumber = 1000,
                FileName = "Test File Name",
                FunctionName = "Test Function Name",
                Source = "Test Source"
            });

            _result.Add("Stack", string.Join(Environment.NewLine, _stack.Select(s => $"{s}{Environment.NewLine}{Environment.NewLine}").ToList()));

            _detail = new OltNgxLoggerDetail
            {
                Id = "Test Id",
                AppId = _result["AppId"],
                Message = "Test Message",
                Name = _result["Name"],
                Status = _result["Status"],
                Time = _unixTime,
                Url = _result["Url"],
                User = _result["User"],
                Stack = _stack
            };
        }

        [Fact]
        public void NgxLoggerMessage()
        { 
            var dt = DateTimeOffset.Now;

            var result = new Dictionary<string, string>(_result);

            var msg = new OltNgxLoggerMessage
            {
                Message = "Test Parent Message",
                Additional = new List<List<OltNgxLoggerDetail>>
                {
                    new List<OltNgxLoggerDetail>
                    {
                        _detail
                    }
                },
                Timestamp = dt,
                Level = OltNgxLoggerLevel.Error,
                FileName = "Test File Name",
                LineNumber = "2940",
            };

            result.Add("Username", msg.Username);
            result.Add("Level", msg.Level.ToString());
            result.Add("LineNumber", msg.LineNumber);
            result.Add("FileName", msg.FileName);
            result.Add("Timestamp", msg.Timestamp?.ToISO8601());


            var exception = msg.ToException();
            Assert.Equal(msg.Additional.FirstOrDefault()?.FirstOrDefault()?.Message, exception.Message);


            var dict = new Dictionary<string, string>();
            foreach (DictionaryEntry dictionaryEntry in exception.Data)
            {
                dict.Add(dictionaryEntry.Key.ToString() ?? string.Empty, dictionaryEntry.Value?.ToString());
                Logger.Debug("{key} = {value}", dictionaryEntry.Key, dictionaryEntry.Value);
            }

            Assert.Collection(dict,
                item => Assert.Equal(result["Name"], item.Value),
                item => Assert.Equal(result["AppId"], item.Value),
                item => Assert.Equal(result["User"], item.Value),
                item => Assert.Equal(result["Time"], item.Value),
                item => Assert.Equal(result["Url"], item.Value),
                item => Assert.Equal(result["Status"], item.Value),
                item => Assert.Equal(result["Stack"], item.Value),
                item => Assert.Equal(result["Username"], item.Value),
                item => Assert.Equal(result["Level"], item.Value),
                item => Assert.Equal(result["LineNumber"], item.Value),
                item => Assert.Equal(result["FileName"], item.Value),
                item => Assert.Equal(result["Timestamp"], item.Value)
            );

        }

        [Fact]
        public void NgxLoggerDetail()
        {

            var exception = _detail.ToException();
            Assert.Equal(_detail.Message, exception.Message);
            Assert.Equal(_detail.Id, exception.Source);

            var dict = new Dictionary<string, string>();
            foreach (DictionaryEntry dictionaryEntry in exception.Data)
            {
                dict.Add(dictionaryEntry.Key.ToString() ?? string.Empty, dictionaryEntry.Value?.ToString());
                Logger.Debug("{key} = {value}", dictionaryEntry.Key.ToString(), dictionaryEntry.Value?.ToString());
            }

            Assert.Collection(dict,
                item => Assert.Equal(_result["Name"], item.Value),
                item => Assert.Equal(_result["AppId"], item.Value),
                item => Assert.Equal(_result["User"], item.Value),
                item => Assert.Equal(_result["Time"], item.Value),
                item => Assert.Equal(_result["Url"], item.Value),
                item => Assert.Equal(_result["Status"], item.Value),
                item => Assert.Equal(_result["Stack"], item.Value)
            );
            
        }


    }
}