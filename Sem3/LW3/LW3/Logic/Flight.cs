namespace LW3.Logic
{
    [Serializable]
    class Flight : IComparable<Flight>
    {
        public Airport Destination = new();
        public DateTime DepartureTime = DateTime.Now;
        public TimeSpan Interval = TimeSpan.Zero;
        public bool HasAssignedPlane = false;

        public Flight(Airport destination, DateTime departuteTime, TimeSpan interval = default)
        {
            Destination = destination;
            DepartureTime = departuteTime;
            Interval = interval;
        }

        public void Complete()
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
