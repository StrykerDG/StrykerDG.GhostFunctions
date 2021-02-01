using StrykerDG.GhostFunctions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerDG.GhostFunctions.DTOs.Tags
{
    public class TagEvent : IGhostEvent<Tag>
    {
        public Tag Current { get; set; }
        public Tag Previous { get; set; }
    }
}
