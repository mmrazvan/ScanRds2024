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
		SuspendLayout();
		// 
		// button1
		// 
		button1.Location = new Point(295, 13);
		button1.Name = "button1";
		button1.Size = new Size(75, 23);
		button1.TabIndex = 0;
		button1.Text = "button1";
		button1.UseVisualStyleBackColor = true;
		button1.Click +=  Button1_Click ;
		// 
		// textBoxScan
		// 
		textBoxScan.Location = new Point(12, 13);
		textBoxScan.Name = "textBoxScan";
		textBoxScan.Size = new Size(277, 23);
		textBoxScan.TabIndex = 1;
		// 
		// progressBarScan
		// 
		progressBarScan.Location = new Point(12, 57);
		progressBarScan.Name = "progressBarScan";
		progressBarScan.Size = new Size(950, 23);
		progressBarScan.TabIndex = 2;
		// 
		// labelProgress
		// 
		labelProgress.AutoSize = true;
		labelProgress.Location = new Point(461, 83);
		labelProgress.Name = "labelProgress";
		labelProgress.Size = new Size(38, 15);
		labelProgress.TabIndex = 3;
		labelProgress.Text = "label1";
		// 
		// buttonSelectWorkfolder
		// 
		buttonSelectWorkfolder.Location = new Point(12, 462);
		buttonSelectWorkfolder.Name = "buttonSelectWorkfolder";
		buttonSelectWorkfolder.Size = new Size(117, 23);
		buttonSelectWorkfolder.TabIndex = 5;
		buttonSelectWorkfolder.Text = "Select work folder";
		buttonSelectWorkfolder.UseVisualStyleBackColor = true;
		buttonSelectWorkfolder.Click +=  ButtonSelectWorkfolder_Click ;
		// 
		// ScanForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(974, 497);
		Controls.Add(buttonSelectWorkfolder);
		Controls.Add(labelProgress);
		Controls.Add(progressBarScan);
		Controls.Add(textBoxScan);
		Controls.Add(button1);
		Name = "ScanForm";
		Text = "Scan";
		Load +=  ScanForm_Load ;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Button button1;
	private TextBox textBoxScan;
	private ProgressBar progressBarScan;
	private Label labelProgress;
	private Button buttonSelectWorkfolder;
}
