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
    
    public partial class UserDevice
    {
        public int Id { get; set; }
        public int userDeviceType { get; set; }
        public System.DateTime userDeviceDate { get; set; }
        public int userID { get; set; }
    
        public virtual User User { get; set; }
    }
}
