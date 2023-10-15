using System.Reflection;

namespace LW3.UserInterface
{
    public partial class SimulationForm : Form
    {
        public SimulationForm()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            panel1, new object[] { true });
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SimulationFormDrawing.DrawFlights(e);
            SimulationFormDrawing.DrawAirports(e);
            SimulationFormDrawing.DrawPlanes(e);
        }

        private void graphicsUpdateTimer_Tick(object sender, EventArgs e)
        {
            Program.simulation.Update();
            panel1.Refresh();
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            graphicsUpdateTimer.Interval = (int)Program.simulation.UpdateInterval.TotalMilliseconds;
            graphicsUpdateTimer.Start();
        }

        private void saveSimulationButton_Click(object sender, EventArgs e)
        {
            if (saveSimulationDialog.ShowDialog() == DialogResult.OK)
            {
                LW3.Logic.FileSystem.SetFilePath(saveSimulationDialog.FileName);
                LW3.Logic.FileSystem.SaveToFile(Program.simulation);
            }
        }

        private void loadSimulationButton_Click(object sender, EventArgs e)
        {
            if (loadSimulationDialog.ShowDialog() == DialogResult.OK)
            {
                LW3.Logic.FileSystem.SetFilePath(loadSimulationDialog.FileName);
                Program.simulation = LW3.Logic.FileSystem.ReadFromFile();
            }
        }

        private void resetSimulationButton_Click(object sender, EventArgs e)
        {
            Program.simulation.InitializeExample();
        }
    }
}