
namespace CovidPropagationGraphicInterface
{
    partial class FrmLegend
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.legend = new CovidPropagationGraphicInterface.Classes.Legend();
            this.SuspendLayout();
            // 
            // legend
            // 
            this.legend.Location = new System.Drawing.Point(-2, 1);
            this.legend.Name = "legend";
            this.legend.Size = new System.Drawing.Size(298, 537);
            this.legend.TabIndex = 0;
            this.legend.Text = "legend";
            // 
            // FrmLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 531);
            this.Controls.Add(this.legend);
            this.MaximumSize = new System.Drawing.Size(302, 570);
            this.MinimumSize = new System.Drawing.Size(302, 570);
            this.Name = "FrmLegend";
            this.Text = "Légende";
            this.ResumeLayout(false);

        }

        #endregion

        private Classes.Legend legend;
    }
}