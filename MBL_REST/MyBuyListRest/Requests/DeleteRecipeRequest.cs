using System.Runtime.Serialization;

namespace MyBuyListRest
{
    [DataContract]
    public class DeleteRecipeRequest
    {
        [DataMember]
        public int recipeId { get; set; }
    }
}