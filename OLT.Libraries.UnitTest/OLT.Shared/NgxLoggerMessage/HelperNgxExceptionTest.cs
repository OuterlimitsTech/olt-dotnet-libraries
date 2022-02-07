using System;
using System.Collections.Generic;
using System.Linq;
using Faker;
using OLT.Core;

namespace OLT.Libraries.UnitTest.OLT.Shared.NgxLoggerMessage
{
    public class HelperNgxExceptionTest
    {
        public HelperNgxExceptionTest(DateTimeOffset? dt)
        {
            if (dt.HasValue)
            {
                Timestamp = dt.Value;
                UnixTime = dt.Value.ToUnixTimeMilliseconds();
            }
            

            Stack = new List<OltNgxLoggerStack>
            {
                new OltNgxLoggerStack
                {
                    ColumnNumber = Faker.RandomNumber.Next(3),
                    LineNumber = Faker.RandomNumber.Next(4),
                    FileName = Faker.Lorem.GetFirstWord(),
                    FunctionName = Faker.Lorem.GetFirstWord(),
                    Source = Faker.Lorem.Paragraph()
                }
            };


            Result = new Dictionary<string, string>
            {
                { "Name", Faker.Name.FullName(NameFormats.WithSuffix) },
                { "AppId", Faker.Lorem.GetFirstWord() },
                { "User", Faker.Internet.UserName() },
                { "Time", UnixTime.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(UnixTime.Value).ToISO8601() : null },
                { "Url", Faker.Internet.Url() },
                { "Status", Faker.RandomNumber.Next(200, 600).ToString() },
                { "Stack", string.Join(Environment.NewLine,  Stack.Select(s => $"{s}{Environment.NewLine}{Environment.NewLine}").ToList()) }
            };

            Detail = new OltNgxLoggerDetail
            {
                Id = Faker.Lorem.GetFirstWord(),
                AppId = Result["AppId"],
                Message = Faker.Lorem.Sentence(),
                Name = Result["Name"],
                Status = Result["Status"],
                Time = UnixTime,
                Url = Result["Url"],
                User = Result["User"],
                Stack = Stack
            };
        }


        public long? UnixTime { get; }
        public DateTimeOffset? Timestamp { get; }
        public Dictionary<string, string> Result { get; }
        public List<OltNgxLoggerStack> Stack { get; }
        public OltNgxLoggerDetail Detail { get; }


        public OltNgxLoggerMessage BuildMessage(OltNgxLoggerLevel? level, OltNgxLoggerDetail detail)
        {
            var msg = new OltNgxLoggerMessage
            {
                Message = Faker.Lorem.Sentence(),
                Timestamp = Timestamp,
                FileName = Faker.Lorem.GetFirstWord(),
                LineNumber = Faker.RandomNumber.Next(1000, 4000).ToString(),
            };

            msg.Level = level ?? msg.Level;

            if (detail != null)
            {
                msg.Additional = new List<List<OltNgxLoggerDetail>>
                {
                    new List<OltNgxLoggerDetail>
                    {
                        detail
                    }
                };
            }

            Result.Add("Username", msg.GetUsername());
            Result.Add("Level", msg.Level?.ToString());
            Result.Add("LineNumber", msg.LineNumber);
            Result.Add("FileName", msg.FileName);
            Result.Add("Timestamp", msg.Timestamp?.ToISO8601());

            return msg;
        }

    }
}