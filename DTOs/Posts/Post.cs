using Newtonsoft.Json;
using StrykerDG.GhostFunctions.DTOs.Tags;
using StrykerDG.GhostFunctions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerDG.GhostFunctions.DTOs.Posts
{
    public class Post : IGhostObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("authors")]
        public List<Author> Authors { get; set; }
        [JsonProperty("primary_author")]
        public Author PrimaryAuthor { get; set; }
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
        [JsonProperty("primary_tag")]
        public Tag PrimaryTag { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
