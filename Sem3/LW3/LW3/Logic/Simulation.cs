using System.Numerics;

namespace LW3.Logic
{
    [Serializable]
    class Simulation
    {
        public static Rectangle Bounds = new(0, 0, 200, 200);
        public List<Airport> Airports { get; set; } = new();
        public List<Plane> Planes { get; set; } = new();

        public Simulation() { }
        public void InitializeExample()
        {
            Airports.Clear();
            Planes.Clear();

            Airport airport1 = new(new Vector2(200, 100)) { Name = "Airport One" };
            Airport airport2 = new(new Vector2(800, 100)) { Name = "Airport Two" };
            Airport airport3 = new(new Vector2(500, 500)) { Name = "Airport Three" };
            Airport airport4 = new(new Vector2(1000, 300)) { Name = "Airport Four" };

            var airport1Planes = new List<Plane>() {
                new Plane("Boeing 737"),
                new Plane("Boeing 767"),
                new Plane("Boeing 777")
            };

            var airport2Planes = new List<Plane>() {
                new Plane("Airbus A320"),
                new Plane("Airbus A330"),
                new Plane("Airbus A380")
            };

            var airport3Planes = new List<Plane>() {
                new Plane("Ту-204"),
                new Plane("Ту-214")
            };

            Planes.AddRange(airport1Planes);
            Planes.AddRange(airport2Planes);
            Planes.AddRange(airport3Planes);

            foreach ( Plane plane in airport1Planes)
                airport1.AddPlane(plane);

            foreach (Plane plane in airport2Planes)
                airport2.AddPlane(plane);

            foreach (Plane plane in airport3Planes)
                airport3.AddPlane(plane);

            Flight testFlight1 = new(airport2, DateTime.Now, new TimeSpan(0, 0, 0));
            Flight testFlight2 = new(airport3, DateTime.Now, new TimeSpan(0, 0, 0));
            Flight testFlight3 = new(airport1, DateTime.Now, new TimeSpan(0, 0, 0));

            Flight testFlight4 = new(airport4, DateTime.Now, new TimeSpan(0, 0, 0));
            Flight testFlight5 = new(airport1, DateTime.Now, new TimeSpan(0, 0, 0));
            Flight testFlight6 = new(airport3, DateTime.Now, new TimeSpan(0, 0, 0));

            airport1.ScheduleFlight(testFlight1);
            airport2.ScheduleFlight(testFlight2);
            airport3.ScheduleFlight(testFlight3);

            airport2.ScheduleFlight(testFlight4);
            airport4.ScheduleFlight(testFlight5);
            airport4.ScheduleFlight(testFlight6);

            Airports.Add(airport1);
            Airports.Add(airport2);
            Airports.Add(airport3);
            Airports.Add(airport4);
        }
        public void Update()
        {
            foreach (Airport airport in Airports)
            {
                airport.Update();
            }
            foreach(Plane plane in Planes)
            {
                plane.Update();
            }
        }
    }
}