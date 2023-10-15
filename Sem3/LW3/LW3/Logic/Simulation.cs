namespace LW3.Logic
{
    [Serializable]
    class Simulation
    {
        public static Rectangle Bounds { get; set; } = new(0, 0, 200, 200);
        public TimeSpan UpdateInterval { get; set; } = new(0, 0, 0, 0, 100);
        public List<Airport> Airports { get; init; } = new();
        public List<Plane> Planes { get; init; } = new();
        public void InitializeExample()
        {
            Airports.Clear();
            Planes.Clear();

            static Rectangle randomRect(int x, int y)
            {
                return new(x - 30, y - 30, 60, 60);
            }

            Airport newyork = new(randomRect(250, 100)) { Name = "Нью-Йорк" };
            Airport berlin = new(randomRect(900, 100)) { Name = "Берлин" };
            Airport moscow = new(randomRect(250, 500)) { Name = "Москва" };
            Airport minsk = new(randomRect(900, 500)) { Name = "Минск" };

            var newyorkPlanes = new List<Plane>() {
                new Plane("Boeing 737", 230),
                new Plane("Boeing 767", 250),
                new Plane("Boeing 777", 270)
            };

            var berlinPlanes = new List<Plane>() {
                new Plane("Airbus A320", 230),
                new Plane("Airbus A330", 240),
                new Plane("Airbus A380", 250)
            };

            var moscowPlanes = new List<Plane>() {
                new Plane("Ту-204", 200),
                new Plane("Ту-214", 220)
            };

            var minskPlanes = new List<Plane>() {
                new Plane("Embraer 195", 250),
                new Plane("Embraer 195-E2", 270),
                new Plane("Embraer 175", 220)
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

            Flight newyorkFlight1 = new(berlin, DateTime.Now, new TimeSpan(0, 0, 2));
            Flight berlinFlight1 = new(moscow, DateTime.Now, new TimeSpan(0, 0, 4));
            Flight berlinFlight2 = new(minsk, DateTime.Now, new TimeSpan(0, 0, 4));
            Flight moscowFlight1 = new(newyork, DateTime.Now, new TimeSpan(0, 0, 3));
            Flight minskFlight1 = new(newyork, DateTime.Now, new TimeSpan(0, 0, 5));
            Flight minskFlight2 = new(moscow, DateTime.Now, new TimeSpan(0, 0, 6));

            newyork.ScheduleFlight(newyorkFlight1);
            berlin.ScheduleFlight(berlinFlight1);
            berlin.ScheduleFlight(berlinFlight2);
            moscow.ScheduleFlight(moscowFlight1);
            minsk.ScheduleFlight(minskFlight1);
            minsk.ScheduleFlight(minskFlight2);

            Airports.Add(newyork);
            Airports.Add(berlin);
            Airports.Add(moscow);
            Airports.Add(minsk);
        }
        public void Update()
        {
            foreach (Airport airport in Airports)
            {
                airport.Update();
            }
            foreach (Plane plane in Planes)
            {
                plane.Update();
            }
        }
    }
}