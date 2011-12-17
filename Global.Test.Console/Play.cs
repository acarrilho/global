using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Global.Test.Console
{
    [DataContract(Namespace = "", Name = "play")]
    public class Play
    {
        [DataMember(Name = "asset")]
        public string Asset { get; set; }
        [DataMember(Name = "parameters")]
        public List<Parameter> Parameters { get; set; }
    }
}