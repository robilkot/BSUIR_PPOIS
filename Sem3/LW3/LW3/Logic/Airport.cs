using System.Numerics;

namespace LW3.Logic
{
    [Serializable]
    class Airport : IUpdateable
    {
        private Vector2 _location = new();
        public Vector2 Location
        {
            get
            {
                return _location;
            }
            init
            {
                _location = value;
            }
        }
        public string Name { get; set; } = "Unnamed Airport";
        public List<Flight> Schedule { get; private set; } = new();
        public List<Plane> LandedPlanes { get; private set; } = new();

        public Airport()
        {
            _location = new(0,0);
        }
        public Airport(Rectangle bounds)
        {
            _location = new(new Random().Next(bounds.Left, bounds.Right), new Random().Next(bounds.Top, bounds.Bottom));
        }
        public Airport(Vector2 point)
        {
            _location = new(point.X, point.Y);
        }
        public void AddPlane(Plane plane)
        {
            plane.SetLocation(_location);
            LandedPlanes.Add(plane);
        }
        public void RemovePlane()
        {
            if (LandedPlanes.Count == 0) return;
            else LandedPlanes.RemoveAt(0);
        }
        public void RemovePlane(Plane plane)
        {
            LandedPlanes.Remove(plane);
        }

        public void ScheduleFlight(Flight flight)
        {
            Schedule.Add(flight);
        }
        public void RemoveFlight(Flight flight)
        {
            Schedule.Remove(flight);
        }
        public Flight? NextFlight()
        {
            Flight next = Schedule.Aggregate((f1, f2) => f1.DepartureTime < f2.DepartureTime ? f1 : f2);
            return next;
        }
        public void AssignAllPossibleFlights()
        {
            var notAssignedFlights = Schedule.Where(flight => flight.HasAssignedPlane == false);

            foreach(Flight flight in notAssignedFlights) {
                try
                {
                    AssignFlight(flight);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }
        public void AssignFlight(Flight flight)
        {
            if (LandedPlanes.Count == 0)
            {
                throw new Exception("No free planes");
            }
            Plane assignee = LandedPlanes.First(plane => plane.Flight == null);
            flight.HasAssignedPlane = true;

            assignee.Flight = flight;
        }
        public void AcceptPlane(Plane plane)
        {
            plane.Flight?.Complete();
            plane.Flight = null;
            LandedPlanes.Add(plane);
        }
        public void Update()
        {
            LandedPlanes.RemoveAll(plane => plane.Location != _location);
            AssignAllPossibleFlights();
        }
    }
}
