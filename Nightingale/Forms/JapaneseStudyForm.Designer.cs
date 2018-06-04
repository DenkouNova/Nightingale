namespace Nightingale.Forms
{
    partial class JapaneseStudyForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbTotalDisplayedWords = new System.Windows.Forms.Label();
            this.btnPaint = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbTotalWords = new System.Windows.Forms.Label();
            this.lbNumberOfMastered = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveConfigs = new System.Windows.Forms.Button();
            this.tbBaseNumberOfWords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbSmallBrush = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(200, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(759, 584);
            this.panel3.TabIndex = 1;
            // 
            // lbTotalDisplayedWords
            // 
            this.lbTotalDisplayedWords.AutoSize = true;
            this.lbTotalDisplayedWords.Enabled = false;
            this.lbTotalDisplayedWords.Location = new System.Drawing.Point(15, 43);
            this.lbTotalDisplayedWords.Margin = new System.Windows.Forms.Padding(15, 15, 3, 0);
            this.lbTotalDisplayedWords.Name = "lbTotalDisplayedWords";
            this.lbTotalDisplayedWords.Size = new System.Drawing.Size(164, 13);
            this.lbTotalDisplayedWords.TabIndex = 10;
            this.lbTotalDisplayedWords.Text = "{TOTAL_DISPLAYED_WORDS}";
            // 
            // btnPaint
            // 
            this.btnPaint.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaint.Location = new System.Drawing.Point(40, 429);
            this.btnPaint.Margin = new System.Windows.Forms.Padding(40, 20, 3, 3);
            this.btnPaint.Name = "btnPaint";
            this.btnPaint.Size = new System.Drawing.Size(112, 72);
            this.btnPaint.TabIndex = 18;
            this.btnPaint.Text = "Paint";
            this.btnPaint.UseVisualStyleBackColor = true;
            this.btnPaint.Click += new System.EventHandler(this.btnPaint_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.lbTotalWords);
            this.flowLayoutPanel1.Controls.Add(this.lbTotalDisplayedWords);
            this.flowLayoutPanel1.Controls.Add(this.lbNumberOfMastered);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.btnPaint);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 578);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lbTotalWords
            // 
            this.lbTotalWords.AutoSize = true;
            this.lbTotalWords.Enabled = false;
            this.lbTotalWords.Location = new System.Drawing.Point(15, 15);
            this.lbTotalWords.Margin = new System.Windows.Forms.Padding(15, 15, 3, 0);
            this.lbTotalWords.Name = "lbTotalWords";
            this.lbTotalWords.Size = new System.Drawing.Size(98, 13);
            this.lbTotalWords.TabIndex = 22;
            this.lbTotalWords.Text = "{TOTAL_WORDS}";
            // 
            // lbNumberOfMastered
            // 
            this.lbNumberOfMastered.AutoSize = true;
            this.lbNumberOfMastered.Enabled = false;
            this.lbNumberOfMastered.Location = new System.Drawing.Point(15, 71);
            this.lbNumberOfMastered.Margin = new System.Windows.Forms.Padding(15, 15, 3, 0);
            this.lbNumberOfMastered.Name = "lbNumberOfMastered";
            this.lbNumberOfMastered.Size = new System.Drawing.Size(146, 13);
            this.lbNumberOfMastered.TabIndex = 24;
            this.lbNumberOfMastered.Text = "(NUMBER_OF_MASTERED)";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSaveConfigs);
            this.panel1.Controls.Add(this.tbBaseNumberOfWords);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(3, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 319);
            this.panel1.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(67, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Configs";
            // 
            // btnSaveConfigs
            // 
            this.btnSaveConfigs.Location = new System.Drawing.Point(56, 51);
            this.btnSaveConfigs.Name = "btnSaveConfigs";
            this.btnSaveConfigs.Size = new System.Drawing.Size(80, 35);
            this.btnSaveConfigs.TabIndex = 4;
            this.btnSaveConfigs.Text = "button1";
            this.btnSaveConfigs.UseVisualStyleBackColor = true;
            // 
            // tbBaseNumberOfWords
            // 
            this.tbBaseNumberOfWords.Location = new System.Drawing.Point(116, 24);
            this.tbBaseNumberOfWords.Name = "tbBaseNumberOfWords";
            this.tbBaseNumberOfWords.Size = new System.Drawing.Size(58, 20);
            this.tbBaseNumberOfWords.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Base num. of words";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbSmallBrush);
            this.panel2.Location = new System.Drawing.Point(3, 507);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 66);
            this.panel2.TabIndex = 25;
            // 
            // cbSmallBrush
            // 
            this.cbSmallBrush.AutoSize = true;
            this.cbSmallBrush.Enabled = false;
            this.cbSmallBrush.Location = new System.Drawing.Point(53, 13);
            this.cbSmallBrush.Name = "cbSmallBrush";
            this.cbSmallBrush.Size = new System.Drawing.Size(80, 17);
            this.cbSmallBrush.TabIndex = 25;
            this.cbSmallBrush.Text = "Small brush";
            this.cbSmallBrush.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(959, 584);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // JapaneseStudyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 584);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "JapaneseStudyForm";
            this.Text = "JapaneseStudyForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JapaneseStudyForm_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbTotalDisplayedWords;
        private System.Windows.Forms.Button btnPaint;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbTotalWords;
        private System.Windows.Forms.Label lbNumberOfMastered;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveConfigs;
        private System.Windows.Forms.TextBox tbBaseNumberOfWords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbSmallBrush;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}