namespace LW3.UserInterface
{
    partial class SimulationForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.graphicsUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.loadSimulationDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSimulationDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveFileWorker = new System.ComponentModel.BackgroundWorker();
            this.saveSimulationButton = new System.Windows.Forms.Button();
            this.loadSimulationButton = new System.Windows.Forms.Button();
            this.createSimulationButton = new System.Windows.Forms.Button();
            this.simulationSpread = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.instructionsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.simulationSpread)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphicsUpdateTimer
            // 
            this.graphicsUpdateTimer.Interval = 30;
            this.graphicsUpdateTimer.Tick += new System.EventHandler(this.graphicsUpdateTimer_Tick);
            // 
            // loadSimulationDialog
            // 
            this.loadSimulationDialog.FileName = "simulation.json";
            this.loadSimulationDialog.Filter = ".json files|*.json";
            this.loadSimulationDialog.Title = "Load simulation";
            // 
            // saveSimulationDialog
            // 
            this.saveSimulationDialog.FileName = "simulation.json";
            this.saveSimulationDialog.Filter = ".json files|*.json";
            this.saveSimulationDialog.Title = "Save simulation";
            // 
            // saveFileWorker
            // 
            this.saveFileWorker.WorkerReportsProgress = true;
            this.saveFileWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.saveFileWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // saveSimulationButton
            // 
            this.saveSimulationButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.saveSimulationButton.Location = new System.Drawing.Point(14, 8);
            this.saveSimulationButton.Name = "saveSimulationButton";
            this.saveSimulationButton.Size = new System.Drawing.Size(150, 34);
            this.saveSimulationButton.TabIndex = 2;
            this.saveSimulationButton.Text = "Сохранить";
            this.saveSimulationButton.UseVisualStyleBackColor = true;
            this.saveSimulationButton.Click += new System.EventHandler(this.saveSimulationButton_Click);
            // 
            // loadSimulationButton
            // 
            this.loadSimulationButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.loadSimulationButton.Location = new System.Drawing.Point(170, 8);
            this.loadSimulationButton.Name = "loadSimulationButton";
            this.loadSimulationButton.Size = new System.Drawing.Size(150, 34);
            this.loadSimulationButton.TabIndex = 1;
            this.loadSimulationButton.Text = "Открыть";
            this.loadSimulationButton.UseVisualStyleBackColor = true;
            this.loadSimulationButton.Click += new System.EventHandler(this.loadSimulationButton_Click);
            // 
            // createSimulationButton
            // 
            this.createSimulationButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createSimulationButton.Location = new System.Drawing.Point(944, 8);
            this.createSimulationButton.Name = "createSimulationButton";
            this.createSimulationButton.Size = new System.Drawing.Size(306, 34);
            this.createSimulationButton.TabIndex = 0;
            this.createSimulationButton.Text = "Создать симуляцию";
            this.createSimulationButton.UseVisualStyleBackColor = true;
            this.createSimulationButton.Click += new System.EventHandler(this.createSimulationButton_Click);
            // 
            // simulationSpread
            // 
            this.simulationSpread.BackColor = System.Drawing.SystemColors.ControlLight;
            this.simulationSpread.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.simulationSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simulationSpread.Location = new System.Drawing.Point(0, 0);
            this.simulationSpread.Name = "simulationSpread";
            this.simulationSpread.Size = new System.Drawing.Size(1262, 673);
            this.simulationSpread.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.simulationSpread.TabIndex = 3;
            this.simulationSpread.TabStop = false;
            this.simulationSpread.Paint += new System.Windows.Forms.PaintEventHandler(this.simulationSpread_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.copyrightLabel);
            this.panel1.Controls.Add(this.createSimulationButton);
            this.panel1.Controls.Add(this.loadSimulationButton);
            this.panel1.Controls.Add(this.saveSimulationButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 623);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1262, 50);
            this.panel1.TabIndex = 4;
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.copyrightLabel.AutoSize = true;
            this.copyrightLabel.Location = new System.Drawing.Point(519, 15);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(255, 20);
            this.copyrightLabel.TabIndex = 3;
            this.copyrightLabel.Text = "ППОИС ЛР3. With love by @robilkot.";
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.instructionsLabel.AutoSize = true;
            this.instructionsLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.instructionsLabel.Location = new System.Drawing.Point(489, 284);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(303, 20);
            this.instructionsLabel.TabIndex = 4;
            this.instructionsLabel.Text = "Откройте или создайте новую симуляцию";
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.simulationSpread);
            this.MinimumSize = new System.Drawing.Size(900, 450);
            this.Name = "SimulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Airports Simulation";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.simulationSpread)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer graphicsUpdateTimer;
        private OpenFileDialog loadSimulationDialog;
        private SaveFileDialog saveSimulationDialog;
        private System.ComponentModel.BackgroundWorker saveFileWorker;
        private Button saveSimulationButton;
        private Button loadSimulationButton;
        private Button createSimulationButton;
        private PictureBox simulationSpread;
        private Panel panel1;
        private Label copyrightLabel;
        private Label instructionsLabel;
    }
}