namespace LW3
{
    internal class Flight : IComparable<Flight>
    {
        public Airport Destination = new();
        public DateTime DepartureTime = new();
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
