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
        public string ID { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonProperty(PropertyName = "vote")]
        public int Vote { get; set; }

        public string Club92 { get; set; }
        public string Coppers { get; set; }
        public string Buskers { get; set; }

    }
}
