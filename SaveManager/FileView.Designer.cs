namespace SaveCopier
{
	partial class FileView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileView));
			this.label1 = new System.Windows.Forms.Label();
			this.txtExt = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cbIncSub = new System.Windows.Forms.CheckBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.btnRefrash = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lvFile = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(30, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "副檔名：";
			// 
			// txtExt
			// 
			this.txtExt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtExt.Location = new System.Drawing.Point(89, 40);
			this.txtExt.Name = "txtExt";
			this.txtExt.Size = new System.Drawing.Size(147, 22);
			this.txtExt.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbIncSub);
			this.panel1.Controls.Add(this.btnApply);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.txtFolder);
			this.panel1.Controls.Add(this.btnRefrash);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.txtExt);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(5, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 75);
			this.panel1.TabIndex = 0;
			// 
			// cbIncSub
			// 
			this.cbIncSub.AutoSize = true;
			this.cbIncSub.Location = new System.Drawing.Point(242, 43);
			this.cbIncSub.Name = "cbIncSub";
			this.cbIncSub.Size = new System.Drawing.Size(84, 16);
			this.cbIncSub.TabIndex = 7;
			this.cbIncSub.Text = "包含子目錄";
			this.cbIncSub.UseVisualStyleBackColor = true;
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnApply.Location = new System.Drawing.Point(435, 40);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 5;
			this.btnApply.Text = "套用";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "儲存檔目錄：";
			// 
			// txtFolder
			// 
			this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFolder.Location = new System.Drawing.Point(89, 11);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(421, 22);
			this.txtFolder.TabIndex = 1;
			// 
			// btnRefrash
			// 
			this.btnRefrash.Location = new System.Drawing.Point(332, 40);
			this.btnRefrash.Name = "btnRefrash";
			this.btnRefrash.Size = new System.Drawing.Size(75, 23);
			this.btnRefrash.TabIndex = 4;
			this.btnRefrash.Text = "重新整理";
			this.btnRefrash.UseVisualStyleBackColor = true;
			this.btnRefrash.Click += new System.EventHandler(this.btnRefrash_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lvFile);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(5, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
			this.groupBox1.Size = new System.Drawing.Size(520, 255);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "以下為搜尋結果，請驗證是否正確";
			// 
			// lvFile
			// 
			this.lvFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chPath,
            this.chTime});
			this.lvFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFile.Location = new System.Drawing.Point(10, 20);
			this.lvFile.Name = "lvFile";
			this.lvFile.Size = new System.Drawing.Size(500, 225);
			this.lvFile.TabIndex = 1;
			this.lvFile.UseCompatibleStateImageBehavior = false;
			this.lvFile.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "名稱";
			this.chName.Width = 150;
			// 
			// chPath
			// 
			this.chPath.Text = "子目錄";
			this.chPath.Width = 200;
			// 
			// chTime
			// 
			this.chTime.Text = "日期";
			this.chTime.Width = 120;
			// 
			// FileView
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(530, 340);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FileView";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "檔案清單";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtExt;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnRefrash;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lvFile;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chPath;
		private System.Windows.Forms.ColumnHeader chTime;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.CheckBox cbIncSub;

	}
}