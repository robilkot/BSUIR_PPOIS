using LW3.Logic;
using System.Drawing.Drawing2D;
using System.Text;

namespace LW3.UserInterface
{
    public class SimulationFormDrawing
    {
        private static readonly Font s_airportNameFont = new("Segoe UI", 12);
        private static readonly Font s_airportDetailsFont = new("Segoe UI", 9);
        private static readonly Font s_flightNameFont = new("Segoe UI", 9);
        private static readonly Font s_planeModelFont = new("Segoe UI", 9);
        private static readonly Font s_passengerNameFont = new("Segoe UI", 9);
        private static readonly Font s_simulationDetailsFont = new("Segoe UI", 9);

        private static readonly Pen s_dashPen = new(SystemColors.ControlDark, 1) { DashPattern = new float[] { 10, 50 } };

        private static readonly SolidBrush s_fillBrush = new(SystemColors.ControlDark);

        private static readonly int s_graphRadius = 7;
        public static void DrawAirports(PaintEventArgs e)
        {
            foreach (var airport in Program.simulation.Airports)
            {
                Rectangle rect = new((int)Math.Round(airport.Location.X - s_graphRadius), (int)Math.Round(airport.Location.Y - s_graphRadius), 2 * s_graphRadius, 2 * s_graphRadius);

                e.Graphics.DrawRectangle(Pens.Black, rect);
                e.Graphics.FillRectangle(s_fillBrush, rect);

                var textLocation = new Point((int)Math.Round(airport.Location.X - s_graphRadius), (int)Math.Round(airport.Location.Y + s_graphRadius));
                
                var passengerTextLocation = textLocation;
                passengerTextLocation.Y -= s_airportNameFont.Height + s_graphRadius * 2 ;
                DrawPassengers(e, airport.Passengers, passengerTextLocation);

                TextRenderer.DrawText(e.Graphics, airport.Name, s_airportNameFont, textLocation, SystemColors.ControlText);
                textLocation.Y += s_airportNameFont.Height;

                //foreach (var flight in airport.Schedule)
                //{
                //    TextRenderer.DrawText(e.Graphics, $"-> {flight.Destination?.Name}, {flight.DepartureTime:HH:mm:ss}", s_flightNameFont, textLocation, SystemColors.ControlText);
                //    textLocation.Y += s_airportDetailsFont.Height;
                //}
                //textLocation.Y += s_flightNameFont.Height;

                foreach (var plane in airport.LandedPlanes)
                {
                    TextRenderer.DrawText(e.Graphics, $"{plane.Flight?.DepartureTime.ToString("HH:mm:ss")} {plane.Flight?.Destination?.Name} ({plane.Model})", s_airportDetailsFont, textLocation, SystemColors.ControlText);
                    textLocation.Y += s_airportDetailsFont.Height;
                }
            }
        }
        private static void DrawPassengers(PaintEventArgs e, List<Passenger> passengers, Point textLocation)
        {
            foreach (var passenger in passengers)
            {
                TextRenderer.DrawText(e.Graphics, $"{passenger.Name}", s_passengerNameFont, textLocation, SystemColors.ControlText);
                textLocation.Y -= s_passengerNameFont.Height;
            }
        }
        public static void DrawFlights(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var airport in Program.simulation.Airports)
            {

                foreach (var flight in airport.Schedule)
                {
                    if(flight.Destination == null)
                    {
                        continue;
                    }
                    
                    var firstPoint = new Point((int)airport.Location.X, (int)airport.Location.Y);
                    var secondPoint = new Point((int)flight.Destination.Location.X, (int)flight.Destination.Location.Y);

                    e.Graphics.DrawLine(s_dashPen, firstPoint, secondPoint);

                    var textLocation = new Point(firstPoint.X + (secondPoint.X - firstPoint.X) * 2 / 5, firstPoint.Y + (secondPoint.Y - firstPoint.Y) * 2 / 5);
                    TextRenderer.DrawText(e.Graphics, $"{airport.Name} -> {flight.Destination.Name}", s_flightNameFont, textLocation, SystemColors.ControlDark);
                }

            }
        }
        public static void DrawPlanes(PaintEventArgs e)
        {
            var planesToDraw = Program.simulation.Planes.Where(plane => plane.Idling(Program.simulation.VirtualCurrentTime) == false);
            foreach (var plane in planesToDraw)
            {
                Rectangle rect = new((int)Math.Round(plane.Location.X - s_graphRadius / 2), (int)Math.Round(plane.Location.Y - s_graphRadius / 2), s_graphRadius, s_graphRadius);
                e.Graphics.FillEllipse(s_fillBrush, rect);

                var textLocation = new Point((int)Math.Round(plane.Location.X - s_graphRadius / 2), (int)Math.Round(plane.Location.Y + s_graphRadius / 2));
                TextRenderer.DrawText(e.Graphics, $"{plane.Model} ({ plane.Flight?.Destination?.Name})", s_planeModelFont, textLocation, SystemColors.ControlText);

                var passengerTextLocation = textLocation;
                passengerTextLocation.Y -= 4 * s_graphRadius;
                DrawPassengers(e, plane.Passengers, passengerTextLocation);
            }
        }
        public static void DrawDetails(PaintEventArgs e)
        {
            var textLocation = new Point(25, 25);

            StringBuilder detailsText= new(128);
            detailsText.Append("Время в симуляции: ");
            detailsText.Append(Program.simulation.VirtualCurrentTime);
            detailsText.Append("\nКоэфициент ускорения: ");
            detailsText.Append(Program.simulation.TimeScale);
            
            TextRenderer.DrawText(e.Graphics, detailsText.ToString(), s_simulationDetailsFont, textLocation, SystemColors.ControlText);
        }
    }
}
