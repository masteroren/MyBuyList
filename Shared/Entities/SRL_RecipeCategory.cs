using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    public class SRL_RecipeCategory
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public SRL_RecipeCategory(int recipeId, int categoryId, string categoryName)
        {
            this.RecipeId = recipeId;
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
        }
    }
}
