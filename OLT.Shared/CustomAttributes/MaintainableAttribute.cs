using System;

namespace OLT.Core
{
    public class MaintainableAttribute : Attribute
    {

        public OltEntityMaintainable Create { get; set; } = OltEntityMaintainable.NotSet;
        public OltEntityMaintainable Update { get; set; } = OltEntityMaintainable.NotSet;
        public OltEntityMaintainable Delete { get; set; } = OltEntityMaintainable.NotSet;

        public void SetEntityValues(IOltEntityMaintainable entity)
        {
            entity.MaintAdd = ToBool(Create);
            entity.MaintUpdate = ToBool(Update);
            entity.MaintDelete = ToBool(Delete);
        }

        public bool? ToBool(OltEntityMaintainable value)
        {
            return value == OltEntityMaintainable.NotSet ? new bool?() : value == OltEntityMaintainable.Yes;
        }
    }
}