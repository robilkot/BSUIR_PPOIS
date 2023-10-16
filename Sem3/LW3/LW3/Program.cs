using LW3.Logic;

namespace LW3
{
    internal static class Program
    {

        public static Simulation? simulation;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var form = new UserInterface.SimulationForm();

            Application.Run(form);
        }
    }
}