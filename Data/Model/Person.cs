//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.FranchisePositions = new HashSet<FranchisePosition>();
        }
    
        public int Personid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CommonName { get; set; }
        public string Suffix { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string mod_usr { get; set; }
        public System.DateTime mod_date { get; set; }
        public Nullable<System.DateTime> DOD { get; set; }
        public string SportTypeGroupCode { get; set; }
        public Nullable<System.DateTime> StartDt { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FranchisePosition> FranchisePositions { get; set; }
        public virtual SportTypeGroup SportTypeGroup { get; set; }
    }
}
