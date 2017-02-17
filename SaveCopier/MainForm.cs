using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SaveCopier
{
	public partial class MainForm : Form
	{
		#region Variables
		Font _MenuFont = null;
		Color _MenuBackColor = Color.WhiteSmoke;
		Color _MenuTextColor = Color.Blue;
		Control _MouseDownMenu = null;
		int _MenuItemHeight = 0;
		int _GameItemHeight = 0;
		int _MainMenuWidth = 0;
		int _SaveButtonWidth = 0;
		int _IconAreaWidth = 0;
		string _CaptionText = string.Empty;
		string _GameRoot = string.Empty;
		string _IconPath = string.Empty;
		string _MenuPath = string.Empty;
		string _SelectedMenu = string.Empty;
		public string _LoginedUserID = string.Empty;
		#endregion

		#region Public Method : MainForm()
		public MainForm()
		{
			this.Hide();
			InitializeComponent();
			this.Text = labCaption.Text = _CaptionText = Program.Config["Caption"];
			_MenuBackColor = ColorTranslator.FromHtml(Program.Config["MenuBackColor"]);
			_MenuFont = new Font(Program.Config["MenuFont"], Convert.ToSingle(Program.Config["MenuFontSize"]));
			_MenuTextColor = ColorTranslator.FromHtml(Program.Config["MenuTextColor"]);
			_MenuItemHeight = Convert.ToInt32(Program.Config["MenuItemHeight"]);
			_GameItemHeight = Convert.ToInt32(Program.Config["GameItemHeight"]);
			_SaveButtonWidth = Convert.ToInt32(Program.Config["SaveButtonWidth"]);
			_IconAreaWidth = Convert.ToInt32(Program.Config["IconAreaWidth"]);
			labCopyright.Text = string.Format("{0}\nv{1}", Program.GetCopyright(), Application.ProductVersion);
			_MainMenuWidth = Convert.ToInt32(Program.Config["FormWidth"]);
			_GameRoot = Program.Config["GameRoot"];
			_IconPath = Program.Config["IconPath"];
			_MenuPath = Program.Config["MenuPath"];
			this.Size = new Size(_MainMenuWidth, Screen.PrimaryScreen.WorkingArea.Height);
			this.BackColor = ColorTranslator.FromHtml(Program.Config["FormBackColor"]);
			labCopyright.BackColor = this.BackColor;
			labCaption.BackColor = this.BackColor;
			this.Location = new Point(0, 0);
			panLeft.Width = this.Size.Width - this.Padding.Horizontal;
			ShowMenuItem();
			this.Show();
		}
		#endregion

		#region Private Method : void MainForm_KeyDown(object sender, KeyEventArgs e)
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape && panRight.Visible)
				CloseGameList();
		}
		#endregion

		#region Protected Override Method : void OnPaint(PaintEventArgs e)
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen p1 = new Pen(Color.WhiteSmoke, 1F);
			Pen p2 = new Pen(Color.DimGray, 1F);
			Point[] ps1 = { new Point(this.Bounds.Width - 1, 0), new Point(0, 0), new Point(0, this.Bounds.Height - 1) };
			Point[] ps2 = { new Point(this.Bounds.Width - 1, 0), new Point(this.Bounds.Width - 1, this.Bounds.Height - 1), new Point(0, this.Bounds.Height - 1) };
			Point[] ps3 = { new Point(_MainMenuWidth - 1, 0), new Point(_MainMenuWidth - 1, this.Bounds.Height - 1) };
			Point[] ps4 = { new Point(_MainMenuWidth, 0), new Point(_MainMenuWidth, this.Bounds.Height - 1) };
			g.DrawLines(p1, ps1);
			g.DrawLines(p2, ps2);
			if (panRight.Visible)
			{
				g.DrawLines(p2, ps3);
				g.DrawLines(p1, ps4);
			}
			base.OnPaint(e);
		}
		#endregion

		#region Private Method : void ShowMenuItem()
		private void ShowMenuItem()
		{
			panMenu.Hide();
			Application.DoEvents();
			panMenu.Controls.Clear();
			LinkLabel lnk = new LinkLabel();
			lnk.Name = "lnkMenu";
			lnk.Location = new Point(0, -100);
			lnk.Text = "None";
			panMenu.Controls.Add(lnk);
			Label lab = null;
			foreach (MenuInfo mi in Program.Menus)
			{
				lab = new Label();
				lab.FlatStyle = FlatStyle.Flat;
				lab.Name = string.Format("labMenuItem_{0}", mi.ID.Replace(" ", "_"));
				lab.MouseEnter += new EventHandler(MenuItem_MouseEnter);
				lab.MouseLeave += new EventHandler(MenuItem_MouseLeave);
				lab.MouseDown += new MouseEventHandler(MenuItem_MouseDown);
				lab.MouseUp += new MouseEventHandler(MenuItem_MouseUp);
				lab.MouseClick += new MouseEventHandler(MenuItem_MouseClick);
				lab.Text = mi.Name;
				lab.Font = _MenuFont;
				lab.ForeColor = _MenuTextColor;
				lab.BackColor = _MenuBackColor;
				lab.TextAlign = ContentAlignment.MiddleCenter;
				lab.Dock = DockStyle.Top;
				lab.Height = _MenuItemHeight;
				lab.Tag = mi.ID;
				panMenu.Controls.Add(lab);
				lab.BringToFront();
			}
			panMenu.Show();
			panMenu.Focus();
			Application.DoEvents();
		}
		#endregion

		#region Private Method : void DrawButtonStyle(Control c, bool isDown)
		private void DrawButtonStyle(Control c, bool isDown)
		{
			DrawButtonStyle(c, c.CreateGraphics(), isDown);
		}
		#endregion

		#region Private Method : void DrawButtonStyle(Control c, Graphics g, bool isDown)
		private void DrawButtonStyle(Control c, Graphics g, bool isDown)
		{
			if (Program.IsExit) return;
			Pen p1 = new Pen(Color.WhiteSmoke, 1F);
			Pen p2 = new Pen(Color.DimGray, 1F);
			Pen p3 = new Pen(Color.DarkGray, 1F);
			Point[] ps1 = { new Point(c.Bounds.Width - 1, 0), new Point(0, 0), new Point(0, c.Bounds.Height - 1) };
			Point[] ps2 = { new Point(c.Bounds.Width - 1, 0), new Point(c.Bounds.Width - 1, c.Bounds.Height - 1), new Point(0, c.Bounds.Height - 1) };
			if (c.Enabled)
			{
				if (!isDown)
				{
					g.DrawLines(p1, ps1);
					g.DrawLines(p2, ps2);
				}
				else
				{
					g.DrawLines(p2, ps1);
					g.DrawLines(p1, ps2);
				}
			}
			else
			{
				g.DrawLines(p3, ps1);
				g.DrawLines(p3, ps2);
			}
			Application.DoEvents();
		}
		#endregion

		#region Private Method : void CleanMenuItemButton(Label lab)
		private void CleanMenuItemButton(Control lab)
		{
			if (Program.IsExit) return;
			Graphics g = lab.CreateGraphics();
			Pen p = new Pen(_MenuBackColor, 2F);
			g.DrawRectangle(p, 0, 0, lab.Bounds.Width - 1, lab.Bounds.Height - 1);
			Application.DoEvents();
		}
		#endregion

		#region Private Method : void MenuItem_MouseUp(object sender, MouseEventArgs e)
		private void MenuItem_MouseUp(object sender, MouseEventArgs e)
		{
			if (_MouseDownMenu == null || Program.IsExit) return;
			Control lab = (Control)sender;
			if (!lab.Enabled) return;
			if (!_MouseDownMenu.Name.Equals(lab.Name, StringComparison.OrdinalIgnoreCase)) return;
			DrawButtonStyle(lab, false);
			_MouseDownMenu = null;
		}
		#endregion

		#region Private Method : void MenuItem_MouseDown(object sender, MouseEventArgs e)
		private void MenuItem_MouseDown(object sender, MouseEventArgs e)
		{
			Control lab = (Control)sender;
			if (!lab.Enabled) return;
			_MouseDownMenu = lab;
			DrawButtonStyle(lab, true);
		}
		#endregion

		#region Private Method : void MenuItem_MouseLeave(object sender, EventArgs e)
		private void MenuItem_MouseLeave(object sender, EventArgs e)
		{
			Control lab = (Control)sender;
			if (!lab.Name.StartsWith("labMenuItem_"))
				DrawButtonStyle(lab, false);
			else
				CleanMenuItemButton(lab);
		}
		#endregion

		#region Private Method : void MenuItem_MouseEnter(object sender, EventArgs e)
		private void MenuItem_MouseEnter(object sender, EventArgs e)
		{
			Control lab = (Control)sender;
			if (!lab.GetType().Equals(typeof(GameButton)))
				DrawButtonStyle(lab, false);
		    List_MouseEnter(lab.Parent, null);
		}
		#endregion

		#region Private Method : void MenuItem_MouseClick(object sender, MouseEventArgs e)
		private void MenuItem_MouseClick(object sender, MouseEventArgs e)
		{
			Control lab = (Control)sender;
			DrawButtonStyle(lab, false);
			if (lab.Equals(labClose))
			{
				if (MessageBox.Show("是否確定離開本程式？", _CaptionText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					Program.IsExit = true;
					this.Close();
				}
			}
			else if (lab.Equals(labGameClose))
			{
				CloseGameList();
				this.Refresh();
			}
			else if (lab.Equals(labRegist))
			{
				using (RegForm reg = new RegForm())
				{
					if (reg.ShowDialog(this) == DialogResult.OK)
					{
						_LoginedUserID = reg.UserID;
						SetLogin(_LoginedUserID);
						txtUser.Text = _LoginedUserID;
						txtPwd.Text = reg.Password;
					}
				}
			}
			else if (lab.Equals(labLogin))
			{
				if (lab.Text.Equals("登入"))
				{
					if (DataTier.UserLogin(txtUser.Text, txtPwd.Text))
						SetLogin(txtUser.Text);
					else
						MessageBox.Show("帳號密碼錯誤!! 請重新輸入!!\n(請注意密碼有區分大小寫)", _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
					SetLogout();
			}
			else if (lab.Parent.Equals(panMenu))
			{
				if (!panRight.Visible)
					this.Width = Screen.PrimaryScreen.WorkingArea.Width;
				_SelectedMenu = lab.Tag.ToString();
				ShowGameList(Program.Menus[_SelectedMenu]);
				this.Refresh();
			}
		}
		#endregion

		#region Private Method : void InBox_Paint(object sender, PaintEventArgs e)
		private void InBox_Paint(object sender, PaintEventArgs e)
		{
			DrawButtonStyle((Control)sender, e.Graphics, true);
		}
		#endregion

		#region Private Method : void OutBox_Paint(object sender, PaintEventArgs e)
		private void OutBox_Paint(object sender, PaintEventArgs e)
		{
			DrawButtonStyle((Control)sender, e.Graphics, false);
		}
		#endregion

		#region Privaet Method : void labClose_Paint(object sender, PaintEventArgs e)
		private void labClose_Paint(object sender, PaintEventArgs e)
		{
			DrawButtonStyle((Control)sender, e.Graphics, false);
		}
		#endregion

		#region Private Method : void ShowGameList(MenuInfo mi)
		private void ShowGameList(MenuInfo mi)
		{
			panRight.Hide();
			Application.DoEvents();
			labClass.Text = mi.Name;
			panGame.Controls.Clear();
			Application.DoEvents();
			LinkLabel lnk = new LinkLabel();
			lnk.Name = "lnkGame";
			lnk.Location = new Point(0, -100);
			lnk.Text = "None";
			panGame.Controls.Add(lnk);
			GameButton gb = null;
			List<GameInfo> lgi = Program.Games.FindAll(gi => gi.MenuID.Equals(mi.ID, StringComparison.OrdinalIgnoreCase));
			lgi.Sort((g1, g2) => g1.Sort.CompareTo(g2.Sort));
			foreach (GameInfo gi in lgi)
			{
				gb = new GameButton();
				gb.Text = gi.Name;
				gb.Name = string.Format("gb_{0}", gi.ID.Replace(" ", "_"));
				gb.MouseEnter +=new EventHandler(MenuItem_MouseEnter);
				gb.Font = _MenuFont;
				gb.ForeColor = _MenuTextColor;
				gb.BackColor = _MenuBackColor;
				gb.Click += new EventHandler(GameButton_Click);
				gb.Dock = DockStyle.Top;
				gb.Height = _GameItemHeight;
				gb.Tag = gi.ID;

				#region 處理圖示
				if (!string.IsNullOrEmpty(gi.Icon))
				{
					string iconPath = string.Empty;
					int iconIndex = 0;
					if (gi.Icon.IndexOf(',') != -1)
					{
						string[] sp = gi.Icon.Split(',');
						iconPath = sp[0];
						int.TryParse(sp[1], out iconIndex);
					}
					else
						iconPath = gi.Icon;
					//iconPath = iconPath.Replace("%Game%", gi.Path);
					if (!Path.IsPathRooted(iconPath))
						iconPath = Path.Combine(_IconPath, iconPath);
					Image img = null;
					if (File.Exists(iconPath))
					{
						string[] mime = GameInfo.MimeType(iconPath).ToLower().Split('/');

						switch (mime[0])
						{
							case "application":
								{
									Icon[] icons = IconHandler.IconsFromFile(iconPath, IconSize.Large);
									if (icons != null && icons.Length != 0)
									{
										if (icons.Length <= iconIndex)
											img = icons[0].ToBitmap();
										else
											img = icons[iconIndex].ToBitmap();
									}
									break;
								}
							case "image":
								{
									switch (mime[1])
									{
										case "x-icon":
											{
												Size s = Size.Empty;
												if (_IconAreaWidth > gb.Height)
													s = new Size(gb.Height, gb.Height);
												else
													s = new Size(_IconAreaWidth, _IconAreaWidth);
												img = new Icon(iconPath, s).ToBitmap();
												break;
											}
										default:
											img = Image.FromFile(iconPath);
											break;
									}
									break;
								}
						}
					}
					if (img != null)
					{
						gb.IconWidth = _IconAreaWidth;
						gb.Icon = img;
						gb.ShowIcon = true;
					}
				}
				#endregion

				#region 處理上/下傳按鍵
				gb.ShowSaveButton = gi.HasSave;
				if (gi.HasSave)
				{
					gb.UploadButtonText = "上傳進度";
					gb.UploadButtonFont = new Font("新細明體", 9);
					gb.UploadButtonImage = global::SaveCopier.Properties.Resources.Previous;
					gb.UploadButtonEnabled = !string.IsNullOrEmpty(_LoginedUserID);
					gb.UploadClick += new EventHandler(GameButton_UploadClick);
					gb.DownloadButtonText = "下載進度";
					gb.DownloadButtonFont = new Font("新細明體", 9);
					gb.DownloadButtonImage = global::SaveCopier.Properties.Resources.Next;
					gb.DownloadButtonEnabled = !string.IsNullOrEmpty(_LoginedUserID);
					gb.DownloadClick += new EventHandler(GameButton_DownloadClick);
					gb.SaveButtonWidth = _SaveButtonWidth;
				}
				#endregion

				panGame.Controls.Add(gb);
				gb.BringToFront();
				Application.DoEvents();
			}
			panRight.Show();
			Application.DoEvents();
			panGame.Focus();
		}
		#endregion

		#region Private Method : void SetLogin(string userId)
		private void SetLogin(string userId)
		{
			_LoginedUserID = userId;
			labStatus.Text = "已登入";
			labStatus.BackColor = Color.Green;
			labStatus.ForeColor = Color.White;
			txtUser.ReadOnly = true;
			txtPwd.ReadOnly = true;
			labLogin.Text = "登出";
			labRegist.Enabled = false;
			if (panRight.Visible)
				ShowGameList(Program.Menus[_SelectedMenu]);
		}
		#endregion

		#region Private Method : void SetLogout()
		private void SetLogout()
		{
			_LoginedUserID = string.Empty;
			labStatus.Text = "未登入";
			labStatus.BackColor = Color.Red;
			labStatus.ForeColor = Color.White;
			labLogin.Text = "登入";
			labRegist.Enabled = true;
			txtUser.Clear();
			txtUser.ReadOnly = false;
			txtPwd.Clear();
			txtPwd.ReadOnly = false;
			if (panRight.Visible)
				ShowGameList(Program.Menus[_SelectedMenu]);
			txtUser.Focus();
		}
		#endregion

		#region Private Method : void GameButton_UploadClick(object sender, EventArgs e)
		private void GameButton_UploadClick(object sender, EventArgs e)
		{
			GameButton gb = (GameButton)sender;
			gb.UploadButtonEnabled = false;
			Application.DoEvents();
			GameInfo gi = Program.Games[gb.Tag.ToString()];
			if (gi == null || !gi.HasSave) return;
			using (CopyForm cf = new CopyForm(_LoginedUserID, false))
			{
				cf.GameInfo = gi;
				DialogResult dr = cf.ShowDialog(this);
				switch (dr)
				{
					case DialogResult.OK:
						MessageBox.Show("遊戲進度資料檔已上傳完畢!!", _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case DialogResult.Abort:
						MessageBox.Show("找不到遊戲進度儲存目錄，無法備份進度檔!!", _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
					default:
						MessageBox.Show("遊戲進度資料檔尚未備份完畢，請重新上傳!!", _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
				}
				gb.UploadButtonEnabled = true;
			}
		}
		#endregion

		#region Private Method : void GameButton_DownloadClick(object sender, EventArgs e)
		private void GameButton_DownloadClick(object sender, EventArgs e)
		{
			GameButton gb = (GameButton)sender;
			gb.DownloadButtonEnabled = false;
			Application.DoEvents();
			GameInfo gi = Program.Games[gb.Tag.ToString()];
			if (gi == null || !gi.HasSave) return;
			string userSavesPath = Path.Combine(Path.Combine(Program.Config["SavesPath"], gi.ID), _LoginedUserID);
			if (!Directory.Exists(userSavesPath))
			{
				MessageBox.Show("您沒有備份此遊戲進度資料!!", _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				CopyForm cf = new CopyForm(_LoginedUserID, true);
				cf.GameInfo = gi;
				if (cf.ShowDialog(this) == DialogResult.OK)
					MessageBox.Show("遊戲進度資料檔已下載完畢!!", _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show("遊戲進度資料檔尚未下載完畢，請重新下載!!", _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			gb.DownloadButtonEnabled = true;
		}
		#endregion

		#region Private Method : void GameButton_Click(object sender, EventArgs e)
		private void GameButton_Click(object sender, EventArgs e)
		{
			GameButton gb = (GameButton)sender;
			GameInfo gi = Program.Games[gb.Tag.ToString()];
			if (gi == null) return;
			string fn = gi.Exec;
			string gPath = gi.Path;
			if (string.IsNullOrEmpty(gPath))
				gPath = Path.Combine(_GameRoot, gi.ID);
			//else
			//    gPath = gPath.Replace("%GameRoot%", _GameRoot);
			//fn = fn.Replace("%Game%", gPath);
			if (!Path.IsPathRooted(fn))
				fn = Path.Combine(gPath, fn);
			if (!File.Exists(fn))
			{
				MessageBox.Show(string.Format("找不到遊戲 \"{0}\" 的執行檔或捷徑檔!!", gi.Name), _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			else
			{
				try
				{
					ProcessStartInfo psi = new ProcessStartInfo(fn);
					psi.WorkingDirectory = gPath;
					Process.Start(psi);
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("發生錯誤!!\n\n{0}", ex.Message), _CaptionText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
		}
		#endregion

		#region Private Method : void List_MouseEnter(object sender, EventArgs e)
		private void List_MouseEnter(object sender, EventArgs e)
		{
			Panel pan = (Panel)sender;
			pan.Focus();
		}
		#endregion

		#region Private Method : void CloseGameList()
		private void CloseGameList()
		{
			this.Width = _MainMenuWidth;
			_SelectedMenu = string.Empty;
			panRight.Visible = false;
			panMenu.Focus();
		}
		#endregion

		#region Private Method : void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
		private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && !txtPwd.ReadOnly)
			{
				MenuItem_MouseClick(labLogin, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 0));
				List_MouseEnter(panMenu, null);
				e.Handled = true;
			}
		}
		#endregion

		#region Private Method : void txtUser_KeyPress(object sender, KeyPressEventArgs e)
		private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && !txtUser.ReadOnly)
			{
				txtPwd.Focus();
				e.Handled = true;
			}
		}
		#endregion
	}
}
