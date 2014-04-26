using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WP8jukebox
{
    class Track
    {
        public string TrackID { get; set; }
        public string Title { get; set; }      
        public string Artist { get; set; }       
        public string Genre { get; set; }    
        public int Vote { get; set; }
        public string PopBar { get; set; }
        public string PartyClub { get; set; }
        public string RockBar { get; set; }
        public string DanceClub { get; set; }
        //public string Venue { get; set; }
        public string AlternativeBar { get; set; }
        public string PopClub { get; set; }
        public string RnbClub { get; set; }
        public string Admin { get; set; }
    }
}
