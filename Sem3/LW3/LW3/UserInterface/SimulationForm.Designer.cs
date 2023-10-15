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
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadSimulationButton = new System.Windows.Forms.Button();
            this.saveSimulationButton = new System.Windows.Forms.Button();
            this.graphicsUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.loadSimulationDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSimulationDialog = new System.Windows.Forms.SaveFileDialog();
            this.resetSimulationButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.resetSimulationButton);
            this.panel1.Controls.Add(this.loadSimulationButton);
            this.panel1.Controls.Add(this.saveSimulationButton);
            this.panel1.Location = new System.Drawing.Point(0, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1266, 683);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // loadSimulationButton
            // 
            this.loadSimulationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadSimulationButton.Location = new System.Drawing.Point(168, 630);
            this.loadSimulationButton.Name = "loadSimulationButton";
            this.loadSimulationButton.Size = new System.Drawing.Size(150, 34);
            this.loadSimulationButton.TabIndex = 1;
            this.loadSimulationButton.Text = "Load simulation";
            this.loadSimulationButton.UseVisualStyleBackColor = true;
            this.loadSimulationButton.Click += new System.EventHandler(this.loadSimulationButton_Click);
            // 
            // saveSimulationButton
            // 
            this.saveSimulationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveSimulationButton.Location = new System.Drawing.Point(12, 630);
            this.saveSimulationButton.Name = "saveSimulationButton";
            this.saveSimulationButton.Size = new System.Drawing.Size(150, 34);
            this.saveSimulationButton.TabIndex = 0;
            this.saveSimulationButton.Text = "Save simulation";
            this.saveSimulationButton.UseVisualStyleBackColor = true;
            this.saveSimulationButton.Click += new System.EventHandler(this.saveSimulationButton_Click);
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
            // resetSimulationButton
            // 
            this.resetSimulationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetSimulationButton.Location = new System.Drawing.Point(1100, 630);
            this.resetSimulationButton.Name = "resetSimulationButton";
            this.resetSimulationButton.Size = new System.Drawing.Size(150, 34);
            this.resetSimulationButton.TabIndex = 2;
            this.resetSimulationButton.Text = "Reset simulation";
            this.resetSimulationButton.UseVisualStyleBackColor = true;
            this.resetSimulationButton.Click += new System.EventHandler(this.resetSimulationButton_Click);
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SimulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Airports Simulation";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel1;
        private System.Windows.Forms.Timer graphicsUpdateTimer;
        private Button loadSimulationButton;
        private Button saveSimulationButton;
        private OpenFileDialog loadSimulationDialog;
        private SaveFileDialog saveSimulationDialog;
        private Button resetSimulationButton;
    }
}