//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dmlm
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocationHour
    {
        public int Id { get; set; }
        public Nullable<System.TimeSpan> hoursStart { get; set; }
        public Nullable<System.TimeSpan> hoursEnd { get; set; }
        public Nullable<int> locationID { get; set; }
        public string day { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
