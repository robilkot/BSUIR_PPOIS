using System.Text.Json.Serialization;

namespace LW3.Logic
{
    [Serializable]
    public class Passenger
    {
        public string Name { get; init; } = string.Empty;

        public Airport? CurrentAirport;
        public Airport? Destination
        {
            get => _destination;
            set => _destination = value;
        }
        private Airport? _destination;

        [JsonConstructor]
        public Passenger() { }
        public Passenger(string name, Airport? destination = default)
        {
            Name = name;
            Destination = destination;
        }

        public void Update()
        {
            if (CurrentAirport == null) return;

            var neededPlane = (PassengerPlane)CurrentAirport.LandedPlanes.Find(p => p is PassengerPlane && p?.Flight?.Destination == _destination);
            if (neededPlane != null)
            {
                Board(neededPlane);
            }
        }
        public void Board(PassengerPlane plane)
        {
            if(CurrentAirport == null) return;

            plane.Passengers.Add(this);
            CurrentAirport.Passengers.Remove(this);
            CurrentAirport = null;
        }
    }
}
