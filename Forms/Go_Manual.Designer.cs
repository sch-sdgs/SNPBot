namespace SNPBot
{
    partial class Go_Manual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Go_Manual));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGOs = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "The GO terms for this RefSeq accession could not be \r\nretrieved. Please enter the" +
    "m manually into the box \r\nbelow and click done. ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(453, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "N.B. Please do not paste the terms on seperate lines. They need to be single spac" +
    "e separated.";
            // 
            // textBoxGOs
            // 
            this.textBoxGOs.Location = new System.Drawing.Point(16, 119);
            this.textBoxGOs.Multiline = true;
            this.textBoxGOs.Name = "textBoxGOs";
            this.textBoxGOs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxGOs.Size = new System.Drawing.Size(439, 243);
            this.textBoxGOs.TabIndex = 2;
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(380, 368);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 3;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // Go_Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 401);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxGOs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Go_Manual";
            this.Text = "Go_Manual";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGOs;
        private System.Windows.Forms.Button buttonDone;
    }
}