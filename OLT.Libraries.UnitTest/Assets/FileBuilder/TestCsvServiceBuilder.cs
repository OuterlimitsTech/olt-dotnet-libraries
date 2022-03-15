//using OLT.Core;
//using OLT.Libraries.UnitTest.Assets.Entity;
//using System;
//using System.Text;

//namespace OLT.Libraries.UnitTest.Assets.FileBuilder
//{
//    public class TestCsvServiceBuilder : OltFileBuilderService
//    {
//        public TestCsvServiceBuilder(SqlDatabaseContext context)
//        {
//            Context = context;            
//            var csvString = new StringBuilder();
//            csvString.AppendLine("\"PersonId\",\"FirstName\",\"LastName\"");
//            for (int i = 0; i < 3; i++)
//            {
//                var person = UnitTestHelper.AddPerson(Context);
//                Context.SaveChanges();
//                csvString.AppendLine($"\"{person.Id}\",\"{person.NameFirst}\",\"{person.NameLast}\"");
//            }
//            Data = new TestCsvData(csvString);
//        }
//        private SqlDatabaseContext Context { get; }
//        public override string BuilderName => nameof(TestCsvServiceBuilder);
//        public string FileName => $"{BuilderName} [{DateTimeOffset.Now:s}].csv";
//        public TestCsvData Data { get; }

//        public override IOltFileBase64 Build<TRequest>(TRequest request)
//        {
//            return new OltFileBase64
//            {
//                FileName = this.FileName,
//                ContentType = MimeMapping.MimeUtility.GetMimeMapping(this.FileName),
//                FileBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(Data.CsvString.ToString()))
//            };
//        }
//    }
//}
