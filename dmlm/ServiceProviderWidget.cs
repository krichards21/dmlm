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
    
    public partial class ServiceProviderWidget
    {
        public int Id { get; set; }
        public int ServiceProviderID { get; set; }
        public int WidgetID { get; set; }
        public string WidgetName { get; set; }
        public Nullable<int> Priority { get; set; }
    
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual Widget Widget { get; set; }
    }
}
