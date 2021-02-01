using StrykerDG.GhostFunctions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerDG.GhostFunctions.DTOs.Pages
{
    public class PageEvent : IGhostEvent<Page>
    {
        public Page Current { get; set; }
        public Page Previous { get; set; }
    }
}
