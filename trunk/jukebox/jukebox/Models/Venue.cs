//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jukebox.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venue
    {
        public Venue()
        {
            this.VenueTracks = new HashSet<VenueTrack>();
        }
    
        public int VenueID { get; set; }
        public string VenueName { get; set; }
    
        public virtual ICollection<VenueTrack> VenueTracks { get; set; }
        //public virtual ICollection<Venue> Venues { get; set; }
    }
}
