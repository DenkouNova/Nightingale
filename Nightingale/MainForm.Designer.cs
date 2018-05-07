namespace Nightingale
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreateDictionary = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnStudy = new System.Windows.Forms.Button();
            this.tlpBird = new System.Windows.Forms.TableLayoutPanel();
            this.pbBird = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbUseDatabaseSqlite = new System.Windows.Forms.CheckBox();
            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.tlpBird.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBird)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 1);
            this.tlpMain.Controls.Add(this.tlpBird, 0, 3);
            this.tlpMain.Controls.Add(this.panel1, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 305F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(634, 527);
            this.tlpMain.TabIndex = 2;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 8;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.Controls.Add(this.btnCreateDictionary, 1, 0);
            this.tlpButtons.Controls.Add(this.btnImport, 3, 0);
            this.tlpButtons.Controls.Add(this.btnStudy, 5, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(3, 19);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(628, 154);
            this.tlpButtons.TabIndex = 0;
            // 
            // btnCreateDictionary
            // 
            this.btnCreateDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreateDictionary.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateDictionary.Image")));
            this.btnCreateDictionary.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCreateDictionary.Location = new System.Drawing.Point(47, 3);
            this.btnCreateDictionary.Name = "btnCreateDictionary";
            this.btnCreateDictionary.Size = new System.Drawing.Size(144, 148);
            this.btnCreateDictionary.TabIndex = 0;
            this.btnCreateDictionary.Text = "Create study dictionary";
            this.btnCreateDictionary.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCreateDictionary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateDictionary.UseVisualStyleBackColor = true;
            this.btnCreateDictionary.Click += new System.EventHandler(this.btnCreateDictionary_Click);
            // 
            // btnImport
            // 
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnImport.Location = new System.Drawing.Point(227, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(144, 148);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import file to dictionary";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnStudy
            // 
            this.btnStudy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStudy.Image = ((System.Drawing.Image)(resources.GetObject("btnStudy.Image")));
            this.btnStudy.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStudy.Location = new System.Drawing.Point(407, 3);
            this.btnStudy.Name = "btnStudy";
            this.btnStudy.Size = new System.Drawing.Size(144, 148);
            this.btnStudy.TabIndex = 2;
            this.btnStudy.Text = "Start studying";
            this.btnStudy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStudy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStudy.UseVisualStyleBackColor = true;
            this.btnStudy.Click += new System.EventHandler(this.btnStudy_Click);
            // 
            // tlpBird
            // 
            this.tlpBird.ColumnCount = 3;
            this.tlpBird.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBird.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 305F));
            this.tlpBird.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBird.Controls.Add(this.pbBird, 1, 0);
            this.tlpBird.Controls.Add(this.label1, 0, 0);
            this.tlpBird.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBird.Location = new System.Drawing.Point(3, 209);
            this.tlpBird.Name = "tlpBird";
            this.tlpBird.RowCount = 1;
            this.tlpBird.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBird.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 299F));
            this.tlpBird.Size = new System.Drawing.Size(628, 299);
            this.tlpBird.TabIndex = 1;
            // 
            // pbBird
            // 
            this.pbBird.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbBird.Image = ((System.Drawing.Image)(resources.GetObject("pbBird.Image")));
            this.pbBird.Location = new System.Drawing.Point(164, 3);
            this.pbBird.Name = "pbBird";
            this.pbBird.Size = new System.Drawing.Size(299, 293);
            this.pbBird.TabIndex = 0;
            this.pbBird.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(25, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 45, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 254);
            this.label1.TabIndex = 1;
            this.label1.Text = "chirp chirp motherfucker! >";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbUseDatabaseSqlite);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 179);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 24);
            this.panel1.TabIndex = 2;
            // 
            // cbUseDatabaseSqlite
            // 
            this.cbUseDatabaseSqlite.AutoSize = true;
            this.cbUseDatabaseSqlite.Location = new System.Drawing.Point(238, 5);
            this.cbUseDatabaseSqlite.Name = "cbUseDatabaseSqlite";
            this.cbUseDatabaseSqlite.Size = new System.Drawing.Size(119, 17);
            this.cbUseDatabaseSqlite.TabIndex = 0;
            this.cbUseDatabaseSqlite.Text = "use Database.sqlite";
            this.cbUseDatabaseSqlite.UseVisualStyleBackColor = true;
            this.cbUseDatabaseSqlite.CheckedChanged += new System.EventHandler(this.cbUseDatabaseSqlite_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 527);
            this.Controls.Add(this.tlpMain);
            this.Name = "MainForm";
            this.Text = "Nightingale";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tlpMain.ResumeLayout(false);
            this.tlpButtons.ResumeLayout(false);
            this.tlpBird.ResumeLayout(false);
            this.tlpBird.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBird)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCreateDictionary;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnStudy;
        private System.Windows.Forms.TableLayoutPanel tlpBird;
        private System.Windows.Forms.PictureBox pbBird;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbUseDatabaseSqlite;

    }
}

