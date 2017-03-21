namespace Lego_Death_Race
{
    partial class StartGameSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudPlayerCount = new System.Windows.Forms.NumericUpDown();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.tboxPlayerName0 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tboxPlayerName1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tboxPlayerName2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tboxPlayerName3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayerCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Players:";
            // 
            // nudPlayerCount
            // 
            this.nudPlayerCount.Location = new System.Drawing.Point(11, 24);
            this.nudPlayerCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudPlayerCount.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudPlayerCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPlayerCount.Name = "nudPlayerCount";
            this.nudPlayerCount.Size = new System.Drawing.Size(90, 20);
            this.nudPlayerCount.TabIndex = 1;
            this.nudPlayerCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPlayerCount.ValueChanged += new System.EventHandler(this.nudPlayerAmount_ValueChanged);
            // 
            // btnStartGame
            // 
            this.btnStartGame.Location = new System.Drawing.Point(47, 157);
            this.btnStartGame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(92, 29);
            this.btnStartGame.TabIndex = 2;
            this.btnStartGame.Text = "Start Game ";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // tboxPlayerName0
            // 
            this.tboxPlayerName0.Location = new System.Drawing.Point(62, 59);
            this.tboxPlayerName0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tboxPlayerName0.MaxLength = 12;
            this.tboxPlayerName0.Name = "tboxPlayerName0";
            this.tboxPlayerName0.Size = new System.Drawing.Size(114, 20);
            this.tboxPlayerName0.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player 1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Player 2:";
            // 
            // tboxPlayerName1
            // 
            this.tboxPlayerName1.Location = new System.Drawing.Point(62, 82);
            this.tboxPlayerName1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tboxPlayerName1.MaxLength = 12;
            this.tboxPlayerName1.Name = "tboxPlayerName1";
            this.tboxPlayerName1.Size = new System.Drawing.Size(114, 20);
            this.tboxPlayerName1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Player 3:";
            // 
            // tboxPlayerName2
            // 
            this.tboxPlayerName2.Location = new System.Drawing.Point(62, 105);
            this.tboxPlayerName2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tboxPlayerName2.MaxLength = 12;
            this.tboxPlayerName2.Name = "tboxPlayerName2";
            this.tboxPlayerName2.Size = new System.Drawing.Size(114, 20);
            this.tboxPlayerName2.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 130);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Player 4:";
            // 
            // tboxPlayerName3
            // 
            this.tboxPlayerName3.Location = new System.Drawing.Point(62, 128);
            this.tboxPlayerName3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tboxPlayerName3.MaxLength = 12;
            this.tboxPlayerName3.Name = "tboxPlayerName3";
            this.tboxPlayerName3.Size = new System.Drawing.Size(114, 20);
            this.tboxPlayerName3.TabIndex = 9;
            // 
            // StartGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 196);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tboxPlayerName3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tboxPlayerName2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tboxPlayerName1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tboxPlayerName0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudPlayerCount);
            this.Controls.Add(this.btnStartGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "StartGameSettings";
            this.Text = "Start New Game";
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayerCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPlayerCount;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.TextBox tboxPlayerName0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tboxPlayerName1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tboxPlayerName2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tboxPlayerName3;
    }
}