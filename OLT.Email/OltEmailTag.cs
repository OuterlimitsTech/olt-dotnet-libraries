using System.Collections.Generic;
// ReSharper disable VirtualMemberCallInConstructor

namespace OLT.Email
{
    public class OltEmailTag
    {
        public OltEmailTag() { }

        public OltEmailTag(string tag, string value)
        {
            Tag = tag;
            Value = value;
        }

        public virtual string Tag { get; set; }
        public virtual string Value { get; set; }


        /// <summary>
        /// Converts All Tags to a Dictionary (WARNING: We can only have 1 tag, so this takes the first tag with a name)
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public  static Dictionary<string, string> ToDictionary(List<OltEmailTag> tags)
        {
            var dict = new Dictionary<string, string>();
            tags.ForEach(tag =>
            {
                if (!dict.ContainsKey(tag.Tag))
                {
                    dict.Add(tag.Tag, tag.Value);
                }
            });
            return dict;
        }
    }
}