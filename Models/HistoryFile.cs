using System;
using Newtonsoft.Json;

namespace mvc.Models
{
	public class HistoryFile
	{
		public HistoryFile()
		{
		}
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("DateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Size")]
        public string Size { get; set; }
        [JsonProperty("Url")]
        public string Url { get; set; }
    }
}

