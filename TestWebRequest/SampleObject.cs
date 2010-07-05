using System;
using System.Runtime.Serialization;

namespace TestWebRequest
{
    [DataContract(Namespace = "", Name = "sampleobject")]
    public class SampleObject
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "subobject")]
        public SubObject AnotherObject { get; set; }

        [DataContract(Namespace = "", Name = "subobject")]
        public class SubObject
        {
             [DataMember(Name = "age")]
             public int Age { get; set; }

            [DataMember(Name = "birthdate")]
            public DateTime Birthdate { get; set; }
        }
    }
}
