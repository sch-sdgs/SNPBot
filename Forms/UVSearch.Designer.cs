namespace SNPBot
{
    partial class UVSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UVSearch));
            this.buttonSearch = new System.Windows.Forms.Button();
            this.webBrowserMain = new System.Windows.Forms.WebBrowser();
            this.textBoxFasta = new System.Windows.Forms.TextBox();
            this.getFastaButton = new System.Windows.Forms.Button();
            this.refSeqIDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.variantTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.dataGridViewTools = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControlTools = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.tabPagePMut = new System.Windows.Forms.TabPage();
            this.webBrowserPMut = new System.Windows.Forms.WebBrowser();
            this.tabPageSandGo = new System.Windows.Forms.TabPage();
            this.webBrowserSandGo = new System.Windows.Forms.WebBrowser();
            this.tabPageHansa = new System.Windows.Forms.TabPage();
            this.webBrowserHansa = new System.Windows.Forms.WebBrowser();
            this.tabPagePP2 = new System.Windows.Forms.TabPage();
            this.webBrowserPP2 = new System.Windows.Forms.WebBrowser();
            this.label5 = new System.Windows.Forms.Label();
            this.geneBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.refSeqProteinBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.summariseBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTools)).BeginInit();
            this.tabControlTools.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPagePMut.SuspendLayout();
            this.tabPageSandGo.SuspendLayout();
            this.tabPageHansa.SuspendLayout();
            this.tabPagePP2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(17, 243);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(120, 38);
            this.buttonSearch.TabIndex = 0;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // webBrowserMain
            // 
            this.webBrowserMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserMain.Location = new System.Drawing.Point(3, 3);
            this.webBrowserMain.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.webBrowserMain.MinimumSize = new System.Drawing.Size(34, 34);
            this.webBrowserMain.Name = "webBrowserMain";
            this.webBrowserMain.ScriptErrorsSuppressed = true;
            this.webBrowserMain.Size = new System.Drawing.Size(426, 230);
            this.webBrowserMain.TabIndex = 1;
            this.webBrowserMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserMain_DocumentCompleted);
            // 
            // textBoxFasta
            // 
            this.textBoxFasta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFasta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxFasta.Location = new System.Drawing.Point(17, 292);
            this.textBoxFasta.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxFasta.Multiline = true;
            this.textBoxFasta.Name = "textBoxFasta";
            this.textBoxFasta.ReadOnly = true;
            this.textBoxFasta.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxFasta.Size = new System.Drawing.Size(431, 217);
            this.textBoxFasta.TabIndex = 2;
            // 
            // getFastaButton
            // 
            this.getFastaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getFastaButton.Location = new System.Drawing.Point(126, 76);
            this.getFastaButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.getFastaButton.Name = "getFastaButton";
            this.getFastaButton.Size = new System.Drawing.Size(108, 29);
            this.getFastaButton.TabIndex = 3;
            this.getFastaButton.Text = "Get FASTA";
            this.getFastaButton.UseVisualStyleBackColor = true;
            this.getFastaButton.Click += new System.EventHandler(this.getFastaButton_Click);
            // 
            // refSeqIDTextBox
            // 
            this.refSeqIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refSeqIDTextBox.Location = new System.Drawing.Point(9, 42);
            this.refSeqIDTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.refSeqIDTextBox.Name = "refSeqIDTextBox";
            this.refSeqIDTextBox.Size = new System.Drawing.Size(108, 24);
            this.refSeqIDTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "RefSeq mRNA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(348, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Variant";
            // 
            // variantTextBox
            // 
            this.variantTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.variantTextBox.Location = new System.Drawing.Point(349, 42);
            this.variantTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.variantTextBox.Name = "variantTextBox";
            this.variantTextBox.Size = new System.Drawing.Size(88, 24);
            this.variantTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 172);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Email address";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailTextBox.Location = new System.Drawing.Point(12, 194);
            this.emailTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(428, 24);
            this.emailTextBox.TabIndex = 8;
            // 
            // dataGridViewTools
            // 
            this.dataGridViewTools.AllowUserToAddRows = false;
            this.dataGridViewTools.AllowUserToDeleteRows = false;
            this.dataGridViewTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTools.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTools.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridViewTools.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTools.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridViewTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTools.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTools.Location = new System.Drawing.Point(462, 12);
            this.dataGridViewTools.MultiSelect = false;
            this.dataGridViewTools.Name = "dataGridViewTools";
            this.dataGridViewTools.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewTools.RowHeadersVisible = false;
            this.dataGridViewTools.RowTemplate.Height = 24;
            this.dataGridViewTools.ShowCellToolTips = false;
            this.dataGridViewTools.ShowEditingIcon = false;
            this.dataGridViewTools.Size = new System.Drawing.Size(440, 217);
            this.dataGridViewTools.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "Path to results folder";
            // 
            // pathBox
            // 
            this.pathBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathBox.Location = new System.Drawing.Point(9, 139);
            this.pathBox.Margin = new System.Windows.Forms.Padding(4);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(428, 24);
            this.pathBox.TabIndex = 11;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(361, 106);
            this.browseBtn.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(79, 29);
            this.browseBtn.TabIndex = 16;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // tabControlTools
            // 
            this.tabControlTools.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTools.Controls.Add(this.tabPageMain);
            this.tabControlTools.Controls.Add(this.tabPagePMut);
            this.tabControlTools.Controls.Add(this.tabPageSandGo);
            this.tabControlTools.Controls.Add(this.tabPageHansa);
            this.tabControlTools.Controls.Add(this.tabPagePP2);
            this.tabControlTools.Location = new System.Drawing.Point(462, 243);
            this.tabControlTools.Name = "tabControlTools";
            this.tabControlTools.SelectedIndex = 0;
            this.tabControlTools.Size = new System.Drawing.Size(440, 266);
            this.tabControlTools.TabIndex = 17;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.webBrowserMain);
            this.tabPageMain.Location = new System.Drawing.Point(4, 26);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(432, 236);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Main tab";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // tabPagePMut
            // 
            this.tabPagePMut.Controls.Add(this.webBrowserPMut);
            this.tabPagePMut.Location = new System.Drawing.Point(4, 26);
            this.tabPagePMut.Name = "tabPagePMut";
            this.tabPagePMut.Size = new System.Drawing.Size(432, 236);
            this.tabPagePMut.TabIndex = 5;
            this.tabPagePMut.Text = "PMut";
            this.tabPagePMut.UseVisualStyleBackColor = true;
            // 
            // webBrowserPMut
            // 
            this.webBrowserPMut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserPMut.Location = new System.Drawing.Point(0, 0);
            this.webBrowserPMut.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPMut.Name = "webBrowserPMut";
            this.webBrowserPMut.Size = new System.Drawing.Size(432, 236);
            this.webBrowserPMut.TabIndex = 0;
            this.webBrowserPMut.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPMut_DocumentCompleted);
            // 
            // tabPageSandGo
            // 
            this.tabPageSandGo.Controls.Add(this.webBrowserSandGo);
            this.tabPageSandGo.Location = new System.Drawing.Point(4, 26);
            this.tabPageSandGo.Name = "tabPageSandGo";
            this.tabPageSandGo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSandGo.Size = new System.Drawing.Size(432, 236);
            this.tabPageSandGo.TabIndex = 4;
            this.tabPageSandGo.Text = "SNPsandGO";
            this.tabPageSandGo.UseVisualStyleBackColor = true;
            // 
            // webBrowserSandGo
            // 
            this.webBrowserSandGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserSandGo.Location = new System.Drawing.Point(3, 3);
            this.webBrowserSandGo.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserSandGo.Name = "webBrowserSandGo";
            this.webBrowserSandGo.Size = new System.Drawing.Size(426, 230);
            this.webBrowserSandGo.TabIndex = 0;
            this.webBrowserSandGo.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserSandGo_DocumentCompleted);
            // 
            // tabPageHansa
            // 
            this.tabPageHansa.Controls.Add(this.webBrowserHansa);
            this.tabPageHansa.Location = new System.Drawing.Point(4, 26);
            this.tabPageHansa.Name = "tabPageHansa";
            this.tabPageHansa.Size = new System.Drawing.Size(432, 236);
            this.tabPageHansa.TabIndex = 2;
            this.tabPageHansa.Text = "HANSA";
            this.tabPageHansa.UseVisualStyleBackColor = true;
            // 
            // webBrowserHansa
            // 
            this.webBrowserHansa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserHansa.Location = new System.Drawing.Point(0, 0);
            this.webBrowserHansa.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHansa.Name = "webBrowserHansa";
            this.webBrowserHansa.Size = new System.Drawing.Size(432, 236);
            this.webBrowserHansa.TabIndex = 1;
            this.webBrowserHansa.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserHansa_DocumentCompleted);
            this.webBrowserHansa.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser2_NewWindow);
            // 
            // tabPagePP2
            // 
            this.tabPagePP2.Controls.Add(this.webBrowserPP2);
            this.tabPagePP2.Location = new System.Drawing.Point(4, 26);
            this.tabPagePP2.Name = "tabPagePP2";
            this.tabPagePP2.Size = new System.Drawing.Size(432, 236);
            this.tabPagePP2.TabIndex = 3;
            this.tabPagePP2.Text = "PolyPhen2";
            this.tabPagePP2.UseVisualStyleBackColor = true;
            // 
            // webBrowserPP2
            // 
            this.webBrowserPP2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserPP2.Location = new System.Drawing.Point(0, 0);
            this.webBrowserPP2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPP2.Name = "webBrowserPP2";
            this.webBrowserPP2.Size = new System.Drawing.Size(432, 236);
            this.webBrowserPP2.TabIndex = 2;
            this.webBrowserPP2.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPP2_DocumentCompleted);
            this.webBrowserPP2.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser3_NewWindow);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(241, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "Gene";
            // 
            // geneBox
            // 
            this.geneBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geneBox.Location = new System.Drawing.Point(242, 42);
            this.geneBox.Margin = new System.Windows.Forms.Padding(4);
            this.geneBox.Name = "geneBox";
            this.geneBox.Size = new System.Drawing.Size(75, 24);
            this.geneBox.TabIndex = 19;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // refSeqProteinBox
            // 
            this.refSeqProteinBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refSeqProteinBox.Location = new System.Drawing.Point(126, 42);
            this.refSeqProteinBox.Margin = new System.Windows.Forms.Padding(4);
            this.refSeqProteinBox.Name = "refSeqProteinBox";
            this.refSeqProteinBox.Size = new System.Drawing.Size(108, 24);
            this.refSeqProteinBox.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(125, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "RefSeq protein";
            // 
            // summariseBtn
            // 
            this.summariseBtn.Enabled = false;
            this.summariseBtn.Location = new System.Drawing.Point(147, 242);
            this.summariseBtn.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.summariseBtn.Name = "summariseBtn";
            this.summariseBtn.Size = new System.Drawing.Size(120, 38);
            this.summariseBtn.TabIndex = 23;
            this.summariseBtn.Text = "Summarise";
            this.summariseBtn.UseVisualStyleBackColor = true;
            this.summariseBtn.Click += new System.EventHandler(this.summariseBtn_Click);
            // 
            // UVSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(914, 530);
            this.Controls.Add(this.summariseBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxFasta);
            this.Controls.Add(this.refSeqProteinBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.geneBox);
            this.Controls.Add(this.tabControlTools);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pathBox);
            this.Controls.Add(this.dataGridViewTools);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.variantTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.refSeqIDTextBox);
            this.Controls.Add(this.getFastaButton);
            this.Controls.Add(this.buttonSearch);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "UVSearch";
            this.Text = "SNPBot - SNP Pathogenicity Search Tool         ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UVSearch_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTools)).EndInit();
            this.tabControlTools.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPagePMut.ResumeLayout(false);
            this.tabPageSandGo.ResumeLayout(false);
            this.tabPageHansa.ResumeLayout(false);
            this.tabPagePP2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.WebBrowser webBrowserMain;
        private System.Windows.Forms.TextBox textBoxFasta;
        private System.Windows.Forms.Button getFastaButton;
        private System.Windows.Forms.TextBox refSeqIDTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox variantTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.DataGridView dataGridViewTools;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl tabControlTools;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox geneBox;
        private System.Windows.Forms.TabPage tabPageHansa;
        private System.Windows.Forms.WebBrowser webBrowserHansa;
        private System.Windows.Forms.TabPage tabPagePP2;
        private System.Windows.Forms.WebBrowser webBrowserPP2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox refSeqProteinBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button summariseBtn;
        private System.Windows.Forms.TabPage tabPageSandGo;
        private System.Windows.Forms.WebBrowser webBrowserSandGo;
        private System.Windows.Forms.TabPage tabPagePMut;
        private System.Windows.Forms.WebBrowser webBrowserPMut;
    }
}

