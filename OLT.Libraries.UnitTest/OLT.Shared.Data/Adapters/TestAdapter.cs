using OLT.Core;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data.Adapters
{
    public class TestAdapterFromModel
    {
        public string First { get; set; }
    }

    public class TestAdapterToModel
    {
        public string NameFirst { get; set; }
    }

    public class TestAdapter3Model
    {
        public string Name { get; set; }
    }


    public class TestAdapter : OltAdapter<TestAdapterFromModel, TestAdapterToModel>
    {
        public override void Map(TestAdapterFromModel source, TestAdapterToModel destination)
        {
            destination.NameFirst = source.First;
        }

        public override void Map(TestAdapterToModel source, TestAdapterFromModel destination)
        {
            destination.First = source.NameFirst;
        }
    }


    public class TestAdapter2 : OltAdapter<TestAdapterFromModel, TestAdapter3Model>
    {
        public override void Map(TestAdapterFromModel source, TestAdapter3Model destination)
        {
            destination.Name = source.First;
        }

        public override void Map(TestAdapter3Model source, TestAdapterFromModel destination)
        {
            destination.First = source.Name;
        }
    }
}