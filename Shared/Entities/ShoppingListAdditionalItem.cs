//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyBuyList.Shared.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShoppingListAdditionalItem
    {
        public int ShoppingListItemId { get; set; }
        public int MenuId { get; set; }
        public Nullable<int> GeneralItemId { get; set; }
        public string ItemName { get; set; }
    
        public virtual GeneralItem GeneralItem { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
