namespace Lego_Death_Race
{
    partial class Main
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
            this.btnStartRace = new System.Windows.Forms.Button();
            this.btnQuitRace = new System.Windows.Forms.Button();
            this.pnlSeperator = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnStartRace
            // 
            this.btnStartRace.Location = new System.Drawing.Point(12, 12);
            this.btnStartRace.Name = "btnStartRace";
            this.btnStartRace.Size = new System.Drawing.Size(140, 35);
            this.btnStartRace.TabIndex = 0;
            this.btnStartRace.Text = "Start Race";
            this.btnStartRace.UseVisualStyleBackColor = true;
            // 
            // btnQuitRace
            // 
            this.btnQuitRace.Location = new System.Drawing.Point(158, 12);
            this.btnQuitRace.Name = "btnQuitRace";
            this.btnQuitRace.Size = new System.Drawing.Size(140, 35);
            this.btnQuitRace.TabIndex = 1;
            this.btnQuitRace.Text = "Quit Race";
            this.btnQuitRace.UseVisualStyleBackColor = true;
            // 
            // pnlSeperator
            // 
            this.pnlSeperator.BackColor = System.Drawing.Color.Black;
            this.pnlSeperator.Location = new System.Drawing.Point(0, 53);
            this.pnlSeperator.Name = "pnlSeperator";
            this.pnlSeperator.Size = new System.Drawing.Size(200, 2);
            this.pnlSeperator.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(702, 331);
            this.Controls.Add(this.pnlSeperator);
            this.Controls.Add(this.btnQuitRace);
            this.Controls.Add(this.btnStartRace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Lego Death Race";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartRace;
        private System.Windows.Forms.Button btnQuitRace;
        private System.Windows.Forms.Panel pnlSeperator;
    }
}