using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLT.Core
{
    public abstract class OltEntityApplicationLog : IOltEntityApplicationLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [StringLength(50)]
        public virtual string Application { get; set; }

        [StringLength(500)]
        public virtual string CallSite { get; set; }

        public virtual DateTimeOffset Date { get; set; }

        [StringLength(100)]
        public virtual string SourceName { get; set; }

        public virtual int? EventId { get; set; }

        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(100)]
        public virtual string Level { get; set; }

        [StringLength(255)]
        public virtual string Logger { get; set; }

        [StringLength(255)]
        public virtual string MachineName { get; set; }

        [StringLength(1000)]
        public virtual string Url { get; set; }

        [StringLength(100)]
        public virtual string ServerAddress { get; set; }

        [StringLength(100)]
        public virtual string RemoteAddress { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public virtual string Message { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public virtual string Exception { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public virtual string StackTrace { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public virtual string Payload { get; set; }
    }
}