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

        private bool _servedPassengersWithMeal = false;

        public PassengerPlane(string model, uint velocity) : base(model, velocity) { }
        [JsonConstructor]
        public PassengerPlane() { }

        public override void Unload(Airport airport)
        {
            foreach (var passenger in Passengers)
            {
                airport.AcceptPassenger(passenger);
            }
            Passengers.Clear();
        }
        public void ServePassengersWithMeal()
        {
            foreach(var passenger in Passengers)
            {
                passenger.Satisfaction += 15;
            }
        }

        public override void Update(DateTime currentTime, TimeSpan dT)
        {
            base.Update(currentTime, dT);

            if(Flight != null && currentTime - Flight.DepartureTime > TimeSpan.FromHours(1) && !_servedPassengersWithMeal)
            {
                ServePassengersWithMeal();
                _servedPassengersWithMeal = true;
            }
        }
    }
}
