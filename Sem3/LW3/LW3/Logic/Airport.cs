using System.Numerics;
using System.Text.Json.Serialization;

namespace LW3.Logic
{
    [Serializable]
    class Airport : IUpdateable
    {
        private PointF _location = new ();
        public PointF Location
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

        private List<Flight> _schedule = new();
        public List<Flight> Schedule
        {
            get
            {
                return _schedule;
            }
            init
            {
                _schedule = value;
            }
        }

        private List<Plane> _landedPlanes = new();
        public List<Plane> LandedPlanes
        {
            get
            {
                return _landedPlanes;
            }
            init
            {
                _landedPlanes = value;
            }
        }
        [JsonConstructor]
        public Airport() { }
        public Airport(Rectangle bounds)
        {
            _location = new(new Random().Next(bounds.Left, bounds.Right), new Random().Next(bounds.Top, bounds.Bottom));
        }
        public Airport(PointF point)
        {
            _location = point;
        }
        public void AddPlane(Plane plane)
        {
            plane.SetLocation(_location);
            _landedPlanes.Add(plane);
        }
        public void RemovePlane()
        {
            if (_landedPlanes.Count == 0) return;
            else _landedPlanes.RemoveAt(0);
        }
        public void RemovePlane(Plane plane)
        {
            _landedPlanes.Remove(plane);
        }

        public void ScheduleFlight(Flight flight)
        {
            _schedule.Add(flight);
        }
        public void RemoveFlight(Flight flight)
        {
            _schedule.Remove(flight);
        }

        public void AssignAllPossibleFlights()
        {
            var notAssignedFlights = _schedule.Where(flight => flight.HasAssignedPlane == false);
            for (var i = 0; i < notAssignedFlights.Count(); i++)
            {
                try
                {
                    Flight next = notAssignedFlights.Aggregate((f1, f2) => f1.CompareTo(f2) == 1 ? f1 : f2);
                    AssignFlight(next);
                }
                catch
                {
                    break;
                }
            }
        }
        public void AssignFlight(Flight flight)
        {
            if (_landedPlanes.Count == 0)
            {
                throw new Exception("No free planes");
            }
            Plane assignee = _landedPlanes.First(plane => plane.Flight == null);
            flight.HasAssignedPlane = true;

            assignee.Flight = new Flight(flight);
            flight.Next();
        }
        public void AcceptPlane(Plane plane)
        {
            plane.Flight = null;
            _landedPlanes.Add(plane);
        }
        public void Update()
        {
            LandedPlanes.RemoveAll(plane => plane.Location != _location);
            AssignAllPossibleFlights();
        }
    }
}
