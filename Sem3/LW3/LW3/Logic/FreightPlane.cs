using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LW3.Logic
{
    [Serializable]
    public class FreightPlane : Plane
    {
        public List<Cargo> Cargo = new();

        public FreightPlane(string model, uint velocity) : base(model, velocity) { }
        [JsonConstructor]
        public FreightPlane() { }
    }
}
