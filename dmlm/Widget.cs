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
    
    public partial class Widget
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Widget()
        {
            this.ServiceProviderWidgets = new HashSet<ServiceProviderWidget>();
        }
    
        public int Id { get; set; }
        public string WidgetName { get; set; }
        public Nullable<bool> DefaultWidget { get; set; }
        public Nullable<int> DefaultWidgetPriority { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceProviderWidget> ServiceProviderWidgets { get; set; }
    }
}