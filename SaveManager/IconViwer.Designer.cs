namespace SaveCopier
{
	partial class IconViwer
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.panIcons = new System.Windows.Forms.FlowLayoutPanel();
			this.btnUse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "請選擇欲使用圖示";
			// 
			// txtFile
			// 
			this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFile.BackColor = System.Drawing.SystemColors.Window;
			this.txtFile.Location = new System.Drawing.Point(15, 29);
			this.txtFile.Name = "txtFile";
			this.txtFile.ReadOnly = true;
			this.txtFile.Size = new System.Drawing.Size(292, 22);
			this.txtFile.TabIndex = 1;
			// 
			// panIcons
			// 
			this.panIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panIcons.AutoScroll = true;
			this.panIcons.BackColor = System.Drawing.SystemColors.Window;
			this.panIcons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panIcons.Location = new System.Drawing.Point(15, 58);
			this.panIcons.Name = "panIcons";
			this.panIcons.Size = new System.Drawing.Size(373, 165);
			this.panIcons.TabIndex = 2;
			// 
			// btnUse
			// 
			this.btnUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUse.Location = new System.Drawing.Point(313, 29);
			this.btnUse.Name = "btnUse";
			this.btnUse.Size = new System.Drawing.Size(75, 23);
			this.btnUse.TabIndex = 3;
			this.btnUse.Text = "使用";
			this.btnUse.UseVisualStyleBackColor = true;
			this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
			// 
			// IconViwer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(401, 238);
			this.Controls.Add(this.btnUse);
			this.Controls.Add(this.panIcons);
			this.Controls.Add(this.txtFile);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "IconViwer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "選擇圖示";
			this.Shown += new System.EventHandler(this.IconViwer_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.FlowLayoutPanel panIcons;
		private System.Windows.Forms.Button btnUse;
	}
}