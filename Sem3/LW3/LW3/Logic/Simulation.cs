﻿using System.Text.Json.Serialization;

namespace LW3.Logic
{
    [Serializable]
    class Simulation
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

            static Rectangle randomRect(int x, int y)
            {
                return new(x - 30, y - 30, 60, 60);
            }

            Airport newyork = new(randomRect(Bounds.Width / 5, Bounds.Height / 5)) { Name = "Нью-Йорк" };
            Airport berlin = new(randomRect(Bounds.Width / 5 * 4 - 100, Bounds.Height / 5)) { Name = "Берлин" };
            Airport moscow = new(randomRect(Bounds.Width / 5 * 4 - 100, Bounds.Height / 5 * 4 - 100)) { Name = "Москва" };
            Airport minsk = new(randomRect(Bounds.Width / 5, Bounds.Height / 5 * 4 - 100)) { Name = "Минск" };

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
            foreach (Plane plane in Planes)
            {
                plane.Update(_virtualCurrentTime, _virtualCurrentTime - _virtualLastUpdateTime);
            }

            _virtualLastUpdateTime = _virtualCurrentTime;
            _lastUpdateTime = DateTime.Now;
        }
    }
}