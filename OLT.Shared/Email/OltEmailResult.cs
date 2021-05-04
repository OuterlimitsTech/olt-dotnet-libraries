using System.Collections.Generic;
using System.Linq;

namespace OLT.Email
{
    public class OltEmailResult
    {
        public bool Success => !Errors.Any();
        public List<string> Errors { get; set; } = new List<string>();
        public OltEmailRecipientResult RecipientResults { get; set; }
    }


}
