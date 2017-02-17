namespace SaveCopier
{
	partial class CopyForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyForm));
			this.labCopy = new System.Windows.Forms.Label();
			this.pbCopy = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// labCopy
			// 
			this.labCopy.AutoEllipsis = true;
			this.labCopy.Location = new System.Drawing.Point(24, 22);
			this.labCopy.Name = "labCopy";
			this.labCopy.Size = new System.Drawing.Size(363, 46);
			this.labCopy.TabIndex = 1;
			// 
			// pbCopy
			// 
			this.pbCopy.Location = new System.Drawing.Point(26, 71);
			this.pbCopy.Name = "pbCopy";
			this.pbCopy.Size = new System.Drawing.Size(361, 14);
			this.pbCopy.TabIndex = 2;
			// 
			// CopyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 108);
			this.Controls.Add(this.pbCopy);
			this.Controls.Add(this.labCopy);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CopyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "複製檔案中，請稍後...";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CopyForm_FormClosing);
			this.Shown += new System.EventHandler(this.CopyForm_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labCopy;
		private System.Windows.Forms.ProgressBar pbCopy;
	}
}