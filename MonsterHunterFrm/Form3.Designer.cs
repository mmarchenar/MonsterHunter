namespace MonsterHunterFrm
{
    partial class Form3
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
            progressBar1 = new ProgressBar();
            Hp = new Label();
            leve = new Label();
            actuallevel = new Label();
            currenthp = new Label();
            playername = new Label();
            infos = new Label();
            tb_infos = new TextBox();
            actualname = new Label();
            score = new Label();
            actualscore = new Label();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(82, 31);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(237, 15);
            progressBar1.TabIndex = 0;
            progressBar1.Click += progressBar1_Click;
            // 
            // Hp
            // 
            Hp.AutoSize = true;
            Hp.Location = new Point(32, 31);
            Hp.Name = "Hp";
            Hp.Size = new Size(26, 15);
            Hp.TabIndex = 1;
            Hp.Text = "HP:";
            Hp.Click += label1_Click;
            // 
            // leve
            // 
            leve.AutoSize = true;
            leve.Location = new Point(32, 81);
            leve.Name = "leve";
            leve.Size = new Size(37, 15);
            leve.TabIndex = 2;
            leve.Text = "Level:";
            leve.Click += label2_Click;
            // 
            // actuallevel
            // 
            actuallevel.AutoSize = true;
            actuallevel.Location = new Point(67, 81);
            actuallevel.Name = "actuallevel";
            actuallevel.Size = new Size(38, 15);
            actuallevel.TabIndex = 3;
            actuallevel.Text = "label3";
            actuallevel.Click += label3_Click;
            // 
            // currenthp
            // 
            currenthp.AutoSize = true;
            currenthp.Location = new Point(53, 31);
            currenthp.Name = "currenthp";
            currenthp.Size = new Size(23, 15);
            currenthp.TabIndex = 4;
            currenthp.Text = "HP";
            currenthp.Click += label4_Click;
            // 
            // playername
            // 
            playername.AutoSize = true;
            playername.Location = new Point(148, 81);
            playername.Name = "playername";
            playername.Size = new Size(74, 15);
            playername.TabIndex = 5;
            playername.Text = "PlayerName:";
            playername.Click += label5_Click;
            // 
            // infos
            // 
            infos.AutoSize = true;
            infos.Location = new Point(325, 30);
            infos.Name = "infos";
            infos.Size = new Size(43, 15);
            infos.TabIndex = 6;
            infos.Text = "INFOS:";
            infos.Click += label6_Click;
            // 
            // tb_infos
            // 
            tb_infos.Location = new Point(369, 27);
            tb_infos.Multiline = true;
            tb_infos.Name = "tb_infos";
            tb_infos.ReadOnly = true;
            tb_infos.Size = new Size(285, 95);
            tb_infos.TabIndex = 7;
            tb_infos.TextChanged += textBox1_TextChanged;
            // 
            // actualname
            // 
            actualname.AutoSize = true;
            actualname.Location = new Point(221, 81);
            actualname.Name = "actualname";
            actualname.Size = new Size(39, 15);
            actualname.TabIndex = 8;
            actualname.Text = "sasass";
            // 
            // score
            // 
            score.AutoSize = true;
            score.Location = new Point(285, 81);
            score.Name = "score";
            score.Size = new Size(39, 15);
            score.TabIndex = 9;
            score.Text = "Score:";
            // 
            // actualscore
            // 
            actualscore.AutoSize = true;
            actualscore.Location = new Point(320, 81);
            actualscore.Name = "actualscore";
            actualscore.Size = new Size(43, 15);
            actualscore.TabIndex = 10;
            actualscore.Text = "asdsad";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(666, 134);
            Controls.Add(actualscore);
            Controls.Add(score);
            Controls.Add(actualname);
            Controls.Add(tb_infos);
            Controls.Add(infos);
            Controls.Add(playername);
            Controls.Add(currenthp);
            Controls.Add(actuallevel);
            Controls.Add(leve);
            Controls.Add(Hp);
            Controls.Add(progressBar1);
            Name = "Form3";
            Text = "Form3";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar1;
        private Label Hp;
        private Label leve;
        private Label actuallevel;
        private Label currenthp;
        private Label playername;
        private Label infos;
        private TextBox tb_infos;
        private Label actualname;
        private Label score;
        private Label actualscore;
    }
}