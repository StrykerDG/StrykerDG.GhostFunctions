using StrykerDG.GhostFunctions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerDG.GhostFunctions.DTOs.Posts
{
    public class PostEvent : IGhostEvent<Post>
    {
        public Post Current { get; set; }
        public Post Previous { get; set; }
    }
}
