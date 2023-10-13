namespace LW3
{
    internal class LW3
    {
        static void Main()
        {
            Console.WriteLine("Kill me please");
        }

        interface IUpdateable
        {
            public abstract void Update(TimeSpan dT);
        }

        class Airport : IUpdateable
        {
            public (int, int) Location = ( 0, 0 );
            public string Name { get; private set; } = "Unnamed Airport";
            public List<Gate> Gates { get; private set; } = new List<Gate>();
            public List<Flight> Schedule { get; private set; } = new List<Flight>();

            public void Update(TimeSpan dT)
            {

            }
        }
        class Gate
        {
            int Number = 0;
            public Queue<Flight> Flights = new();
        }
        class Plane : IUpdateable
        {
            public string Model { get; init; } = string.Empty;
            public int MaxVelocity { get; init; } = 900;
            public Airport Destination = new();

            public void Update(TimeSpan dT)
            {

            }
        }
        class PassengerPlane : Plane
        {
            public int Capacity { get; init; } = 200;
            public List<Passenger> Passengers { get; private set; } = new List<Passenger>();
        }
        class FreightPlane : Plane
        {

        }
        class Flight
        {
            public Plane Plane = new();

            public Airport Departure = new();
            public Gate DepartureGate = new();
            public DateTime DepartureTime = new();

            public Airport Destination = new();
            public DateTime ArrivalTime = new();
        }
        class Passenger
        {
            public string Name = string.Empty;
            public string Surname = string.Empty;
            public int PassNumber = 0;
            public List<Ticket> Tickets = new List<Ticket>();

            public Passenger(string name, string surname, int passnumber)
            {
                Name = name;
                Surname = surname;
                PassNumber = passnumber;
            }
        }
        class Ticket
        {
            public Flight Flight { get; set; } = new();
            public int PassNumber { get; init; }

            public Ticket(Flight flight, int passNumber)
            {
                Flight = flight;
                PassNumber = passNumber;
            }
        }
    }
}