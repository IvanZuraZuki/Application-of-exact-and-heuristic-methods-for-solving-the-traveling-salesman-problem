namespace BruteForce
{
    partial class Form1
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
            this.btnBrute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBrojVrhova = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUnos = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNHH = new System.Windows.Forms.Button();
            this.CWS = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrute
            // 
            this.btnBrute.Location = new System.Drawing.Point(9, 30);
            this.btnBrute.Name = "btnBrute";
            this.btnBrute.Size = new System.Drawing.Size(169, 23);
            this.btnBrute.TabIndex = 0;
            this.btnBrute.Text = "BruteForce - backtrack";
            this.btnBrute.UseVisualStyleBackColor = true;
            this.btnBrute.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Broj vrhova";
            // 
            // txtBrojVrhova
            // 
            this.txtBrojVrhova.Location = new System.Drawing.Point(6, 67);
            this.txtBrojVrhova.Name = "txtBrojVrhova";
            this.txtBrojVrhova.Size = new System.Drawing.Size(100, 22);
            this.txtBrojVrhova.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUnos);
            this.groupBox1.Controls.Add(this.txtBrojVrhova);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 195);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unos vrhova";
            // 
            // btnUnos
            // 
            this.btnUnos.Location = new System.Drawing.Point(12, 105);
            this.btnUnos.Name = "btnUnos";
            this.btnUnos.Size = new System.Drawing.Size(75, 23);
            this.btnUnos.TabIndex = 4;
            this.btnUnos.Text = "unos";
            this.btnUnos.UseVisualStyleBackColor = true;
            this.btnUnos.Click += new System.EventHandler(this.btnUnos_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNHH);
            this.groupBox2.Controls.Add(this.CWS);
            this.groupBox2.Controls.Add(this.btnBrute);
            this.groupBox2.Location = new System.Drawing.Point(12, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 122);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Odabir algoritma";
            // 
            // btnNHH
            // 
            this.btnNHH.Location = new System.Drawing.Point(9, 59);
            this.btnNHH.Name = "btnNHH";
            this.btnNHH.Size = new System.Drawing.Size(169, 23);
            this.btnNHH.TabIndex = 7;
            this.btnNHH.Text = "NNH";
            this.btnNHH.UseVisualStyleBackColor = true;
            this.btnNHH.Click += new System.EventHandler(this.btnNHH_Click);
            // 
            // CWS
            // 
            this.CWS.Location = new System.Drawing.Point(9, 88);
            this.CWS.Name = "CWS";
            this.CWS.Size = new System.Drawing.Size(169, 23);
            this.CWS.TabIndex = 6;
            this.CWS.Text = "CWS";
            this.CWS.UseVisualStyleBackColor = true;
            this.CWS.Click += new System.EventHandler(this.CWS_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(282, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(489, 336);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.button1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Click += new System.EventHandler(this.button1_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBrute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBrojVrhova;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUnos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnNHH;
        private System.Windows.Forms.Button CWS;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

