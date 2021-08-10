using System.Collections.Generic;
using System.Linq;
using OLT.Core;

namespace OLT.Email
{
    public class OltEmailResult : IOltResult
    {
        public virtual bool Success => !Errors.Any();
        public virtual List<string> Errors { get; set; } = new List<string>();
        public virtual OltEmailRecipientResult RecipientResults { get; set; }
    }


}
