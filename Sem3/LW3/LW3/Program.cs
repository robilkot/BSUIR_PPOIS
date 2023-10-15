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

            Simulation.Bounds = form.Bounds;

            simulation.InitializeExample();
            simulation.UpdateInterval = new TimeSpan(0, 0, 0, 0, 15);

            Application.Run(form);
        }
    }
}