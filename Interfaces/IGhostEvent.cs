using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerDG.GhostFunctions.Interfaces
{
    public interface IGhostEvent<T> where T: IGhostObject
    {
        [JsonProperty("current")]
        T Current { get; set; }
        [JsonProperty("previous")]
        T Previous { get; set; }
    }
}
