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
    
    public partial class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int ServiceProviderWidgetRoleId { get; set; }
    
        public virtual ServiceProviderWidgetRole ServiceProviderWidgetRole { get; set; }
    }
}
