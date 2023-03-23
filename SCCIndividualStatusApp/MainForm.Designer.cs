namespace SCCIndividualStatusApp
{
  partial class MainForm
  {
    /// <summary>
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.SpreadsheetUrlLabel = new System.Windows.Forms.Label();
      this.GeneratePdfButton = new System.Windows.Forms.Button();
      this.IncludeTodayCheckBox = new System.Windows.Forms.CheckBox();
      this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
      this.OutputFolderTextbox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.PaperSizeComboBox = new System.Windows.Forms.ComboBox();
      this.BrowseOutputFolderButton = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.OutputFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.LogTextBox = new System.Windows.Forms.RichTextBox();
      this.SpreadsheetUrlRichTextBox = new System.Windows.Forms.RichTextBox();
      this.OptionsGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // SpreadsheetUrlLabel
      // 
      this.SpreadsheetUrlLabel.AutoSize = true;
      this.SpreadsheetUrlLabel.Location = new System.Drawing.Point(25, 27);
      this.SpreadsheetUrlLabel.Margin = new System.Windows.Forms.Padding(5);
      this.SpreadsheetUrlLabel.Name = "SpreadsheetUrlLabel";
      this.SpreadsheetUrlLabel.Size = new System.Drawing.Size(143, 23);
      this.SpreadsheetUrlLabel.TabIndex = 1;
      this.SpreadsheetUrlLabel.Text = "SpreadSheet URL";
      // 
      // GeneratePdfButton
      // 
      this.GeneratePdfButton.AutoSize = true;
      this.GeneratePdfButton.BackColor = System.Drawing.Color.DeepSkyBlue;
      this.GeneratePdfButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
      this.GeneratePdfButton.Location = new System.Drawing.Point(74, 634);
      this.GeneratePdfButton.Margin = new System.Windows.Forms.Padding(60, 11, 10, 11);
      this.GeneratePdfButton.Name = "GeneratePdfButton";
      this.GeneratePdfButton.Size = new System.Drawing.Size(102, 38);
      this.GeneratePdfButton.TabIndex = 2;
      this.GeneratePdfButton.Text = "PDF生成";
      this.GeneratePdfButton.UseVisualStyleBackColor = false;
      this.GeneratePdfButton.Click += new System.EventHandler(this.GeneratePdfButton_Click);
      // 
      // IncludeTodayCheckBox
      // 
      this.IncludeTodayCheckBox.AutoSize = true;
      this.IncludeTodayCheckBox.Checked = true;
      this.IncludeTodayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.IncludeTodayCheckBox.Location = new System.Drawing.Point(15, 239);
      this.IncludeTodayCheckBox.Margin = new System.Windows.Forms.Padding(5, 11, 5, 5);
      this.IncludeTodayCheckBox.Name = "IncludeTodayCheckBox";
      this.IncludeTodayCheckBox.Size = new System.Drawing.Size(173, 27);
      this.IncludeTodayCheckBox.TabIndex = 0;
      this.IncludeTodayCheckBox.Text = "今日を出力に含める";
      this.toolTip1.SetToolTip(this.IncludeTodayCheckBox, "今日のデータを出力内容に含めるかどうかを決めます。");
      this.IncludeTodayCheckBox.UseVisualStyleBackColor = true;
      // 
      // OptionsGroupBox
      // 
      this.OptionsGroupBox.Controls.Add(this.OutputFolderTextbox);
      this.OptionsGroupBox.Controls.Add(this.label2);
      this.OptionsGroupBox.Controls.Add(this.label1);
      this.OptionsGroupBox.Controls.Add(this.PaperSizeComboBox);
      this.OptionsGroupBox.Controls.Add(this.BrowseOutputFolderButton);
      this.OptionsGroupBox.Controls.Add(this.IncludeTodayCheckBox);
      this.OptionsGroupBox.Location = new System.Drawing.Point(30, 260);
      this.OptionsGroupBox.Margin = new System.Windows.Forms.Padding(10, 22, 10, 11);
      this.OptionsGroupBox.Name = "OptionsGroupBox";
      this.OptionsGroupBox.Padding = new System.Windows.Forms.Padding(10, 11, 10, 11);
      this.OptionsGroupBox.Size = new System.Drawing.Size(542, 283);
      this.OptionsGroupBox.TabIndex = 4;
      this.OptionsGroupBox.TabStop = false;
      this.OptionsGroupBox.Text = "Options";
      // 
      // OutputFolderTextbox
      // 
      this.OutputFolderTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.OutputFolderTextbox.Location = new System.Drawing.Point(25, 150);
      this.OutputFolderTextbox.Margin = new System.Windows.Forms.Padding(5);
      this.OutputFolderTextbox.Name = "OutputFolderTextbox";
      this.OutputFolderTextbox.ReadOnly = true;
      this.OutputFolderTextbox.Size = new System.Drawing.Size(373, 31);
      this.OutputFolderTextbox.TabIndex = 8;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 122);
      this.label2.Margin = new System.Windows.Forms.Padding(5);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(106, 23);
      this.label2.TabIndex = 7;
      this.label2.Text = "出力フォルダ";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 45);
      this.label1.Margin = new System.Windows.Forms.Padding(5);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 23);
      this.label1.TabIndex = 7;
      this.label1.Text = "用紙";
      // 
      // PaperSizeComboBox
      // 
      this.PaperSizeComboBox.FormattingEnabled = true;
      this.PaperSizeComboBox.Items.AddRange(new object[] {
            "A4",
            "B5"});
      this.PaperSizeComboBox.Location = new System.Drawing.Point(25, 73);
      this.PaperSizeComboBox.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
      this.PaperSizeComboBox.Name = "PaperSizeComboBox";
      this.PaperSizeComboBox.Size = new System.Drawing.Size(121, 31);
      this.PaperSizeComboBox.TabIndex = 6;
      // 
      // BrowseOutputFolderButton
      // 
      this.BrowseOutputFolderButton.AutoSize = true;
      this.BrowseOutputFolderButton.BackColor = System.Drawing.Color.White;
      this.BrowseOutputFolderButton.ForeColor = System.Drawing.Color.Black;
      this.BrowseOutputFolderButton.Location = new System.Drawing.Point(425, 144);
      this.BrowseOutputFolderButton.Margin = new System.Windows.Forms.Padding(5);
      this.BrowseOutputFolderButton.Name = "BrowseOutputFolderButton";
      this.BrowseOutputFolderButton.Size = new System.Drawing.Size(102, 38);
      this.BrowseOutputFolderButton.TabIndex = 5;
      this.BrowseOutputFolderButton.Text = "参照";
      this.BrowseOutputFolderButton.UseVisualStyleBackColor = false;
      this.BrowseOutputFolderButton.Click += new System.EventHandler(this.BrowseOutputFolderButton_Click);
      // 
      // LogTextBox
      // 
      this.LogTextBox.BackColor = System.Drawing.Color.White;
      this.LogTextBox.Font = new System.Drawing.Font("Noto Sans JP", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.LogTextBox.Location = new System.Drawing.Point(191, 559);
      this.LogTextBox.Margin = new System.Windows.Forms.Padding(5);
      this.LogTextBox.Name = "LogTextBox";
      this.LogTextBox.ReadOnly = true;
      this.LogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
      this.LogTextBox.Size = new System.Drawing.Size(381, 119);
      this.LogTextBox.TabIndex = 5;
      this.LogTextBox.Text = "";
      this.LogTextBox.WordWrap = false;
      // 
      // SpreadsheetUrlRichTextBox
      // 
      this.SpreadsheetUrlRichTextBox.Location = new System.Drawing.Point(55, 58);
      this.SpreadsheetUrlRichTextBox.Name = "SpreadsheetUrlRichTextBox";
      this.SpreadsheetUrlRichTextBox.Size = new System.Drawing.Size(502, 177);
      this.SpreadsheetUrlRichTextBox.TabIndex = 6;
      this.SpreadsheetUrlRichTextBox.Text = "";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(603, 705);
      this.Controls.Add(this.SpreadsheetUrlRichTextBox);
      this.Controls.Add(this.LogTextBox);
      this.Controls.Add(this.OptionsGroupBox);
      this.Controls.Add(this.GeneratePdfButton);
      this.Controls.Add(this.SpreadsheetUrlLabel);
      this.Font = new System.Drawing.Font("Noto Sans JP", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "MainForm";
      this.Padding = new System.Windows.Forms.Padding(20, 22, 20, 22);
      this.Text = "Main Window";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.OptionsGroupBox.ResumeLayout(false);
      this.OptionsGroupBox.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label SpreadsheetUrlLabel;
    private System.Windows.Forms.Button GeneratePdfButton;
    private System.Windows.Forms.CheckBox IncludeTodayCheckBox;
    private System.Windows.Forms.GroupBox OptionsGroupBox;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button BrowseOutputFolderButton;
    private System.Windows.Forms.FolderBrowserDialog OutputFolderBrowserDialog;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox PaperSizeComboBox;
    private System.Windows.Forms.TextBox OutputFolderTextbox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.RichTextBox LogTextBox;
    private System.Windows.Forms.RichTextBox SpreadsheetUrlRichTextBox;
  }
}

