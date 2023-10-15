using LW3.Logic;

namespace LW3
{
    internal static class Program
    {

        public static Simulation simulation = new();

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var form = new UserInterface.SimulationForm();

            simulation.UpdateInterval = new TimeSpan(0, 0, 0, 0, 15);

            Application.Run(form);
        }
    }
}