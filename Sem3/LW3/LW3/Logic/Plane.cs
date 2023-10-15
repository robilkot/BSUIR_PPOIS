using System.Numerics;

namespace LW3.Logic
{
    [Serializable]
    class Plane : IUpdateable
    {
        private const string s_defaultModel = "Boeing 777";
        private const uint s_defaultVelocity = 200;
        public uint Velocity { get; init; } = s_defaultVelocity;
        public Flight? Flight { get; set; }
        public string Model { get; init; } = s_defaultModel;
        private PointF _location = new();
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
        public bool Idling => Flight == null || Flight.Destination == null || DateTime.Now < Flight.DepartureTime;
        public Plane() { }
        public Plane(string model, uint velocity)
        {
            Model = model;
            Velocity = velocity;
        }

        public Vector2 FlightDirection()
        {
            if (Flight != null && Flight.Destination != null)
            {
                Vector2 difference = new(Flight.Destination.Location.X - _location.X, Flight.Destination.Location.Y - _location.Y);
                return Vector2.Normalize(difference);
            }
            
            return Vector2.Zero;
        }
        public void SetLocation(PointF location)
        {
            _location = location;
        }
        public void Update()
        {
            var dT = DateTime.Now - _updated;
            _updated = DateTime.Now;
            
            if (Idling)
            {
                return;
            }

            var dS = Vector2.Multiply(FlightDirection(), (int)dT.TotalMilliseconds * Velocity / 1000);

            var remainingDistance = Math.Sqrt(Math.Pow(Flight.Destination.Location.X - _location.X, 2) + Math.Pow(Flight.Destination.Location.Y - _location.Y, 2));
            if (remainingDistance <= dS.Length())
            {
                _location = Flight.Destination.Location;
                Flight.Destination.AcceptPlane(this);
            }
            else
            {
                _location.X += dS.X;
                _location.Y += dS.Y;
            }
        }
    }
}