//using OLT.Core;
//using System;
//using System.Text;

//namespace OLT.Libraries.UnitTest.Assets.FileBuilder
//{
//    public class TestCsvBuilderTyped : OltFileBuilder<TestCsvBuilderRequest>
//    {
//        public TestCsvBuilderTyped()
//        {
//            Data = new TestCsvData();
//        }

//        public override string BuilderName => nameof(TestCsvBuilderTyped);
//        public string FileName => $"{BuilderName} [{DateTimeOffset.Now:s}].csv";

//        public TestCsvData Data { get; }

//        protected override IOltFileBase64 Build(TestCsvBuilderRequest request)
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
