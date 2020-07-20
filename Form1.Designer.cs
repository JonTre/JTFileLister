namespace JTFileLister
{
    partial class Form1
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
            this.goButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dirCountLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fileCountLbl = new System.Windows.Forms.Label();
            this.currFileLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.currDirLbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(12, 130);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 0;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Location = new System.Drawing.Point(188, 129);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Directory Count:";
            // 
            // dirCountLbl
            // 
            this.dirCountLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dirCountLbl.AutoEllipsis = true;
            this.dirCountLbl.Location = new System.Drawing.Point(126, 16);
            this.dirCountLbl.Name = "dirCountLbl";
            this.dirCountLbl.Size = new System.Drawing.Size(136, 15);
            this.dirCountLbl.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "File Count:";
            // 
            // fileCountLbl
            // 
            this.fileCountLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileCountLbl.AutoEllipsis = true;
            this.fileCountLbl.Location = new System.Drawing.Point(126, 43);
            this.fileCountLbl.Name = "fileCountLbl";
            this.fileCountLbl.Size = new System.Drawing.Size(136, 15);
            this.fileCountLbl.TabIndex = 3;
            // 
            // currFileLbl
            // 
            this.currFileLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currFileLbl.AutoEllipsis = true;
            this.currFileLbl.Location = new System.Drawing.Point(126, 97);
            this.currFileLbl.Name = "currFileLbl";
            this.currFileLbl.Size = new System.Drawing.Size(136, 15);
            this.currFileLbl.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "Current File:";
            // 
            // currDirLbl
            // 
            this.currDirLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currDirLbl.AutoEllipsis = true;
            this.currDirLbl.Location = new System.Drawing.Point(126, 70);
            this.currDirLbl.Name = "currDirLbl";
            this.currDirLbl.Size = new System.Drawing.Size(136, 15);
            this.currDirLbl.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "Current Directory:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 165);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(256, 64);
            this.textBox1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 241);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.currDirLbl);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.currFileLbl);
            this.Controls.Add(this.fileCountLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dirCountLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.goButton);
            this.MinimumSize = new System.Drawing.Size(290, 280);
            this.Name = "Form1";
            this.Text = "Interview Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dirCountLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label fileCountLbl;
        private System.Windows.Forms.Label currFileLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label currDirLbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
    }
}

