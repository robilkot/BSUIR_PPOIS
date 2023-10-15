using System.Text.Json.Serialization;

namespace LW3.Logic
{
    [Serializable]
    class Flight : IComparable<Flight>
    {
        public Airport? Destination { get; set; } = null;
        public DateTime DepartureTime { get; set; } = DateTime.Now;
        public TimeSpan Interval { get; set; } = TimeSpan.Zero;
        public bool HasAssignedPlane { get; set; } = false;

        [JsonConstructor]
        public Flight() { }
        public Flight(Airport destination, DateTime departuteTime, TimeSpan interval = default)
        {
            Destination = destination;
            DepartureTime = departuteTime;
            Interval = interval;
        }
        public Flight(Flight other)
        {
            Destination = other.Destination;
            DepartureTime = other.DepartureTime;
            Interval = other.Interval;
            HasAssignedPlane = other.HasAssignedPlane;
        }

        public void Next()
        {
            DepartureTime += Interval;
            HasAssignedPlane = false;
        }
        public int CompareTo(Flight? other)
        {
            if (other is Flight flight)
            {
                return flight.DepartureTime < this.DepartureTime ? -1
                    : flight.DepartureTime > this.DepartureTime ? 1
                    : 0;
            }
            return 0;
        }
    }
}
