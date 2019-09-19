using System;
using System.Collections.Generic;

namespace MyBuyListShare.Models
{
    public class RecipeModel
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public List<string> categories { get; set; }
        public string publishedBy { get; set; }
        public List<IngrediantModel> ingrediants { get; set; }
        public string preparationMethod { get; set; }
        public string token { get; set; }
        public string tools { get; set; }
        public DateTime createDate { get; set; }
        public int servings { get; set; }
        public int? prepTime { get; set; }
        public int? cookTime { get; set; }
        public int? level { get; set; }
        public string tags { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string videoLink { get; set; }
        public byte[] picture { get; set; }
        public DateTime modifyDate { get; set; }
        public bool isPublic { get; set; }
    }

    public class RecipeResponse
    {
        public RecipeModel recipe { get; set; }
    }

    public class RecipesResults
    {
        public MetaData metadata { get; set; }
        public RecipeModel[] results { get; set; }
    }
}