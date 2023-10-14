using LW3.Logic;
using System.Drawing;

namespace LW3.UserInterface
{
    internal static class Program
    {

        public static Simulation simulation = new();

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var form = new SimulationForm();

            Simulation.Bounds = form.Bounds;

            simulation.InitializeExample();

            Application.Run(form);
        }
    }
}