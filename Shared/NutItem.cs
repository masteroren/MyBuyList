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
    
    public partial class NutItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NutItem()
        {
            this.NutValues = new HashSet<NutValue>();
        }
    
        public int NutItemId { get; set; }
        public string NutItemName { get; set; }
        public string DisplayUnit { get; set; }
        public int NutCategoryId { get; set; }
    
        public virtual NutCategory NutCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NutValue> NutValues { get; set; }
    }
}