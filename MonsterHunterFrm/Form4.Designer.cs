namespace MonsterHunterFrm
{
    partial class Form4
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
            textBox1 = new TextBox();
            Leaderboard = new Label();
            PlayerName = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 56);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(203, 146);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // Leaderboard
            // 
            Leaderboard.AutoSize = true;
            Leaderboard.Location = new Point(12, 11);
            Leaderboard.Name = "Leaderboard";
            Leaderboard.Size = new Size(73, 15);
            Leaderboard.TabIndex = 1;
            Leaderboard.Text = "Leaderboard";
            Leaderboard.Click += label1_Click;
            // 
            // PlayerName
            // 
            PlayerName.AutoSize = true;
            PlayerName.Location = new Point(12, 38);
            PlayerName.Name = "PlayerName";
            PlayerName.Size = new Size(71, 15);
            PlayerName.TabIndex = 2;
            PlayerName.Text = "PlayerName";
            PlayerName.Click += PlayerName_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(137, 38);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 3;
            label1.Text = "Score";
            label1.Click += label1_Click_1;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(229, 211);
            Controls.Add(label1);
            Controls.Add(PlayerName);
            Controls.Add(Leaderboard);
            Controls.Add(textBox1);
            Name = "Form4";
            Text = "Form4";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label Leaderboard;
        private Label PlayerName;
        private Label label1;
    }
}