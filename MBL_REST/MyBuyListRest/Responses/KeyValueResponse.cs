using System.Runtime.Serialization;

namespace MyBuyListRest.Responses
{
    [DataContract]
    public class KeyValueResponse
    {
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public double value { get; set; }
    }
}