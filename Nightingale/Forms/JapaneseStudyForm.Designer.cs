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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbSource = new System.Windows.Forms.Label();
            this.lbWord = new System.Windows.Forms.Label();
            this.lbQuote = new System.Windows.Forms.Label();
            this.lbDefinitionOrTranslation = new System.Windows.Forms.Label();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnMasteryDown = new System.Windows.Forms.Button();
            this.btnMasteryUp = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblIdMastery = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel2);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(759, 584);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lbSource, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lbWord, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.lbQuote, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.lbDefinitionOrTranslation, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.tlpButtons, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 9;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(759, 584);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lbSource
            // 
            this.lbSource.AutoSize = true;
            this.lbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSource.Font = new System.Drawing.Font("MS Mincho", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbSource.Location = new System.Drawing.Point(3, 69);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(753, 30);
            this.lbSource.TabIndex = 0;
            this.lbSource.Text = "「セーラームーン第０８話」より";
            this.lbSource.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWord
            // 
            this.lbWord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbWord.AutoSize = true;
            this.lbWord.Font = new System.Drawing.Font("MS Mincho", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbWord.Location = new System.Drawing.Point(320, 99);
            this.lbWord.Name = "lbWord";
            this.lbWord.Size = new System.Drawing.Size(118, 48);
            this.lbWord.TabIndex = 1;
            this.lbWord.Text = "落第";
            this.lbWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbQuote
            // 
            this.lbQuote.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbQuote.AutoSize = true;
            this.lbQuote.Font = new System.Drawing.Font("MS Mincho", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbQuote.Location = new System.Drawing.Point(8, 204);
            this.lbQuote.Name = "lbQuote";
            this.lbQuote.Size = new System.Drawing.Size(742, 48);
            this.lbQuote.TabIndex = 2;
            this.lbQuote.Text = "55 うさぎママ「うさぎ、ママね、贅沢は言わないわ。落第だけはしないでちょうだい。」";
            this.lbQuote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDefinitionOrTranslation
            // 
            this.lbDefinitionOrTranslation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbDefinitionOrTranslation.AutoSize = true;
            this.lbDefinitionOrTranslation.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDefinitionOrTranslation.Location = new System.Drawing.Point(103, 304);
            this.lbDefinitionOrTranslation.Name = "lbDefinitionOrTranslation";
            this.lbDefinitionOrTranslation.Size = new System.Drawing.Size(552, 52);
            this.lbDefinitionOrTranslation.TabIndex = 3;
            this.lbDefinitionOrTranslation.Text = "(n,vs) failure, dropping out of a class, (Pgfadgfadgadgadgadgadgfadgagfadgfadgadg" +
    "adgfadggaddgfad)";
            this.lbDefinitionOrTranslation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 5;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.Controls.Add(this.btnMasteryDown, 3, 0);
            this.tlpButtons.Controls.Add(this.btnMasteryUp, 1, 0);
            this.tlpButtons.Controls.Add(this.btnNext, 0, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(3, 362);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(753, 194);
            this.tlpButtons.TabIndex = 5;
            // 
            // btnMasteryDown
            // 
            this.btnMasteryDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasteryDown.Font = new System.Drawing.Font("MS Mincho", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMasteryDown.Location = new System.Drawing.Point(389, 3);
            this.btnMasteryDown.Name = "btnMasteryDown";
            this.btnMasteryDown.Size = new System.Drawing.Size(194, 188);
            this.btnMasteryDown.TabIndex = 3;
            this.btnMasteryDown.Text = "↓";
            this.btnMasteryDown.UseVisualStyleBackColor = true;
            this.btnMasteryDown.Click += new System.EventHandler(this.btnMasteryDown_Click);
            // 
            // btnMasteryUp
            // 
            this.btnMasteryUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasteryUp.Font = new System.Drawing.Font("MS Mincho", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasteryUp.Location = new System.Drawing.Point(169, 3);
            this.btnMasteryUp.Name = "btnMasteryUp";
            this.btnMasteryUp.Size = new System.Drawing.Size(194, 188);
            this.btnMasteryUp.TabIndex = 0;
            this.btnMasteryUp.Text = "↑";
            this.btnMasteryUp.UseVisualStyleBackColor = true;
            this.btnMasteryUp.Click += new System.EventHandler(this.btnMasteryUp_Click);
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNext.Font = new System.Drawing.Font("MS Mincho", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(3, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(160, 188);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "→";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.lblIdMastery, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 28);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(753, 28);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // lblIdMastery
            // 
            this.lblIdMastery.AutoSize = true;
            this.lblIdMastery.BackColor = System.Drawing.Color.LightGreen;
            this.lblIdMastery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdMastery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIdMastery.Font = new System.Drawing.Font("MS Mincho", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIdMastery.Location = new System.Drawing.Point(229, 0);
            this.lblIdMastery.Name = "lblIdMastery";
            this.lblIdMastery.Size = new System.Drawing.Size(294, 28);
            this.lblIdMastery.TabIndex = 7;
            this.lblIdMastery.Text = "ID:380 Mastery:34";
            this.lblIdMastery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public System.Windows.Forms.Label lbSource;
        public System.Windows.Forms.Label lbWord;
        public System.Windows.Forms.Label lbQuote;
        public System.Windows.Forms.Label lbDefinitionOrTranslation;
        public System.Windows.Forms.TableLayoutPanel tlpButtons;
        public System.Windows.Forms.Button btnMasteryDown;
        public System.Windows.Forms.Button btnMasteryUp;
        public System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        public System.Windows.Forms.Label lblIdMastery;
    }
}