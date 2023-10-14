using LW3.Logic;
using LW3.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    public partial class SimulationForm : Form
    {
        private static readonly Font s_airportNameFont = new("Segoe UI", 12);
        private static readonly Font s_airportDetailsFont = new("Segoe UI", 9);
        private static readonly Font s_flightNameFont = new("Segoe UI", 9);
        private static readonly Font s_planeModelFont = new("Segoe UI", 9);

        private static readonly Pen s_solidPen = new(SystemColors.ControlText, 1);
        private static readonly Pen s_dashPen = new(SystemColors.ControlDark, 1) { DashPattern = new float[]{10,50} };
        private static readonly int s_graphRadius = 10;
        private void DrawAirports(PaintEventArgs e)
        {
            foreach (var airport in Program.simulation.Airports)
            {
                Rectangle rect = new((int)(airport.Location.X - s_graphRadius), (int)(airport.Location.Y - s_graphRadius), 2 * s_graphRadius, 2 * s_graphRadius);

                e.Graphics.DrawRectangle(s_solidPen, rect);

                var textLocation = new Point((int)Math.Round(airport.Location.X - s_graphRadius), (int)Math.Round(airport.Location.Y + s_graphRadius));
                TextRenderer.DrawText(e.Graphics, airport.Name, s_airportNameFont, textLocation, SystemColors.ControlText);
                //e.Graphics.DrawString(airport.Name, s_airportNameFont, s_fontBrush, textLocation);

                float shift = s_graphRadius + s_airportNameFont.Height;
                foreach (var plane in airport.LandedPlanes)
                {
                    var detailsTextLocation = new Point((int)Math.Round(airport.Location.X - s_graphRadius), (int)Math.Round(airport.Location.Y + shift));
                    TextRenderer.DrawText(e.Graphics, plane.Model, s_airportDetailsFont, detailsTextLocation, SystemColors.ControlText);
                    shift += s_airportDetailsFont.Height;
                }
            }
        }
        private void DrawFlights(PaintEventArgs e)
        {
            foreach (var airport in Program.simulation.Airports)
            {

                foreach(var flight in airport.Schedule)
                {
                    var firstPoint = new Point((int)Math.Round(airport.Location.X), (int)Math.Round(airport.Location.Y));
                    var secondPoint = new Point((int)Math.Round(flight.Destination.Location.X), (int)Math.Round(flight.Destination.Location.Y));

                    e.Graphics.DrawLine(s_dashPen, firstPoint, secondPoint);

                    var textLocation = new Point(firstPoint.X + (secondPoint.X - firstPoint.X) * 2 / 3, firstPoint.Y + (secondPoint.Y - firstPoint.Y) * 2 / 3);
                    TextRenderer.DrawText(e.Graphics, $"-> {flight.Destination.Name}", s_flightNameFont, textLocation, SystemColors.ControlDark);
                }

            }
        }
        private void DrawPlanes(PaintEventArgs e)
        {
            var planesToDraw = Program.simulation.Planes.Where(plane => plane.IsIdling() != true);
            foreach (var plane in planesToDraw)
            {
                Rectangle rect = new((int)(plane.Location.X - s_graphRadius / 2), (int)(plane.Location.Y - s_graphRadius / 2), s_graphRadius, s_graphRadius);
                //Rectangle rect = new(300, 300, 20, 20);
                e.Graphics.DrawEllipse(s_solidPen, rect);

                var textLocation = new Point((int)Math.Round(plane.Location.X - s_graphRadius / 2), (int)Math.Round(plane.Location.Y + s_graphRadius / 2));
                TextRenderer.DrawText(e.Graphics, plane.Model, s_planeModelFont, textLocation, SystemColors.ControlText);
            }
        }
    }
}
