﻿using System.ComponentModel;
using System.Reflection;
using LW3.Logic;

namespace LW3.UserInterface
{
    public partial class SimulationForm : Form
    {
        private static readonly string s_saveSimulationButtonText = "Сохранить";
        public SimulationForm()
        {
            InitializeComponent();
        }
        private void simulationSpread_Paint(object sender, PaintEventArgs e)
        {
            SimulationFormDrawing.DrawFlights(e);
            SimulationFormDrawing.DrawAirports(e);
            SimulationFormDrawing.DrawPlanes(e);
        }

        private void graphicsUpdateTimer_Tick(object sender, EventArgs e)
        {
            Program.simulation.Update();
            simulationSpread.Refresh();
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            graphicsUpdateTimer.Interval = (int)Program.simulation.UpdateInterval.TotalMilliseconds;
            graphicsUpdateTimer.Start();
        }

        private void saveSimulationButton_Click(object sender, EventArgs e)
        {
            if (saveFileWorker.IsBusy == false && saveSimulationDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileWorker.RunWorkerAsync();
            }
        }

        private void loadSimulationButton_Click(object sender, EventArgs e)
        {
            if (loadSimulationDialog.ShowDialog() == DialogResult.OK)
            {
                instructionsLabel.Visible = false;
                LW3.Logic.FileSystem.SetFilePath(loadSimulationDialog.FileName);
                Program.simulation = FileSystem.ReadFromFile();
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            saveSimulationButton.Text = "Сохранение";
            loadSimulationButton.Enabled = false;
            LW3.Logic.FileSystem.SetFilePath(saveSimulationDialog.FileName);
            FileSystem.SaveToFile(Program.simulation);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            saveSimulationButton.Text = s_saveSimulationButtonText;
            loadSimulationButton.Enabled = true;
        }

        private void createSimulationButton_Click(object sender, EventArgs e)
        {
            instructionsLabel.Visible = false;
            Program.simulation.Bounds = Bounds;
            Program.simulation.InitializeExample();
        }
    }
}