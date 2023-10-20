using LW3.Logic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace LW3_UnitTest
{
    [TestClass]
    public class LW3_TestClass
    {
        [TestMethod]
        public void Update_ChangesLocationBydS_WhenRemainingDistanceIsGreaterThandSLength()
        {
            var plane = new LW3.Logic.PassengerPlane();
            var currentTime = DateTime.Now;
            var dT = TimeSpan.FromMilliseconds(100);
            var destination = new Airport()
            {
                Location = new PointF(100, 100)
            };
            plane.Flight = new Flight()
            {
                Destination = destination,
                DepartureTime = currentTime
            };
            plane.SetLocation(new PointF(0, 0));

            plane.Update(currentTime, dT);

            Assert.AreEqual(new PointF(14.142136f, 14.142136f), plane.Location);
        }

        [TestMethod]
        public void Update_DoesNotChangeLocation_WhenIdling()
        {
            var plane = new LW3.Logic.PassengerPlane();
            var currentTime = DateTime.Now;
            var dT = TimeSpan.FromSeconds(1);
            var initialLocation = plane.Location;

            plane.Update(currentTime, dT);

            Assert.AreEqual(initialLocation, plane.Location);
        }

        [TestMethod]
        public void Update_FinishesMoving_WhenDistanceToDestinationLessThanDs()
        {
            var plane = new LW3.Logic.PassengerPlane();
            var currentTime = DateTime.Now;
            var dT = TimeSpan.FromSeconds(1);
            var destination = new Airport()
            {
                Location = new PointF(plane.Location.X + 2, plane.Location.Y)
            };
            plane.Flight = new Flight()
            {
                Destination = destination,
                DepartureTime = currentTime
            };

            plane.Update(currentTime, dT);

            Assert.IsTrue(destination.LandedPlanes.Contains(plane));
        }

        [TestMethod]
        public void FlightDirection_ReturnsZeroVector_WhenFlightAndDestinationAreNull()
        {
            var plane = new LW3.Logic.PassengerPlane();

            var result = plane.FlightDirection();

            Assert.AreEqual(Vector2.Zero, result);
        }

        [TestMethod]
        public void SaveToFile_DoesntThrowException()
        {
            var simulation = new LW3.Logic.Simulation();
            simulation.InitializeExample();

            try
            {
                FileSystem.SaveToFile(simulation);
            }
            catch
            {
                Assert.Fail();
            }
            finally
            {
                File.Delete(FileSystem.FilePath);
            }
        }

        [TestMethod]
        public void ReadFromFile_DoesntThrowException()
        {
            Simulation exSimulation = new(), simulation;

            exSimulation.InitializeExample();

            try
            {
                FileSystem.SaveToFile(exSimulation);

                try
                {
                    simulation = FileSystem.ReadFromFile();
                }
                catch
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail("SaveToFile failed");
            }
            finally
            {
                File.Delete(FileSystem.FilePath);
            }
        }

        [TestMethod]
        public void ReadFromFile_ThrowsException_WhenFileNotFound()
        {
            FileSystem.FilePath = "unitTestTempFile.json";
            try
            {
                File.Delete(FileSystem.FilePath);
            }
            finally
            {
                Assert.ThrowsException<FileNotFoundException>(() =>
                {
                    Simulation simulation = FileSystem.ReadFromFile();
                });
            }
        }

        [TestMethod]
        public void Update_Works()
        {
            try
            {
            Simulation simulation = new(DateTime.Now + TimeSpan.FromHours(2));
            simulation.InitializeExample();

            simulation.Update();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Flight_CopyConstructorWorks()
        {
            var now = DateTime.Now;
            Flight fl1 = new (new(), now);
            Flight fl2 = new (new(), now + TimeSpan.FromHours(1));

            fl2 = new Flight(fl1);

            Assert.AreEqual(fl2.DepartureTime, now);
        }

        [TestMethod]
        public void Airport_ConstructsFromPoint()
        {
            PointF pointF = new(1.5f, 2f);

            Airport test = new (pointF);

            Assert.AreEqual(test.Location, pointF);
        }

        [TestMethod]
        public void Airport_CancelsFlight()
        {
            Simulation test = new();
            test.InitializeExample();

            var flightToCancel = test.Airports[0].Schedule[0];
            test.Airports[0].CancelFlight(flightToCancel);

            Assert.IsFalse(test.Airports[0].Schedule.Contains(flightToCancel));
        }

        [TestMethod]
        public void Airport_AcceptsFlight()
        {
            Airport testAirport = new();
            LW3.Logic.PassengerPlane testPlane = new();
            Passenger testPassenger = new();
            testPlane.Passengers.Add(testPassenger);

            testAirport.AcceptPlane(testPlane);

            Assert.IsFalse(testPlane.Passengers.Contains(testPassenger));
        }

        [TestMethod]
        public void Drawing()
        {
            
        }
    }
}