using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class Plane : IUpdateable
    {
        public string Model { get; init; } = string.Empty;
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
        public uint Velocity { get; init; } = 100;
        public Flight? Flight = new();

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

        public Vector2 FlightDirection()
        {
            if (Flight != null)
            {
                return Vector2.Normalize(Flight.Destination.Location - _location);
            }
            else
            {
                return Vector2.Zero;
            }
        }
        public void Update()
        {
            if (Flight == null || DateTime.Now < Flight.DepartureTime)
            {
                return;
            }
            var dT = DateTime.Now - _updated;

            Vector2 dS = Vector2.Multiply(FlightDirection(), (int)dT.TotalSeconds * Velocity);

            if (Vector2.Distance(Flight.Destination.Location, _location) < dS.Length())
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