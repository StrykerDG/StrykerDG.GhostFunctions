using StrykerDG.GhostFunctions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerDG.GhostFunctions.DTOs.Members
{
    public class MemberEvent : IGhostEvent<Member>
    {
        public Member Current { get; set; }
        public Member Previous { get; set; }
    }
}
