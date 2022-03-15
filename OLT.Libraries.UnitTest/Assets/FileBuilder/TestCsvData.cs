//using System.Collections.Generic;
//using System.Text;

//namespace OLT.Libraries.UnitTest.Assets.FileBuilder
//{
//    public class TestCsvData
//    {
//        public TestCsvData()
//        {
//            CsvString.AppendLine("\"FirstName\",\"LastName\"");
//            for (int i = 0; i < 3; i++)
//            {
//                CsvString.AppendLine($"\"{Faker.Name.First()}\",\"{Faker.Name.Last()}\"");
//            }
//        }

//        public TestCsvData(StringBuilder csvString)
//        {
//            CsvString = csvString;
//        }

//        public StringBuilder CsvString { get; } = new StringBuilder();
//    }
//}
