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
            this.lblTimeElapsed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartRace
            // 
            this.btnStartRace.Location = new System.Drawing.Point(9, 10);
            this.btnStartRace.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartRace.Name = "btnStartRace";
            this.btnStartRace.Size = new System.Drawing.Size(105, 28);
            this.btnStartRace.TabIndex = 0;
            this.btnStartRace.Text = "Start Race";
            this.btnStartRace.UseVisualStyleBackColor = true;
            this.btnStartRace.Click += new System.EventHandler(this.btnStartRace_Click);
            // 
            // btnQuitRace
            // 
            this.btnQuitRace.Enabled = false;
            this.btnQuitRace.Location = new System.Drawing.Point(118, 10);
            this.btnQuitRace.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuitRace.Name = "btnQuitRace";
            this.btnQuitRace.Size = new System.Drawing.Size(105, 28);
            this.btnQuitRace.TabIndex = 1;
            this.btnQuitRace.Text = "Quit Race";
            this.btnQuitRace.UseVisualStyleBackColor = true;
            this.btnQuitRace.Click += new System.EventHandler(this.btnQuitRace_Click);
            // 
            // pnlSeperator
            // 
            this.pnlSeperator.BackColor = System.Drawing.Color.Black;
            this.pnlSeperator.Location = new System.Drawing.Point(0, 43);
            this.pnlSeperator.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSeperator.Name = "pnlSeperator";
            this.pnlSeperator.Size = new System.Drawing.Size(150, 2);
            this.pnlSeperator.TabIndex = 2;
            // 
            // lblTimeElapsed
            // 
            this.lblTimeElapsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeElapsed.Font = new System.Drawing.Font("Courier New", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeElapsed.Location = new System.Drawing.Point(367, 7);
            this.lblTimeElapsed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTimeElapsed.Name = "lblTimeElapsed";
            this.lblTimeElapsed.Size = new System.Drawing.Size(151, 28);
            this.lblTimeElapsed.TabIndex = 3;
            this.lblTimeElapsed.Text = "00:00:00";
            this.lblTimeElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(526, 269);
            this.Controls.Add(this.lblTimeElapsed);
            this.Controls.Add(this.pnlSeperator);
            this.Controls.Add(this.btnQuitRace);
            this.Controls.Add(this.btnStartRace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Ultimate Death Race";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartRace;
        private System.Windows.Forms.Button btnQuitRace;
        private System.Windows.Forms.Panel pnlSeperator;
        private System.Windows.Forms.Label lblTimeElapsed;
    }
}