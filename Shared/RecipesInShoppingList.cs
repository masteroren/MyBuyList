//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyBuyList.Shared
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecipesInShoppingList
    {
        public int RECIPE_ID { get; set; }
        public Nullable<int> SERVINGS { get; set; }
        public int USER_ID { get; set; }
    
        public virtual Recipe Recipes { get; set; }
        public virtual User Users { get; set; }
    }
}