using Newtonsoft.Json;
using StrykerDG.GhostFunctions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerDG.GhostFunctions.DTOs.Tags
{
    public class Tag : IGhostObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
