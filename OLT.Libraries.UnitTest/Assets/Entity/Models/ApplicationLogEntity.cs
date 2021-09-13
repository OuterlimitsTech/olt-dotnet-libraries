using System.ComponentModel.DataAnnotations.Schema;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    // ReSharper disable once InconsistentNaming
    [Table("ApplicationLog", Schema = "General")]
    public class ApplicationLogEntity : OltEntityApplicationLog
    {
        
    }
}