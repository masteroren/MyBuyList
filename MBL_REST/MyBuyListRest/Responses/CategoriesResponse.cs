using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyBuyListRest.Responses
{
    [DataContract]
    public class CategoriesResponse
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int? parentId { get; set; }
    }
}