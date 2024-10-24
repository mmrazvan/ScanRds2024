namespace ScanApp;

partial class ScanForm
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose( bool disposing )
	{
		if (disposing && ( components != null ))
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
		button1 = new Button();
		textBoxScan = new TextBox();
		progressBarScan = new ProgressBar();
		labelProgress = new Label();
		buttonSelectWorkfolder = new Button();
		listBoxCounty = new ListBox();
		checkedListBox1 = new CheckedListBox();
		buttonSelectAll = new Button();
		groupBox1 = new GroupBox();
		groupBox2 = new GroupBox();
		labelCountyDetails = new Label();
		groupBox3 = new GroupBox();
		labelStatus = new Label();
		listBoxDetails = new ListBox();
		button2 = new Button();
		labelActions = new Label();
		groupBox1.SuspendLayout();
		groupBox2.SuspendLayout();
		groupBox3.SuspendLayout();
		SuspendLayout();
		// 
		// button1
		// 
		button1.Anchor =     AnchorStyles.Top  |  AnchorStyles.Right ;
		button1.Location = new Point(250, 18);
		button1.Name = "button1";
		button1.Size = new Size(120, 38);
		button1.TabIndex = 0;
		button1.Text = "Complete";
		button1.UseVisualStyleBackColor = true;
		button1.Click +=  Button1_Click ;
		// 
		// textBoxScan
		// 
		textBoxScan.Anchor = AnchorStyles.Top;
		textBoxScan.Font = new Font("Segoe UI", 18F);
		textBoxScan.Location = new Point(346, 22);
		textBoxScan.MinimumSize = new Size(286, 39);
		textBoxScan.Name = "textBoxScan";
		textBoxScan.Size = new Size(286, 39);
		textBoxScan.TabIndex = 1;
		textBoxScan.Enter +=  TextBoxScan_Enter ;
		textBoxScan.KeyDown +=  TextBoxScan_KeyDown ;
		textBoxScan.Leave +=  TextBoxScan_Leave ;
		// 
		// progressBarScan
		// 
		progressBarScan.Anchor =      AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
		progressBarScan.Location = new Point(6, 67);
		progressBarScan.Name = "progressBarScan";
		progressBarScan.Size = new Size(942, 36);
		progressBarScan.TabIndex = 2;
		// 
		// labelProgress
		// 
		labelProgress.Anchor =      AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
		labelProgress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		labelProgress.Location = new Point(6, 106);
		labelProgress.Name = "labelProgress";
		labelProgress.Size = new Size(942, 27);
		labelProgress.TabIndex = 3;
		labelProgress.Text = "label1";
		labelProgress.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// buttonSelectWorkfolder
		// 
		buttonSelectWorkfolder.Anchor =     AnchorStyles.Bottom  |  AnchorStyles.Left ;
		buttonSelectWorkfolder.Location = new Point(12, 533);
		buttonSelectWorkfolder.Name = "buttonSelectWorkfolder";
		buttonSelectWorkfolder.Size = new Size(117, 23);
		buttonSelectWorkfolder.TabIndex = 5;
		buttonSelectWorkfolder.Text = "Select work folder";
		buttonSelectWorkfolder.UseVisualStyleBackColor = true;
		buttonSelectWorkfolder.Click +=  ButtonSelectWorkfolder_Click ;
		// 
		// listBoxCounty
		// 
		listBoxCounty.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Right ;
		listBoxCounty.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
		listBoxCounty.FormattingEnabled = true;
		listBoxCounty.ItemHeight = 20;
		listBoxCounty.Location = new Point(6, 18);
		listBoxCounty.Name = "listBoxCounty";
		listBoxCounty.Size = new Size(238, 324);
		listBoxCounty.TabIndex = 6;
		listBoxCounty.SelectedValueChanged +=  ListBoxCounty_SelectedValueChanged ;
		// 
		// checkedListBox1
		// 
		checkedListBox1.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Right ;
		checkedListBox1.Font = new Font("Segoe UI", 11F);
		checkedListBox1.FormattingEnabled = true;
		checkedListBox1.Location = new Point(250, 62);
		checkedListBox1.Name = "checkedListBox1";
		checkedListBox1.Size = new Size(120, 290);
		checkedListBox1.TabIndex = 7;
		// 
		// buttonSelectAll
		// 
		buttonSelectAll.Anchor =     AnchorStyles.Bottom  |  AnchorStyles.Right ;
		buttonSelectAll.Location = new Point(250, 358);
		buttonSelectAll.Name = "buttonSelectAll";
		buttonSelectAll.Size = new Size(120, 38);
		buttonSelectAll.TabIndex = 8;
		buttonSelectAll.Text = "Select all";
		buttonSelectAll.UseVisualStyleBackColor = true;
		buttonSelectAll.Click +=  ButtonSelectAll_Click ;
		// 
		// groupBox1
		// 
		groupBox1.Anchor =      AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
		groupBox1.Controls.Add(labelActions);
		groupBox1.Controls.Add(textBoxScan);
		groupBox1.Controls.Add(progressBarScan);
		groupBox1.Controls.Add(labelProgress);
		groupBox1.Location = new Point(12, 12);
		groupBox1.MinimumSize = new Size(300, 118);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new Size(955, 136);
		groupBox1.TabIndex = 9;
		groupBox1.TabStop = false;
		groupBox1.Text = "Scan area";
		// 
		// groupBox2
		// 
		groupBox2.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Right ;
		groupBox2.Controls.Add(labelCountyDetails);
		groupBox2.Controls.Add(listBoxCounty);
		groupBox2.Controls.Add(button1);
		groupBox2.Controls.Add(buttonSelectAll);
		groupBox2.Controls.Add(checkedListBox1);
		groupBox2.Location = new Point(591, 154);
		groupBox2.Name = "groupBox2";
		groupBox2.Size = new Size(376, 402);
		groupBox2.TabIndex = 10;
		groupBox2.TabStop = false;
		groupBox2.Text = "Manual scan area";
		// 
		// labelCountyDetails
		// 
		labelCountyDetails.Anchor =      AnchorStyles.Bottom  |  AnchorStyles.Left   |  AnchorStyles.Right ;
		labelCountyDetails.BorderStyle = BorderStyle.Fixed3D;
		labelCountyDetails.Location = new Point(6, 358);
		labelCountyDetails.Name = "labelCountyDetails";
		labelCountyDetails.Size = new Size(238, 35);
		labelCountyDetails.TabIndex = 9;
		labelCountyDetails.Text = "Select";
		labelCountyDetails.TextAlign = ContentAlignment.MiddleLeft;
		// 
		// groupBox3
		// 
		groupBox3.Anchor =       AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
		groupBox3.Controls.Add(labelStatus);
		groupBox3.Controls.Add(listBoxDetails);
		groupBox3.Location = new Point(12, 154);
		groupBox3.Name = "groupBox3";
		groupBox3.Size = new Size(573, 372);
		groupBox3.TabIndex = 11;
		groupBox3.TabStop = false;
		groupBox3.Text = "Statistics area";
		// 
		// labelStatus
		// 
		labelStatus.Anchor =       AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
		labelStatus.BorderStyle = BorderStyle.FixedSingle;
		labelStatus.Font = new Font("Segoe UI", 10F);
		labelStatus.Location = new Point(298, 19);
		labelStatus.Name = "labelStatus";
		labelStatus.Size = new Size(269, 343);
		labelStatus.TabIndex = 1;
		labelStatus.Text = "No errors";
		// 
		// listBoxDetails
		// 
		listBoxDetails.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left ;
		listBoxDetails.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
		listBoxDetails.FormattingEnabled = true;
		listBoxDetails.ItemHeight = 20;
		listBoxDetails.Location = new Point(6, 18);
		listBoxDetails.Name = "listBoxDetails";
		listBoxDetails.Size = new Size(291, 344);
		listBoxDetails.TabIndex = 0;
		// 
		// button2
		// 
		button2.Anchor =     AnchorStyles.Bottom  |  AnchorStyles.Left ;
		button2.Location = new Point(135, 533);
		button2.Name = "button2";
		button2.Size = new Size(75, 23);
		button2.TabIndex = 12;
		button2.Text = "button2";
		button2.UseVisualStyleBackColor = true;
		button2.Click +=  Button2_Click ;
		// 
		// labelActions
		// 
		labelActions.BorderStyle = BorderStyle.Fixed3D;
		labelActions.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Pixel);
		labelActions.Location = new Point(6, 22);
		labelActions.Name = "labelActions";
		labelActions.Size = new Size(334, 39);
		labelActions.TabIndex = 4;
		labelActions.Text = "Status";
		labelActions.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// ScanForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(979, 568);
		Controls.Add(button2);
		Controls.Add(groupBox3);
		Controls.Add(groupBox2);
		Controls.Add(groupBox1);
		Controls.Add(buttonSelectWorkfolder);
		Name = "ScanForm";
		Text = "Scan";
		Load +=  ScanForm_Load ;
		groupBox1.ResumeLayout(false);
		groupBox1.PerformLayout();
		groupBox2.ResumeLayout(false);
		groupBox3.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private Button button1;
	private TextBox textBoxScan;
	private ProgressBar progressBarScan;
	private Label labelProgress;
	private Button buttonSelectWorkfolder;
	private ListBox listBoxCounty;
	private CheckedListBox checkedListBox1;
	private Button buttonSelectAll;
	private GroupBox groupBox1;
	private GroupBox groupBox2;
	private GroupBox groupBox3;
	private Button button2;
	private ListBox listBoxDetails;
	private Label labelStatus;
	private Label labelCountyDetails;
	private Label labelActions;
}
