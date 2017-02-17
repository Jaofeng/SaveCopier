namespace SaveCopier
{
	partial class MainForm
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
		/// 修改這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panel2 = new System.Windows.Forms.Panel();
			this.labRegist = new System.Windows.Forms.Label();
			this.labLogin = new System.Windows.Forms.Label();
			this.labStatus = new System.Windows.Forms.Label();
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.labCaption = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labClose = new System.Windows.Forms.Label();
			this.panLeft = new System.Windows.Forms.Panel();
			this.panMenu = new System.Windows.Forms.Panel();
			this.lnkMenu = new System.Windows.Forms.LinkLabel();
			this.panRight = new System.Windows.Forms.Panel();
			this.panGame = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labClass = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labGameClose = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labCopyright = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panLeft.SuspendLayout();
			this.panMenu.SuspendLayout();
			this.panRight.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.labRegist);
			this.panel2.Controls.Add(this.labLogin);
			this.panel2.Controls.Add(this.labStatus);
			this.panel2.Controls.Add(this.txtPwd);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.txtUser);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(300, 98);
			this.panel2.TabIndex = 0;
			// 
			// labRegist
			// 
			this.labRegist.Location = new System.Drawing.Point(223, 68);
			this.labRegist.Name = "labRegist";
			this.labRegist.Size = new System.Drawing.Size(65, 23);
			this.labRegist.TabIndex = 8;
			this.labRegist.Text = "註冊帳號";
			this.labRegist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labRegist.Paint += new System.Windows.Forms.PaintEventHandler(this.OutBox_Paint);
			this.labRegist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseClick);
			this.labRegist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseDown);
			this.labRegist.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseUp);
			// 
			// labLogin
			// 
			this.labLogin.Location = new System.Drawing.Point(154, 68);
			this.labLogin.Name = "labLogin";
			this.labLogin.Size = new System.Drawing.Size(65, 23);
			this.labLogin.TabIndex = 7;
			this.labLogin.Text = "登入";
			this.labLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.OutBox_Paint);
			this.labLogin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseClick);
			this.labLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseDown);
			this.labLogin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseUp);
			// 
			// labStatus
			// 
			this.labStatus.BackColor = System.Drawing.Color.Red;
			this.labStatus.ForeColor = System.Drawing.Color.White;
			this.labStatus.Location = new System.Drawing.Point(39, 68);
			this.labStatus.Name = "labStatus";
			this.labStatus.Size = new System.Drawing.Size(100, 23);
			this.labStatus.TabIndex = 6;
			this.labStatus.Text = "未登入";
			this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.InBox_Paint);
			// 
			// txtPwd
			// 
			this.txtPwd.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.txtPwd.Location = new System.Drawing.Point(188, 34);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.Size = new System.Drawing.Size(100, 23);
			this.txtPwd.TabIndex = 4;
			this.txtPwd.UseSystemPasswordChar = true;
			this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(156, 39);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 12);
			this.label6.TabIndex = 3;
			this.label6.Text = "密碼";
			// 
			// txtUser
			// 
			this.txtUser.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.txtUser.Location = new System.Drawing.Point(39, 34);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(100, 23);
			this.txtUser.TabIndex = 2;
			this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 73);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(29, 12);
			this.label7.TabIndex = 5;
			this.label7.Text = "狀態";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 39);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 12);
			this.label5.TabIndex = 1;
			this.label5.Text = "帳號";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.labCaption);
			this.panel4.Controls.Add(this.label3);
			this.panel4.Controls.Add(this.labClose);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Margin = new System.Windows.Forms.Padding(5);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(300, 26);
			this.panel4.TabIndex = 0;
			// 
			// labCaption
			// 
			this.labCaption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labCaption.ForeColor = System.Drawing.Color.Black;
			this.labCaption.Location = new System.Drawing.Point(0, 0);
			this.labCaption.Name = "labCaption";
			this.labCaption.Size = new System.Drawing.Size(271, 26);
			this.labCaption.TabIndex = 0;
			this.labCaption.Text = "Caption Name";
			this.labCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labCaption.Paint += new System.Windows.Forms.PaintEventHandler(this.InBox_Paint);
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Right;
			this.label3.Location = new System.Drawing.Point(271, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(3, 26);
			this.label3.TabIndex = 4;
			// 
			// labClose
			// 
			this.labClose.BackColor = System.Drawing.Color.WhiteSmoke;
			this.labClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.labClose.ForeColor = System.Drawing.Color.Black;
			this.labClose.Image = global::SaveCopier.Properties.Resources.Delete;
			this.labClose.Location = new System.Drawing.Point(274, 0);
			this.labClose.Name = "labClose";
			this.labClose.Size = new System.Drawing.Size(26, 26);
			this.labClose.TabIndex = 1;
			this.labClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labClose.Paint += new System.Windows.Forms.PaintEventHandler(this.labClose_Paint);
			this.labClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseClick);
			this.labClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseDown);
			this.labClose.MouseEnter += new System.EventHandler(this.MenuItem_MouseEnter);
			this.labClose.MouseLeave += new System.EventHandler(this.MenuItem_MouseLeave);
			this.labClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseUp);
			// 
			// panLeft
			// 
			this.panLeft.Controls.Add(this.statusStrip1);
			this.panLeft.Controls.Add(this.panMenu);
			this.panLeft.Controls.Add(this.label8);
			this.panLeft.Controls.Add(this.labCopyright);
			this.panLeft.Controls.Add(this.panel2);
			this.panLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panLeft.Location = new System.Drawing.Point(4, 4);
			this.panLeft.Name = "panLeft";
			this.panLeft.Size = new System.Drawing.Size(300, 467);
			this.panLeft.TabIndex = 0;
			// 
			// panMenu
			// 
			this.panMenu.AutoScroll = true;
			this.panMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panMenu.Controls.Add(this.lnkMenu);
			this.panMenu.Location = new System.Drawing.Point(0, 98);
			this.panMenu.Name = "panMenu";
			this.panMenu.Size = new System.Drawing.Size(300, 219);
			this.panMenu.TabIndex = 1;
			this.panMenu.TabStop = true;
			this.panMenu.MouseEnter += new System.EventHandler(this.List_MouseEnter);
			// 
			// lnkMenu
			// 
			this.lnkMenu.AutoSize = true;
			this.lnkMenu.Location = new System.Drawing.Point(-9, -109);
			this.lnkMenu.Name = "lnkMenu";
			this.lnkMenu.Size = new System.Drawing.Size(30, 12);
			this.lnkMenu.TabIndex = 0;
			this.lnkMenu.TabStop = true;
			this.lnkMenu.Text = "None";
			// 
			// panRight
			// 
			this.panRight.BackColor = System.Drawing.Color.Transparent;
			this.panRight.Controls.Add(this.panGame);
			this.panRight.Controls.Add(this.label4);
			this.panRight.Controls.Add(this.panel1);
			this.panRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panRight.Location = new System.Drawing.Point(309, 4);
			this.panRight.Name = "panRight";
			this.panRight.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.panRight.Size = new System.Drawing.Size(331, 467);
			this.panRight.TabIndex = 1;
			this.panRight.Visible = false;
			// 
			// panGame
			// 
			this.panGame.AutoScroll = true;
			this.panGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panGame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panGame.Location = new System.Drawing.Point(3, 30);
			this.panGame.Name = "panGame";
			this.panGame.Size = new System.Drawing.Size(328, 437);
			this.panGame.TabIndex = 1;
			this.panGame.TabStop = true;
			this.panGame.MouseEnter += new System.EventHandler(this.List_MouseEnter);
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(3, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(328, 4);
			this.label4.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.labClass);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.labGameClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(328, 26);
			this.panel1.TabIndex = 0;
			// 
			// labClass
			// 
			this.labClass.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labClass.ForeColor = System.Drawing.Color.Black;
			this.labClass.Location = new System.Drawing.Point(0, 0);
			this.labClass.Name = "labClass";
			this.labClass.Size = new System.Drawing.Size(299, 26);
			this.labClass.TabIndex = 0;
			this.labClass.Text = "Game Saves Backup Manager v1.00.1229.000";
			this.labClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labClass.Paint += new System.Windows.Forms.PaintEventHandler(this.InBox_Paint);
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Right;
			this.label2.Location = new System.Drawing.Point(299, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(3, 26);
			this.label2.TabIndex = 4;
			// 
			// labGameClose
			// 
			this.labGameClose.BackColor = System.Drawing.Color.WhiteSmoke;
			this.labGameClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.labGameClose.ForeColor = System.Drawing.Color.Black;
			this.labGameClose.Image = global::SaveCopier.Properties.Resources.Delete;
			this.labGameClose.Location = new System.Drawing.Point(302, 0);
			this.labGameClose.Name = "labGameClose";
			this.labGameClose.Size = new System.Drawing.Size(26, 26);
			this.labGameClose.TabIndex = 1;
			this.labGameClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labGameClose.Paint += new System.Windows.Forms.PaintEventHandler(this.labClose_Paint);
			this.labGameClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseClick);
			this.labGameClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseDown);
			this.labGameClose.MouseEnter += new System.EventHandler(this.MenuItem_MouseEnter);
			this.labGameClose.MouseLeave += new System.EventHandler(this.MenuItem_MouseLeave);
			this.labGameClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseUp);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(304, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(5, 467);
			this.label1.TabIndex = 6;
			// 
			// labCopyright
			// 
			this.labCopyright.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.labCopyright.ForeColor = System.Drawing.Color.Black;
			this.labCopyright.Location = new System.Drawing.Point(0, 437);
			this.labCopyright.Name = "labCopyright";
			this.labCopyright.Size = new System.Drawing.Size(300, 30);
			this.labCopyright.TabIndex = 5;
			this.labCopyright.Text = "v1.00.0000.000 / Powered By GreatsSoft";
			this.labCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label8.Location = new System.Drawing.Point(0, 432);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(300, 5);
			this.label8.TabIndex = 6;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 410);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(300, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 475);
			this.Controls.Add(this.panRight);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panLeft);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panLeft.ResumeLayout(false);
			this.panLeft.PerformLayout();
			this.panMenu.ResumeLayout(false);
			this.panMenu.PerformLayout();
			this.panRight.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label labCaption;
		private System.Windows.Forms.Label labClose;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panLeft;
		private System.Windows.Forms.Panel panMenu;
		private System.Windows.Forms.LinkLabel lnkMenu;
		private System.Windows.Forms.Panel panRight;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labClass;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labGameClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panGame;
		private System.Windows.Forms.TextBox txtPwd;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labRegist;
		private System.Windows.Forms.Label labLogin;
		private System.Windows.Forms.Label labStatus;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label labCopyright;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.StatusStrip statusStrip1;
	}
}

