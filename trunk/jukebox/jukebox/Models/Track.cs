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
    
    public partial class Track
    {
        public Track()
        {
            this.VenueTracks = new HashSet<VenueTrack>();
        }
    
        public int TrackID { get; set; }
        public string TrackName { get; set; }
    
        public virtual ICollection<VenueTrack> VenueTracks { get; set; }
    }
}
