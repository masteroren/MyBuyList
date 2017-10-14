using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyBuyListRest.Recipes
{
    [DataContract]
    public class IngredientItem
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public double fraction { get; set; }
        [DataMember]
        public string fractionText { get; set; }
        [DataMember]
        public int measureUnit { get; set; }
        [DataMember]
        public string measureUnitText { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public string text { get; set; }
    }

    [DataContract]
    public class RecipeModule
    {
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string comments { get; set; }
        [DataMember]
        public int cookFor { get; set; }
        [DataMember]
        public int cookUnit { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public List<IngredientItem> ingredients { get; set; }
        [DataMember]
        public string instructions { get; set; }
        [DataMember]
        public int level { get; set; }
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string photo { get; set; }
        [DataMember]
        public int prepareFor { get; set; }
        [DataMember]
        public int prepareUnit { get; set; }
        [DataMember]
        public int servings { get; set; }
        [DataMember]
        public bool shared { get; set; }
        [DataMember]
        public string tags { get; set; }
        [DataMember]
        public string tools { get; set; }
        [DataMember]
        public int userId { get; set; }
    }

    [DataContract]
    public class CategoryModule
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<CategoryModule> children { get; set; }
    }


    [DataContract]
    public class AddRecipeRequest
    {
        [DataMember]
        public RecipeModule recipe { get; set; }
        [DataMember]
        public List<CategoryModule> categories { get; set; }
    }
}