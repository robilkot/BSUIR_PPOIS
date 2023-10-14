using System.Numerics;

namespace LW3.Logic
{
    [Serializable]
    class Plane : IUpdateable
    {
        private const string s_defaultModel = "Boeing 777";
        public string Model { get; init; } = s_defaultModel;
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
        public uint Velocity { get; init; } = 10;
        public Flight? Flight;

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

        public Plane(string model = s_defaultModel)
        {
            Model = model;
        }

        public bool IsIdling()
        {
            return Flight == null || DateTime.Now < Flight.DepartureTime;
        }
        public Vector2 FlightDirection()
        {
            if (Flight != null)
            {
                var difference = Flight.Destination.Location - _location;
                if(difference != Vector2.Zero)
                {
                    return Vector2.Normalize(difference);
                }
            }
            
            return Vector2.Zero;
        }
        public void SetLocation(Vector2 location)
        {
            _location = location;
        }
        public void Update()
        {
            if (IsIdling())
            {
                return;
            }
            var dT = DateTime.Now - _updated;

            Vector2 dS = Vector2.Multiply(FlightDirection(), (int)dT.TotalSeconds * Velocity);

            if (Vector2.Distance(Flight.Destination.Location, _location) <= dS.Length())
            {
                _location = Flight.Destination.Location;
                Flight.Destination.AcceptPlane(this);
            }
            else
            {
                _location += dS;
            }
        }
    }
}