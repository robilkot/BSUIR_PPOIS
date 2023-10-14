using LW3.UserInterface;

namespace LW3
{
    public partial class SimulationForm : Form
    {
        public SimulationForm()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawFlights(e);
            DrawAirports(e);
            DrawPlanes(e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.simulation.Update();

            Refresh();
        }
    }
}