using System;

namespace OLT.Core
{
    public interface IOltEntityAudit : IOltEntity
    {

        DateTimeOffset CreateDate { get; set; }
        string CreateUser { get; set; }
        DateTimeOffset? ModifyDate { get; set; }
        string ModifyUser { get; set; }

    }
}