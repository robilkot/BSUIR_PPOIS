using LW3.Logic;
using System.Drawing;
using System.Numerics;

namespace TestProject1
{
    [TestClass]
    public class PlaneTestClass
    {
        [TestMethod]
        public void Update_ChangesLocationBydS_WhenRemainingDistanceIsGreaterThandSLength()
        {
            var plane = new LW3.Logic.Plane();
            var currentTime = DateTime.Now;
            var dT = TimeSpan.FromMilliseconds(100);
            var destination = new Airport()
            {
                Location = new PointF(100, 100)
            };
            plane.Flight = new Flight()
            {
                Destination = destination, DepartureTime = currentTime
            };
            plane.SetLocation(new PointF(0, 0));

            plane.Update(currentTime, dT);

            Assert.AreEqual(new PointF(14.142136f, 14.142136f), plane.Location);
        }

        [TestMethod]
        public void Update_DoesNotChangeLocation_WhenIdling()
        {
            var plane = new LW3.Logic.Plane();
            var currentTime = DateTime.Now;
            var dT = TimeSpan.FromSeconds(1);
            var initialLocation = plane.Location;

            plane.Update(currentTime, dT);

            Assert.AreEqual(initialLocation, plane.Location);
        }

        [TestMethod]
        public void FlightDirection_ReturnsZeroVector_WhenFlightAndDestinationAreNull()
        {
            var plane = new LW3.Logic.Plane();

            var result = plane.FlightDirection();

            Assert.AreEqual(Vector2.Zero, result);
        }
    }
}