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
    
    public partial class missinglists
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public missinglists()
        {
            this.missinglistdetails = new HashSet<missinglistdetails>();
        }
    
        public int ID { get; set; }
        public int CREATED_BY { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<missinglistdetails> missinglistdetails { get; set; }
    }
}