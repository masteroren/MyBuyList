using System;
using System.Runtime.Serialization;

namespace MyBuyListRest
{
    [DataContract]
    public class RecipeServiceResponse
    {
        [DataMember]
        public Exception ex { get; set; }
        [DataMember]
        public bool status { get; set; }
        [DataMember]
        public int deletedId { get; set; }
    }
}