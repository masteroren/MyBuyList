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
    
    public partial class MissingListDetail
    {
        public int ID { get; set; }
        public int LIST_ID { get; set; }
        public int FOOD_ID { get; set; }
        public int MEASUREMENT_UNIT_ID { get; set; }
        public decimal QUANTITY { get; set; }
    
        public virtual Food Food { get; set; }
        public virtual MeasurementUnit MeasurementUnit { get; set; }
        public virtual MissingList MissingList { get; set; }
    }
}
