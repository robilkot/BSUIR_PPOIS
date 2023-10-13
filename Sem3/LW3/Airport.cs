using System.Numerics;

namespace LW3
{
    internal class Airport : IUpdateable
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
        public string Name { get; private set; } = "Unnamed Airport";
        public Queue<Flight> Schedule { get; private set; } = new();
        public Queue<Plane> FreePlanes { get; private set; } = new();

        public void AddPlane(Plane plane)
        {
            FreePlanes.Enqueue(plane);
        }
        public void RemovePlane()
        {
            FreePlanes.Dequeue();
        }

        private DateTime _updated = DateTime.Now;
        public DateTime Updated
        {
            get
            {
                return _updated;
            }
            init
            {
                _updated = value;
            }
        }
        public void AssignAllPossibleFlights()
        {
            for (var i = 0; i < Schedule.Count; i++)
            {
                var flight = Schedule.Peek();

                try
                {
                    AssignFlight(flight);
                    _ = Schedule.Dequeue();
                    Schedule.Enqueue(flight);
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
            if (FreePlanes.Count == 0)
            {
                throw new Exception("No free planes");
            }
            Plane assignee = FreePlanes.Dequeue();
            assignee.Flight = flight;
        }
        public void AcceptPlane(Plane plane)
        {
            plane.Flight = null;
            FreePlanes.Enqueue(plane);
        }
        public void Update()
        {
            //var dT = DateTime.Now - _updated;

            AssignAllPossibleFlights();

            _updated = DateTime.Now;
        }
    }
}
