//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventManager
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Registrations = new HashSet<Registration>();
        }
    
        public int event_ID { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string logoPath { get; set; }
        public string description { get; set; }
        public int location_ID { get; set; }
        public System.DateTime startDateTime { get; set; }
        public System.DateTime endDateTime { get; set; }
    
        public virtual Location Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
