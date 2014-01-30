using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP8ujukeboxClient.ViewModels
{
    class Track
    {
        public string ID { get; set; }

        //[JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        //[JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        //[JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        public int Vote { get; set; }
    }
}
