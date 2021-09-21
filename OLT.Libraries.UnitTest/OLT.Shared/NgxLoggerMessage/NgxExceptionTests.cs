using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.Shared.NgxLoggerMessage
{
    public class NgxExceptionTests : BaseTest
    {

        public NgxExceptionTests(ITestOutputHelper output) : base(output)
        {

        }

        public static IEnumerable<object[]> NgxLoggerMessageData =>
            new List<object[]>
            {
                new object[] { OltNgxLoggerLevel.Error, true, new HelperNgxExceptionTest() },
                new object[] { OltNgxLoggerLevel.Fatal, true, new HelperNgxExceptionTest() },
                new object[] { OltNgxLoggerLevel.Info, true, new HelperNgxExceptionTest() },
                new object[] { null, true, new HelperNgxExceptionTest() },

                new object[] { OltNgxLoggerLevel.Error, false, new HelperNgxExceptionTest() },
                new object[] { OltNgxLoggerLevel.Fatal, false, new HelperNgxExceptionTest() },
                new object[] { OltNgxLoggerLevel.Info, false, new HelperNgxExceptionTest() },
                new object[] { null, false, new HelperNgxExceptionTest() },


            };

        private Dictionary<string, string> ToDictionary(IDictionary data)
        {
            var dict = new Dictionary<string, string>();
            foreach (DictionaryEntry dictionaryEntry in data)
            {
                dict.Add(dictionaryEntry.Key.ToString() ?? string.Empty, dictionaryEntry.Value?.ToString());
                Logger.Debug("{key} = {value}", dictionaryEntry.Key, dictionaryEntry.Value);
            }
            return dict;
        }


        [Theory]
        [MemberData(nameof(NgxLoggerMessageData))]
        public void NgxLoggerMessage(OltNgxLoggerLevel? level, bool loadDetail, HelperNgxExceptionTest data)
        { 
            var dt = DateTimeOffset.Now;
            
            var msg = data.BuildMessage(dt, level, loadDetail ? data.Detail : null);

            if (!loadDetail && level == OltNgxLoggerLevel.Fatal)
            {
                msg.Additional.Add(new List<OltNgxLoggerDetail>());
            }

            var exception = msg.ToException();
            var exceptionMessage = msg.Additional.FirstOrDefault()?.FirstOrDefault()?.Message ?? msg.Message;
            Assert.Equal(exceptionMessage, exception.Message);

            if (level.HasValue)
            {
                Assert.Equal(level == OltNgxLoggerLevel.Error || level == OltNgxLoggerLevel.Fatal, msg.IsError);
            }

            var dict = ToDictionary(exception.Data);
            if (loadDetail)
            {
                Assert.Collection(dict,
                    item => Assert.Equal(data.Result["Name"], item.Value),
                    item => Assert.Equal(data.Result["AppId"], item.Value),
                    item => Assert.Equal(data.Result["User"], item.Value),
                    item => Assert.Equal(data.Result["Time"], item.Value),
                    item => Assert.Equal(data.Result["Url"], item.Value),
                    item => Assert.Equal(data.Result["Status"], item.Value),
                    item => Assert.Equal(data.Result["Stack"], item.Value),
                    item => Assert.Equal(data.Result["Username"], item.Value),
                    item => Assert.Equal(data.Result["Level"], item.Value),
                    item => Assert.Equal(data.Result["LineNumber"], item.Value),
                    item => Assert.Equal(data.Result["FileName"], item.Value),
                    item => Assert.Equal(data.Result["Timestamp"], item.Value)
                );

                return;
            }


            Assert.Collection(dict,
                item => Assert.Equal(data.Result["Username"], item.Value),
                item => Assert.Equal(data.Result["Level"], item.Value),
                item => Assert.Equal(data.Result["LineNumber"], item.Value),
                item => Assert.Equal(data.Result["FileName"], item.Value),
                item => Assert.Equal(data.Result["Timestamp"], item.Value)
            );

        }


        public static IEnumerable<object[]> NgxLoggerDetailData =>
            new List<object[]>
            {
                new object[] { new HelperNgxExceptionTest() },
                new object[] { new HelperNgxExceptionTest() },
                new object[] { new HelperNgxExceptionTest() },
            };

        [Theory]
        [MemberData(nameof(NgxLoggerDetailData))]
        public void NgxLoggerDetail(HelperNgxExceptionTest data)
        {
            var exception = data.Detail.ToException();
            Assert.Equal(data.Detail.Message, exception.Message);
            Assert.Equal(data.Detail.Id, exception.Source);
            var dict = ToDictionary(exception.Data);

            Assert.Collection(dict,
                item => Assert.Equal(data.Result["Name"], item.Value),
                item => Assert.Equal(data.Result["AppId"], item.Value),
                item => Assert.Equal(data.Result["User"], item.Value),
                item => Assert.Equal(data.Result["Time"], item.Value),
                item => Assert.Equal(data.Result["Url"], item.Value),
                item => Assert.Equal(data.Result["Status"], item.Value),
                item => Assert.Equal(data.Result["Stack"], item.Value)
            );
        }


    }
}