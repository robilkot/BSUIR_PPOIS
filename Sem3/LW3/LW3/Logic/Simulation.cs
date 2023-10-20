using System.Text.Json.Serialization;

namespace LW3.Logic
{
    [Serializable]
    public class Simulation
    {
        public Rectangle Bounds { get; set; } = new(0, 0, 200, 200);
        public DateTime VirtualBeginTime { get; init; } = DateTime.Now;
        public DateTime VirtualCurrentTime { get => _virtualCurrentTime; init => _virtualCurrentTime = value; }
        private DateTime _virtualCurrentTime = DateTime.Now;
        public DateTime VirtualLastUpdateTime { get => _virtualLastUpdateTime; init => _virtualLastUpdateTime = value; }
        private DateTime _virtualLastUpdateTime = DateTime.Now;
        //public DateTime LastUpdateTime { get => _lastUpdateTime; init => _lastUpdateTime = value; }
        private DateTime _lastUpdateTime = DateTime.Now;

        public float TimeScale = 1;
        public TimeSpan UpdateInterval { get; set; } = new(0, 0, 0, 0, 15);
        public List<Plane> Planes { get; set; } = new();
        public List<Airport> Airports { get; set; } = new();
        public List<Passenger> Passengers { get; set; } = new();

        [JsonConstructor]
        public Simulation() { }
        public Simulation(DateTime beginTime = default)
        {
            VirtualBeginTime = beginTime;
            _virtualCurrentTime = VirtualBeginTime;
            _virtualLastUpdateTime = VirtualBeginTime;
        }
        public void InitializeExample()
        {
            Airports.Clear();
            Planes.Clear();
            Passengers.Clear();

            static Rectangle randomRect(int x, int y)
            {
                return new(x - 30, y - 30, 60, 60);
            }

            Airport newyork = new(randomRect(Bounds.Width / 5, Bounds.Height / 5)) { Name = "Нью-Йорк" };
            Airport berlin = new(randomRect(Bounds.Width / 5 * 4 - 100, Bounds.Height / 5)) { Name = "Берлин" };
            Airport moscow = new(randomRect(Bounds.Width / 5 * 4 - 100, Bounds.Height / 5 * 4 - 100)) { Name = "Москва" };
            Airport minsk = new(randomRect(Bounds.Width / 5, Bounds.Height / 5 * 4 - 100)) { Name = "Минск" };

            // Planes
            var newyorkPlanes = new List<Plane>() {
                new PassengerPlane("Boeing 737", 230),
                new PassengerPlane("Boeing 767", 250),
                new PassengerPlane("Boeing 777", 270)
            };
            var berlinPlanes = new List<Plane>() {
                new PassengerPlane("Airbus A320", 230),
                new PassengerPlane("Airbus A330", 240),
                new PassengerPlane("Airbus A380", 250)
            };
            var moscowPlanes = new List<Plane>() {
                new PassengerPlane("Ту-204", 200),
                new PassengerPlane("Ту-214", 220)
            };
            var minskPlanes = new List<Plane>() {
                new PassengerPlane("Embraer 195", 250),
                new PassengerPlane("Embraer 195-E2", 270),
                new PassengerPlane("Embraer 175", 220)
            };

            Planes.AddRange(newyorkPlanes);
            Planes.AddRange(berlinPlanes);
            Planes.AddRange(moscowPlanes);
            Planes.AddRange(minskPlanes);

            foreach (Plane plane in newyorkPlanes)
                newyork.AddPlane(plane);

            foreach (Plane plane in berlinPlanes)
                berlin.AddPlane(plane);

            foreach (Plane plane in moscowPlanes)
                moscow.AddPlane(plane);

            foreach (Plane plane in minskPlanes)
                minsk.AddPlane(plane);


            // Flights
            Flight newyorkFlight1 = new(berlin, VirtualBeginTime, new TimeSpan(0, 0, 6));
            Flight berlinFlight1 = new(moscow, VirtualBeginTime, new TimeSpan(0, 0, 12));
            Flight berlinFlight2 = new(minsk, VirtualBeginTime, new TimeSpan(0, 0, 12));
            Flight moscowFlight1 = new(newyork, VirtualBeginTime, new TimeSpan(0, 0, 10));
            Flight moscowFlight2 = new(minsk, VirtualBeginTime, new TimeSpan(0, 0, 9));
            Flight minskFlight1 = new(newyork, VirtualBeginTime, new TimeSpan(0, 0, 15));
            Flight minskFlight2 = new(moscow, VirtualBeginTime, new TimeSpan(0, 0, 6));

            newyork.ScheduleFlight(newyorkFlight1);
            berlin.ScheduleFlight(berlinFlight1);
            berlin.ScheduleFlight(berlinFlight2);
            moscow.ScheduleFlight(moscowFlight1);
            moscow.ScheduleFlight(moscowFlight2);
            minsk.ScheduleFlight(minskFlight1);
            minsk.ScheduleFlight(minskFlight2);

            // Passengers
            var pass1 = new Passenger("Joe", minsk);
            var pass2 = new Passenger("Вова", minsk);
            var pass3 = new Passenger("Merkel", berlin);

            Passengers.Add(pass1);
            Passengers.Add(pass2);
            Passengers.Add(pass3);

            berlin.AcceptPassenger(pass1);
            moscow.AcceptPassenger(pass2);
            newyork.AcceptPassenger(pass3);

            Airports.Add(newyork);
            Airports.Add(berlin);
            Airports.Add(moscow);
            Airports.Add(minsk);
        }
        public void Update()
        {
            _virtualCurrentTime += (DateTime.Now - _lastUpdateTime) * TimeScale;

            foreach (Airport airport in Airports)
            {
                airport.Update();
            }
            foreach (Passenger passenger in Passengers)
            {
                passenger.Update();
            }
            foreach (Plane plane in Planes)
            {
                plane.Update(_virtualCurrentTime, _virtualCurrentTime - _virtualLastUpdateTime);
            }

            _virtualLastUpdateTime = _virtualCurrentTime;
            _lastUpdateTime = DateTime.Now;
        }
    }
}