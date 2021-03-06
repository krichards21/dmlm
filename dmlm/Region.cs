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
    
    public partial class Region
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Region()
        {
            this.RegionUsers = new HashSet<RegionUser>();
            this.RegionLocations = new HashSet<RegionLocation>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int serviceProviderId { get; set; }
        public int territoryId { get; set; }
    
        public virtual ServiceProvider ServiceProvider { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegionUser> RegionUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegionLocation> RegionLocations { get; set; }
        public virtual Territory Territory { get; set; }
    }
}
