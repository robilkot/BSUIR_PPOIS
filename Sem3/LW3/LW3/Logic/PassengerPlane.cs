using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LW3.Logic
{
    [Serializable]
    public class PassengerPlane : Plane
    {
        public List<Passenger> Passengers = new();

        public PassengerPlane(string model, uint velocity) : base(model, velocity) { }
        [JsonConstructor]
        public PassengerPlane() { }
    }
}
