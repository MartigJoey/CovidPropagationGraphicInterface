
namespace CovidPropagationGraphicInterface
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblClock = new System.Windows.Forms.Label();
            this.btnLegend = new System.Windows.Forms.Button();
            this.crtChart = new LiveCharts.WinForms.CartesianChart();
            this.pieChart = new LiveCharts.WinForms.PieChart();
            this.graphicInterface = new CovidPropagationGraphicInterface.GraphicInterface();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Démarrer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.Start_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(93, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Pause";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClock.Location = new System.Drawing.Point(323, 3);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(73, 22);
            this.lblClock.TabIndex = 3;
            this.lblClock.Text = "Horloge";
            // 
            // btnLegend
            // 
            this.btnLegend.Location = new System.Drawing.Point(174, 3);
            this.btnLegend.Name = "btnLegend";
            this.btnLegend.Size = new System.Drawing.Size(143, 23);
            this.btnLegend.TabIndex = 4;
            this.btnLegend.Text = "Légende";
            this.btnLegend.UseVisualStyleBackColor = true;
            this.btnLegend.Click += new System.EventHandler(this.btnLegend_Click);
            // 
            // crtChart
            // 
            this.crtChart.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.crtChart.Location = new System.Drawing.Point(549, 32);
            this.crtChart.Name = "crtChart";
            this.crtChart.Size = new System.Drawing.Size(440, 394);
            this.crtChart.TabIndex = 5;
            this.crtChart.Text = "cartesianChart1";
            // 
            // pieChart
            // 
            this.pieChart.Location = new System.Drawing.Point(995, 23);
            this.pieChart.Name = "pieChart";
            this.pieChart.Size = new System.Drawing.Size(425, 403);
            this.pieChart.TabIndex = 6;
            this.pieChart.Text = "pieChart1";
            // 
            // graphicInterface
            // 
            this.graphicInterface.Location = new System.Drawing.Point(12, 32);
            this.graphicInterface.Name = "graphicInterface";
            this.graphicInterface.Size = new System.Drawing.Size(531, 394);
            this.graphicInterface.TabIndex = 0;
            this.graphicInterface.Text = "graphicInterface";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 438);
            this.Controls.Add(this.pieChart);
            this.Controls.Add(this.crtChart);
            this.Controls.Add(this.btnLegend);
            this.Controls.Add(this.lblClock);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.graphicInterface);
            this.Name = "FrmMain";
            this.Text = "Interface Graphique - Covid Propagation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphicInterface graphicInterface;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Button btnLegend;
        private LiveCharts.WinForms.CartesianChart crtChart;
        private LiveCharts.WinForms.PieChart pieChart;
    }
}

