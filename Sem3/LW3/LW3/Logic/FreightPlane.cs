using System.Text.Json.Serialization;

namespace LW3.Logic
{
    [Serializable]
    public class FreightPlane : Plane
    {
        public List<Cargo> Cargo = new();

        public FreightPlane(string model, uint velocity) : base(model, velocity) { }
        [JsonConstructor]
        public FreightPlane() { }
        public override void Unload(Airport airport)
        {
            foreach (var cargo in Cargo)
            {
                airport.AcceptCargo(cargo);
            }
            Cargo.Clear();
        }
    }
}
