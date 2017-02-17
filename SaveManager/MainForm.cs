using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SaveCopier
{
	public partial class MainForm : Form
	{
		bool _IsAppendMode = false;
		bool _ConfigChanged = false;
		bool _GameInfoChanged = false;
		bool _UserDataChanged = false;
		int _TabPageSelectedIndex = -1;
		MenuInfo _SelectedMenuInfo = null;
		GameInfo _SelectedGameInfo = null;
		BindingSource _UserSource = null;

		public MainForm()
		{
			InitializeComponent();
		}

		#region Private Method : void MainForm_Load(object sender, EventArgs e)
		private void MainForm_Load(object sender, EventArgs e)
		{
			// TODO: 這行程式碼會將資料載入 'usersDataSet.TB_Users' 資料表。您可以視需要進行移動或移除。
			gbSample.ShowIcon = true;
			gbSample.ShowSaveButton = true;
			gbSample.UploadButtonImage = global::SaveCopier.Properties.Resources.Previous;
			gbSample.DownloadButtonImage = global::SaveCopier.Properties.Resources.Next;
			gbSample.Icon = global::SaveCopier.Properties.Resources.Computer;
			LoadConfig();
			LoadGameMenu();
		}
		#endregion

		#region Private Method : void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_ConfigChanged)
			{
				DialogResult dr = MessageBox.Show("參數資料已修改，是否儲存 ?\n\n「是」儲存參數，並關閉程式\n「否」不儲存，且關閉程式\n「取消」關閉本詢問窗且不做任何事", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (dr)
				{
					case DialogResult.Yes:
						btnSaveConfig_Click(null, null); break;
					case DialogResult.No:
						_ConfigChanged = false; break;
					case DialogResult.Cancel:
						e.Cancel = true;
						return;
				}
			}
			if (_GameInfoChanged)
			{
				DialogResult res = MessageBox.Show("遊戲資料已變更，是否儲存!?\n\n「是」儲存遊戲資料\n「否」不儲存\n「取消」關閉本詢問窗且不做任何事", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (res)
				{
					case DialogResult.Yes:
						btnSaveGame_Click(null, null); break;
					case DialogResult.No:
						_GameInfoChanged = false; break;
					case DialogResult.Cancel:
						e.Cancel = true;
						return;
				}
			}
			if (_UserDataChanged)
			{
				DialogResult dr = MessageBox.Show("使用者帳號清單未儲存，是否儲存 ?\n\n「是」儲存使用者帳號清單\n「否」不儲存並切換至選擇的頁籤\n「取消」取消切換頁籤", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (dr)
				{
					case DialogResult.Yes:
						tsbSave_Click(null, null);
						break;
					case DialogResult.No:
						_UserDataChanged = false;
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						return;
				}
			}

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
			Pen p1 = new Pen(Color.WhiteSmoke, 1F);
			Pen p2 = new Pen(Color.DimGray, 1F);
			Pen p3 = new Pen(Color.DarkGray, 1F);
			Point[] ps1 = { new Point((int)c.Bounds.Width - 1, 0), new Point(0, 0), new Point(0, (int)c.Bounds.Height - 1) };
			Point[] ps2 = { new Point((int)c.Bounds.Width - 1, 0), new Point((int)c.Bounds.Width - 1, (int)c.Bounds.Height - 1), new Point(0, (int)c.Bounds.Height - 1) };
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
		}
		#endregion

		#region Private Method : void tabManager_SelectedIndexChanged(object sender, EventArgs e)
		private void tabManager_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!tabManager.Enabled) return;
			if (_UserDataChanged)
			{
				DialogResult dr = MessageBox.Show("使用者帳號清單未儲存，是否儲存 ?\n\n「是」儲存使用者帳號清單\n「否」不儲存並切換至選擇的頁籤\n「取消」取消切換頁籤", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (dr)
				{
					case DialogResult.Yes:
						tsbSave_Click(null, null);
						break;
					case DialogResult.No:
						_UserDataChanged = false;
						break;
					case DialogResult.Cancel:
						tabManager.Enabled = false;
						tabManager.SelectedIndex = _TabPageSelectedIndex;
						tabManager.Enabled = true;
						return;
				}
			}
			_TabPageSelectedIndex = tabManager.SelectedIndex;
			if (tabManager.SelectedIndex != 2) return;
			LoadUsers();
		}
		#endregion

		#region Config Tab
		#region Private Method : void LoadConfig()
		private void LoadConfig()
		{
			txtCaption.Text = Program.Config["Caption"];
			txtFormBackColor.Text = Program.Config["FormBackColor"];
			txtGameRoot.Text = Program.Config["GameRoot"];
			txtIconPath.Text = Program.Config["IconPath"];
			txtSavesPath.Text = Program.Config["SavesPath"];
			txtMenuFont.Text = Program.Config["MenuFont"] + "," + Program.Config["MenuFontSize"];
			txtMenuBackColor.Text = Program.Config["MenuBackColor"];
			txtMenuTextColor.Text = Program.Config["MenuTextColor"];
			udFormWidth.Value = Convert.ToDecimal(Program.Config["FormWidth"]);
			udMenuHeight.Value = Convert.ToDecimal(Program.Config["MenuItemHeight"]);
			udGameItemHeight.Value = Convert.ToDecimal(Program.Config["GameItemHeight"]);
			udSaveButtonWidth.Value = Convert.ToDecimal(Program.Config["SaveButtonWidth"]);
			udIconWidth.Value = Convert.ToDecimal(Program.Config["IconAreaWidth"]);
			_ConfigChanged = false;
		}
		#endregion

		#region Private Method : void btnSaveConfig_Click(object sender, EventArgs e)
		private void btnSaveConfig_Click(object sender, EventArgs e)
		{
			Program.Config["Caption"] = txtCaption.Text;
			Program.Config["FormBackColor"] = txtFormBackColor.Text;
			Program.Config["GameRoot"] = txtGameRoot.Text;
			Program.Config["IconPath"] = txtIconPath.Text;
			Program.Config["SavesPath"] = txtSavesPath.Text;
			Program.Config["MenuFont"] = txtMenuFont.Text.Split(',')[0];
			Program.Config["MenuFontSize"] = txtMenuFont.Text.Split(',')[1];
			Program.Config["MenuBackColor"] = txtMenuBackColor.Text;
			Program.Config["MenuTextColor"] = txtMenuTextColor.Text;
			Program.Config["FormWidth"] = udFormWidth.Value.ToString();
			Program.Config["MenuItemHeight"] = udMenuHeight.Value.ToString();
			Program.Config["GameItemHeight"] = udGameItemHeight.Value.ToString();
			Program.Config["SaveButtonWidth"] = udSaveButtonWidth.Value.ToString();
			Program.Config["IconAreaWidth"] = udIconWidth.Value.ToString();
			if (!DataTier.SaveConfig())
				MessageBox.Show("參數儲存失敗，請聯絡程式開發人員!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			else
			{
				MessageBox.Show("參數儲存成功!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				_ConfigChanged = false;
			}
		}
		#endregion

		#region labMenuSample Events
		private void labMenuSample_MouseUp(object sender, MouseEventArgs e)
		{
			DrawButtonStyle(labMenuSample, false);
		}

		private void labMenuSample_MouseDown(object sender, MouseEventArgs e)
		{
			DrawButtonStyle(labMenuSample, true);
		}

		private void labMenuSample_Paint(object sender, PaintEventArgs e)
		{
			DrawButtonStyle((Control)sender, e.Graphics, false);
		}
		#endregion

		#region Private Mwthod : void ColorChooser_Click(object sender, EventArgs e)
		private void ColorChooser_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			string txtName = "txt" + btn.Name.Substring(3);
			Control[] txt = btn.Parent.Controls.Find(txtName, false);
			if (txt.Length == 0) return;
			cDialog.Color = ColorTranslator.FromHtml(txt[0].Text);
			if (cDialog.ShowDialog(this) == DialogResult.OK)
				txt[0].Text = ColorTranslator.ToHtml(cDialog.Color);
		}
		#endregion

		#region Private Method : void FontChooser_Click(object sender, EventArgs e)
		private void FontChooser_Click(object sender, EventArgs e)
		{
			fDialog.Font = labMenuSample.Font;
			if (fDialog.ShowDialog(this) == DialogResult.OK)
				txtMenuFont.Text = fDialog.Font.Name + "," + fDialog.Font.Size.ToString();
		}
		#endregion

		#region Private Method : void FolderChooser_Click(object sender, EventArgs e)
		private void FolderChooser_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			string txtName = "txt" + btn.Name.Substring(3);
			Control txt = btn.Parent.Controls[txtName];
			if (txt == null) return;
			if (string.IsNullOrEmpty(txt.Text))
				fbDialog.SelectedPath = Program.Config["GameRoot"];
			else
				fbDialog.SelectedPath = txt.Text;
			//if (string.IsNullOrEmpty(txt.Text) || txt.Text.Equals("%GameRoot%"))
			//    fbDialog.SelectedPath = Program.Config["GameRoot"];
			//else
			//    fbDialog.SelectedPath = txt.Text.Replace("%GameRoot%", Program.Config["GameRoot"]);
			fbDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			fbDialog.Description = toolTip1.GetToolTip(txt);
			if (fbDialog.ShowDialog(this) == DialogResult.OK)
			{
				string fn = fbDialog.SelectedPath;
				//if (txt.Equals(txtPath))
				//{
				//    DirectoryInfo di = new DirectoryInfo(fn);
				//    if (fn.StartsWith(Program.Config["GameRoot"], StringComparison.OrdinalIgnoreCase))
				//    {
				//        Regex reg = new Regex("^" + Program.Config["GameRoot"].Replace("\\", "\\\\"));
				//        fn = reg.Replace(fn, "%GameRoot%");
				//        if (fn.Equals("%GameRoot%"))
				//            fn = string.Empty;
				//    }
				//}
				//else if (txt.Equals(txtGameSave))
				//{
				//    string gPath = txtPath.Text;
				//    if (string.IsNullOrEmpty(gPath))
				//        gPath = Path.Combine(Program.Config["GameRoot"], txtGameID.Text);
				//    if (fn.StartsWith(gPath, StringComparison.OrdinalIgnoreCase))
				//        fn = "%Game%" + fn.Substring(Path.GetFullPath(gPath).Length);
				//    else if (fn.StartsWith(Program.Config["GameRoot"], StringComparison.OrdinalIgnoreCase))
				//        fn = "%GameRoot%" + fn.Substring(Path.GetFullPath(Program.Config["GameRoot"]).Length);
				//}
				txt.Text = fn;
			}
		}
		#endregion

		#region Color, Font TextBox Events
		private void txtMenuBackColor_TextChanged(object sender, EventArgs e)
		{
			labMenuSample.BackColor = ColorTranslator.FromHtml(txtMenuBackColor.Text);
			gbSample.BackColor = labMenuSample.BackColor;
			_ConfigChanged = true;
		}

		private void txtMenuTextColor_TextChanged(object sender, EventArgs e)
		{
			labMenuSample.ForeColor = ColorTranslator.FromHtml(txtMenuTextColor.Text);
			gbSample.ForeColor = labMenuSample.ForeColor;
			_ConfigChanged = true;
		}

		private void txtMenuFont_TextChanged(object sender, EventArgs e)
		{
			string[] f = txtMenuFont.Text.Split(',');
			if (f.Length != 2)
			{
				MessageBox.Show("輸入格式錯誤!! 將回復成預設值「標楷體,14」。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMenuFont.Text = "標楷體,14";
				return;
			}
			labMenuSample.Font = new Font(f[0], Convert.ToSingle(f[1]));
			gbSample.Font = labMenuSample.Font;
			_ConfigChanged = true;
		}

		private void txtFormBackColor_TextChanged(object sender, EventArgs e)
		{
			panSample.BackColor = ColorTranslator.FromHtml(txtFormBackColor.Text);
			_ConfigChanged = true;
		}
		#endregion

		#region NumericUpDown Events
		private void udFormWidth_ValueChanged(object sender, EventArgs e)
		{
			_ConfigChanged = true;
		}
		private void udMenuHeight_ValueChanged(object sender, EventArgs e)
		{
			labMenuSample.Height = (int)udMenuHeight.Value;
			NumericUpDown_Enter(udMenuHeight, null);
			_ConfigChanged = true;
		}
		private void udGameItemHeight_ValueChanged(object sender, EventArgs e)
		{
			gbSample.Height = (int)udGameItemHeight.Value;
			_ConfigChanged = true;
		}
		private void udSaveButtonWidth_ValueChanged(object sender, EventArgs e)
		{
			gbSample.SaveButtonWidth = (int)udSaveButtonWidth.Value;
			_ConfigChanged = true;
		}
		private void udIconWidth_ValueChanged(object sender, EventArgs e)
		{
			gbSample.IconWidth = (int)udIconWidth.Value;
			_ConfigChanged = true;
		}
		private void NumericUpDown_Enter(object sender, EventArgs e)
		{
			NumericUpDown nud = (NumericUpDown)sender;
			nud.Select(0, nud.Value.ToString().Length);
		}
		#endregion

		#region Private Method : void Config_Changed(object sender, EventArgs e)
		private void Config_Changed(object sender, EventArgs e)
		{
			_ConfigChanged = true;
		}
		#endregion
		#endregion

		#region Game Menu Tab

		#region Menu List

		#region Private Method : void LoadGameMenu()
		private void LoadGameMenu()
		{
			lstMenu.Items.Clear();
			lstMenu.DisplayMember = "Name";
			lstMenu.Items.AddRange(Program.Menus.ToArray());
			cbMenu.Items.Clear();
			cbMenu.DisplayMember = "Name";
			cbMenu.Items.AddRange(Program.Menus.ToArray());
			lstGame.Items.Clear();
		}
		#endregion

		#region Private Method : void lstMenu_SelectedValueChanged(object sender, EventArgs e)
		private void lstMenu_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!lstMenu.Enabled || lstMenu.AllowDrop) return;
			gbMenuInfo.Enabled = gbGameList.Enabled = btnDelMenu.Enabled = btnAddGame.Enabled = btnMenuDn.Enabled = btnMenuUp.Enabled = (lstMenu.SelectedItem != null);
			if (lstMenu.SelectedItem == null) return;
			MenuInfo mi = (MenuInfo)lstMenu.SelectedItem;
			if (_GameInfoChanged)
			{
				DialogResult res = MessageBox.Show("遊戲資料已變更，是否儲存!?\n\n「是」儲存遊戲資料\n「否」不儲存\n「取消」關閉本詢問窗且不做任何事", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (res == DialogResult.Yes)
					btnSaveGame_Click(btnSaveGame, null);
				else if (res == DialogResult.No)
					_GameInfoChanged = false;
				else
				{
					lstMenu.Enabled = false;
					lstMenu.SelectedItem = _SelectedMenuInfo;
					lstMenu.Enabled = true;
					return;
				}
			}
			_SelectedMenuInfo = mi;
			_IsAppendMode = false;
			_SelectedGameInfo = null;
			lstGame.SelectedItem = null;
			lstGame.Items.Clear();
			CleanGameInfo();
			btnDelMenu.Enabled = true;
			lstGame.DisplayMember = "Name";
			List<GameInfo> lgi = Program.Games.FindAll(gi => gi.MenuID.Equals(mi.ID, StringComparison.OrdinalIgnoreCase));
			if (lgi != null && lgi.Count != 0)
			{
				lgi.Sort((g1, g2) => g1.Sort.CompareTo(g2.Sort));
				lstGame.Items.AddRange(lgi.ToArray());
			}
			txtMenuID.Text = mi.ID;
			txtMenuID.ReadOnly = true;
			txtMenuName.Text = mi.Name;
			gbGameInfo.Enabled = false;
			_GameInfoChanged = false;
		}
		#endregion

		#region Private Method : void btnSaveMenu_Click(object sender, EventArgs e)
		private void btnSaveMenu_Click(object sender, EventArgs e)
		{
			if (txtMenuID.Text.Trim().Length == 0)
			{
				MessageBox.Show("選單代碼為必要欄位資料，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				txtMenuID.Focus();
				return;
			}
			else if (txtMenuName.Text.Trim().Length == 0)
			{
				MessageBox.Show("選單名稱為必要欄位資料，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				txtMenuName.Focus();
				return;
			}
			if (_IsAppendMode)
			{
				if (Program.Menus.Find(txtMenuID.Text) != null)
				{
					MessageBox.Show("選單代碼已存在，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					txtMenuID.Focus();
				}
				else
				{

					MenuInfo mi = new MenuInfo(txtMenuID.Text, txtMenuName.Text, Program.Menus.Count);
					if (DataTier.AppendMenu(mi))
					{
						Program.Menus.Add(mi);
						cbMenu.Items.Add(mi);
						lstMenu.Items.Add(mi);
						lstMenu.SelectedItem = mi;
						_GameInfoChanged = false;
					}
					else
						MessageBox.Show("無法新增選單資料，請聯絡程式開發人員!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				Program.Menus[txtMenuID.Text].Name = txtMenuName.Text;
				if (DataTier.UpdateMenu(Program.Menus[txtMenuID.Text]))
				{
					int idx = lstMenu.SelectedIndex;
					cbMenu.Items.Remove(lstMenu.SelectedItem);
					cbMenu.Items.Insert(idx, Program.Menus[txtMenuID.Text]);
					lstMenu.Enabled = false;
					lstMenu.Items.Remove(lstMenu.SelectedItem);
					lstMenu.Items.Insert(idx, Program.Menus[txtMenuID.Text]);
					lstMenu.SelectedIndex = idx;
					lstMenu.Enabled = true;
				}
				else
					MessageBox.Show("無法更新選單資料，請聯絡程式開發人員!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

		#region Private Method : void btnAddMenu_Click(object sender, EventArgs e)
		private void btnAddMenu_Click(object sender, EventArgs e)
		{
			txtMenuID.Text = string.Empty;
			txtMenuID.ReadOnly = false;
			txtMenuName.Text = string.Empty;
			gbGameInfo.Enabled = false;
			gbMenuInfo.Enabled = true;
			lstMenu.SelectedItem = null;
			_IsAppendMode = true;
			txtMenuID.Focus();
		}
		#endregion

		#region Private Method : void btnDelMenu_Click(object sender, EventArgs e)
		private void btnDelMenu_Click(object sender, EventArgs e)
		{
			if (_SelectedMenuInfo == null) return;
			List<GameInfo> lgi = Program.Games.FindAll(gi => gi.MenuID.Equals(_SelectedMenuInfo.ID, StringComparison.OrdinalIgnoreCase));
			if (lgi != null && lgi.Count != 0)
			{
				MessageBox.Show("歸類於本選單的遊戲必須先清空，否則無法刪除!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			else if (MessageBox.Show("確定刪除本選單!?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				btnDelMenu.Enabled = false;
				DataTier.DeleteMenu(_SelectedMenuInfo);
				Program.Menus.Remove(_SelectedMenuInfo);
				cbMenu.Items.Remove(_SelectedMenuInfo);
				lstMenu.Items.Remove(_SelectedMenuInfo);
				txtMenuID.Clear();
				txtMenuName.Clear();
				_GameInfoChanged = false;
				_IsAppendMode = false;
			}
		}
		#endregion

		#region Private Method : void TextBox_Enter(object sender, EventArgs e)
		private void TextBox_Enter(object sender, EventArgs e)
		{
			TextBox txt = (TextBox)sender;
			txt.SelectAll();
		}
		#endregion

		#region Private Method : void MenuSort_Click(object sender, EventArgs e)
		private void MenuSort_Click(object sender, EventArgs e)
		{
			if (_SelectedMenuInfo == null) return;
			Button btn = (Button)sender;
			int idx = lstMenu.SelectedIndex;
			int newIndex = -1;
			if (btn.Equals(btnMenuUp))
				newIndex = Program.Menus.SortUp(_SelectedMenuInfo);
			else
				newIndex = Program.Menus.SortDn(_SelectedMenuInfo);
			if (idx != newIndex)
			{
				lstMenu.Enabled = false;
				lstMenu.Items.RemoveAt(idx);
				lstMenu.Items.Insert(newIndex, _SelectedMenuInfo);
				DataTier.UpdateMenu(_SelectedMenuInfo);
				DataTier.UpdateMenu(Program.Menus[idx]);
				lstMenu.SelectedItem = _SelectedMenuInfo;
				lstMenu.Enabled = true;
				Application.DoEvents();
			}
		}
		#endregion

		#endregion

		#region Game List

		#region Private Method : void lstGame_SelectedValueChanged(object sender, EventArgs e)
		private void lstGame_SelectedValueChanged(object sender, EventArgs e)
		{
			if (!lstGame.Enabled) return;
			gbGameInfo.Enabled = btnDelGame.Enabled = cbMenu.Enabled = btnGameDn.Enabled = btnGameUp.Enabled = (lstGame.SelectedItem != null);
			if (lstGame.SelectedItem == null) return;
			GameInfo gi = (GameInfo)lstGame.SelectedItem;
			if (_GameInfoChanged)
			{
				DialogResult res = MessageBox.Show("遊戲資料已變更，是否儲存!?\n\n「是」儲存遊戲資料\n「否」不儲存\n「取消」關閉本詢問窗且不做任何事", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (res == DialogResult.Yes)
					btnSaveGame_Click(btnSaveGame, null);
				else if (res == DialogResult.No)
				{
					_GameInfoChanged = false;
					lstGame.Enabled = false;
					lstGame.SelectedItem = gi;
					lstGame.Enabled = true;
				}
				else
				{
					lstGame.Enabled = false;
					lstGame.SelectedItem = _SelectedGameInfo;
					lstGame.Enabled = true;
					return;
				}
			}
			gbMenuInfo.Enabled = false;
			_SelectedGameInfo = gi;
			txtGameID.Text = gi.ID;
			txtGameID.ReadOnly = true;
			txtGameName.Text = gi.Name;
			txtPath.Text = gi.Path;
			txtExec.Text = gi.Exec;
			cbHasSave.Checked = gi.HasSave;
			cbIncSub.Checked = gi.IncSub;
			txtGameSave.Text = gi.SavePath;
			txtSaveFiles.Text = gi.SaveFiles;
			txtIcon.Text = gi.Icon;
			SettingIconViewer(gi.Path, gi.Icon);
			cbMenu.SelectedItem = lstMenu.SelectedItem;
			_IsAppendMode = false;
			_GameInfoChanged = false;
		}
		#endregion

		#region Private Method : void btnSaveGame_Click(object sender, EventArgs e)
		private void btnSaveGame_Click(object sender, EventArgs e)
		{
			if (txtGameID.Text.Trim().Length == 0)
			{
				MessageBox.Show("遊戲代碼為必要欄位資料，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				txtGameID.Focus();
				return;
			}
			else if (txtGameName.Text.Trim().Length == 0)
			{
				MessageBox.Show("遊戲名稱為必要欄位資料，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				txtGameName.Focus();
				return;
			}

			MenuInfo mi = (MenuInfo)cbMenu.SelectedItem;
			MenuInfo cmi = (MenuInfo)lstMenu.SelectedItem;
			if (_IsAppendMode)
			{
				if (Program.Games.Find(txtGameID.Text) != null)
				{
					MessageBox.Show("遊戲代碼已存在，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					txtGameID.Focus();
				}
				else
				{
					List<GameInfo> lgi = Program.Games.FindAll(item => item.MenuID.Equals(mi.ID, StringComparison.OrdinalIgnoreCase));
					GameInfo gi = new GameInfo()
					{
						ID = txtGameID.Text,
						Name = txtGameName.Text,
						MenuID = mi.ID,
						Path = txtPath.Text,
						Exec = txtExec.Text,
						Icon = txtIcon.Text,
						HasSave = cbHasSave.Checked,
						SavePath = string.Empty,
						SaveFiles = string.Empty,
						IncSub = false,
						Sort = lgi.Count
					};
					if (gi.HasSave)
					{
						gi.SavePath = txtGameSave.Text;
						gi.SaveFiles = txtSaveFiles.Text;
						gi.IncSub = cbIncSub.Checked;
					}
					if (DataTier.AppendGame(gi))
					{
						_IsAppendMode = false;
						_GameInfoChanged = false;
						if (mi.ID.Equals(cmi.ID))
						{
							Program.Games.Add(gi);
							lstGame.Items.Add(gi);
							lstGame.SelectedItem = gi;
						}
						else
						{
							lstMenu.SelectedItem = mi;
							lstGame.SelectedItem = gi;
						}
						MessageBox.Show(string.Format("遊戲 \"{0}\" 儲存完畢!!", gi.Name), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
						MessageBox.Show("無法新增遊戲資料，請聯絡程式開發人員!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{

				bool changedName = !_SelectedGameInfo.Name.Equals(txtGameName.Text, StringComparison.OrdinalIgnoreCase);
				bool changedMenu = !_SelectedGameInfo.MenuID.Equals(mi.ID, StringComparison.OrdinalIgnoreCase);
				_SelectedGameInfo.Name = txtGameName.Text;
				_SelectedGameInfo.MenuID = mi.ID;
				_SelectedGameInfo.Path = txtPath.Text;
				_SelectedGameInfo.Exec = txtExec.Text;
				_SelectedGameInfo.Icon = txtIcon.Text;
				_SelectedGameInfo.HasSave = cbHasSave.Checked;
				_SelectedGameInfo.SavePath = string.Empty;
				_SelectedGameInfo.SaveFiles = string.Empty;
				_SelectedGameInfo.IncSub = false;
				if (_SelectedGameInfo.HasSave)
				{
					_SelectedGameInfo.SavePath = txtGameSave.Text;
					_SelectedGameInfo.SaveFiles = txtSaveFiles.Text;
					_SelectedGameInfo.IncSub = cbIncSub.Checked;
				}
				if (changedName)
				{
					lstGame.Enabled = false;
					int idx = lstGame.SelectedIndex;
					lstGame.Items.RemoveAt(idx);
					lstGame.Items.Insert(idx, _SelectedGameInfo);
					lstGame.SelectedIndex = idx;
					lstGame.Enabled = true;
				}
				else if (changedMenu)
				{
					_SelectedGameInfo.Sort = Program.Games.FindAll(gi => gi.MenuID.Equals(mi.ID, StringComparison.OrdinalIgnoreCase)).Count;
					lstGame.Items.Remove(lstGame.SelectedItem);
					CleanGameInfo();
				}
				if (DataTier.UpdateGame(_SelectedGameInfo))
				{
					_GameInfoChanged = false;
					MessageBox.Show(string.Format("遊戲 \"{0}\" 更新完畢!!", _SelectedGameInfo.Name), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
					MessageBox.Show("無法更新遊戲資料，請聯絡程式開發人員!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

		#region Private Method : void btnAddGame_Click(object sender, EventArgs e)
		private void btnAddGame_Click(object sender, EventArgs e)
		{
			gbGameInfo.Enabled = true;
			gbMenuInfo.Enabled = false;
			_IsAppendMode = true;
			txtGameID.Clear();
			txtGameID.ReadOnly = false;
			txtGameName.Clear();
			txtPath.Clear();
			txtExec.Clear();
			picIcon.Image = null;
			cbHasSave.Checked = false;
			txtGameSave.Clear();
			txtSaveFiles.Clear();
			cbIncSub.Checked = false;
			cbMenu.Enabled = true;
			cbMenu.SelectedItem = lstMenu.SelectedItem;
			lstGame.Enabled = false;
			lstGame.SelectedItem = null;
			lstGame.Enabled = true;
			_GameInfoChanged = true;
			txtGameID.Focus();
		}
		#endregion

		#region Private Method : void btnDelGame_Click(object sender, EventArgs e)
		private void btnDelGame_Click(object sender, EventArgs e)
		{
			if (_SelectedGameInfo == null) return;
			if (MessageBox.Show(string.Format("確定刪除此遊戲項目「{0}」!?", _SelectedGameInfo.Name), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				btnDelGame.Enabled = false;
				DataTier.DeleteGame(_SelectedGameInfo);
				Program.Games.Remove(_SelectedGameInfo);
				lstGame.Items.Remove(_SelectedGameInfo);
				CleanGameInfo();
				gbGameInfo.Enabled = false;
				_GameInfoChanged = false;
				_IsAppendMode = false;
				gbGameInfo.Enabled = false;
			}
		}
		#endregion

		#region Private Method : void GameSort_Click(object sender, EventArgs e)
		private void GameSort_Click(object sender, EventArgs e)
		{
			if (_SelectedGameInfo == null) return;
			MenuInfo mi = (MenuInfo)lstMenu.SelectedItem;
			Button btn = (Button)sender;
			int idx = lstGame.SelectedIndex;
			int newIndex = -1;
			List<GameInfo> lgi = Program.Games.FindAll(item => item.MenuID.Equals(mi.ID, StringComparison.OrdinalIgnoreCase));
			lgi.Sort((g1, g2) => g1.Sort.CompareTo(g2.Sort));
			if (btn.Equals(btnGameUp))
			{
				if (idx != 0)
				{
					_SelectedGameInfo.Sort--;
					if (_SelectedGameInfo.Sort <= lgi[idx - 1].Sort)
					{
						lgi[idx - 1].Sort++;
						lgi.RemoveAt(idx);
						newIndex = idx - 1;
						lgi.Insert(newIndex, _SelectedGameInfo);
					}
				}
				else
					return;
			}
			else
			{
				if (idx != lgi.Count - 1)
				{
					_SelectedGameInfo.Sort++;
					if (_SelectedGameInfo.Sort <= lgi[idx + 1].Sort)
					{
						lgi[idx + 1].Sort--;
						lgi.RemoveAt(idx);
						newIndex = idx + 1;
						lgi.Insert(newIndex, _SelectedGameInfo);
					}
				}
				else
					return;
			}
			lstGame.Enabled = false;
			if (newIndex != -1)
			{
				lstGame.Items.RemoveAt(idx);
				lstGame.Items.Insert(newIndex, _SelectedGameInfo);
			}
			DataTier.UpdateGame(_SelectedGameInfo);
			DataTier.UpdateGame(lgi[idx]);
			lstGame.SelectedItem = _SelectedGameInfo;
			lstGame.Enabled = true;
			Application.DoEvents();
		}
		#endregion

		#region Private Method : void cbHasSave_CheckedChanged(object sender, EventArgs e)
		private void cbHasSave_CheckedChanged(object sender, EventArgs e)
		{
			gbSaves.Enabled = cbHasSave.Checked;
			_GameInfoChanged = true;
		}
		#endregion

		#region Private Method : void btnIcon_Click(object sender, EventArgs e)
		private void btnIcon_Click(object sender, EventArgs e)
		{
			string[] sp = txtIcon.Text.Split(',');
			ofDialog.Filter = "支援的檔案(*.ico;*.exe;*.dll;*.png;*.bmp;*.jpg;*.gif)|*.ico;*.exe;*.dll;*.png;*.bmp;*.jpg;*.gif|所有檔案(*.*)|*.*";
			ofDialog.FileName = sp[0];
			ofDialog.Title = "請選擇遊戲圖示";
			if (ofDialog.ShowDialog(this) == DialogResult.OK)
			{
				txtIcon.Text = ofDialog.FileName;
				string[] mime = GameInfo.MimeType(txtIcon.Text).Split('/');
				if (mime[0].Equals("application", StringComparison.OrdinalIgnoreCase) || string.Join("/", mime).Equals("image/x-icon", StringComparison.OrdinalIgnoreCase))
				{
					// Execution File's Icon, Get it from Execution File
					Icon[] icons = IconHandler.IconsFromFile(txtIcon.Text, IconSize.Large);
					if (icons.Length > 1)
					{
						using (IconViwer iv = new IconViwer())
						{
							iv.FileName = ofDialog.FileName;
							if (ofDialog.FileName.Equals(sp[0], StringComparison.OrdinalIgnoreCase) && sp.Length >= 2)
								iv.SelectedIndex = Convert.ToInt32(sp[1]);
							if (iv.ShowDialog(this) == DialogResult.OK)
							{
								if (iv.SelectedIndex != 0)
									txtIcon.Text += "," + iv.SelectedIndex.ToString();
							}
						}
					}
					else if (icons.Length == 0)
					{
						MessageBox.Show(string.Format("檔案 {0} 沒有內含圖示或是無法讀取!!", Path.GetFileName(txtIcon.Text)), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
		}
		#endregion

		#region Private Method : void txtIcon_TextChanged(object sender, EventArgs e)
		private void txtIcon_TextChanged(object sender, EventArgs e)
		{
			SettingIconViewer(txtPath.Text, txtIcon.Text);
		}
		#endregion

		#region Private Method : void SettingIconViewer(string gamePath, string choosedFile)
		private void SettingIconViewer(string gamePath, string choosedFile)
		{
			string iconPath = string.Empty;
			string origPath = choosedFile;
			int iconIndex = 0;
			if (origPath.IndexOf(',') != -1)
			{
				string[] sp = origPath.Split(',');
				iconPath = sp[0];
				int.TryParse(sp[1], out iconIndex);
			}
			else
				iconPath = origPath;
			//iconPath = iconPath.Replace("%Game%", gamePath);
			if (!Path.IsPathRooted(iconPath))
				iconPath = Path.Combine(Program.Config["IconPath"], iconPath);
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
										int iconWidth = Convert.ToInt32(Program.Config["IconAreaWidth"]);
										int gameHeight = Convert.ToInt32(Program.Config["GameItemHeight"]);
										if (iconWidth > gameHeight)
											s = new Size(gameHeight, gameHeight);
										else
											s = new Size(iconWidth, iconWidth);
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
				picIcon.Image = img;
			else
				picIcon.Image = null;
		}
		#endregion

		#region Private Method : void CleanGameInfo()
		private void CleanGameInfo()
		{
			txtGameID.Clear();
			cbMenu.Enabled = false;
			cbMenu.SelectedItem = null;
			txtGameName.Clear();
			txtPath.Clear();
			txtExec.Clear();
			cbHasSave.Checked = false;
			picIcon.Image = null;
			txtIcon.Clear();
			txtGameSave.Clear();
			txtSaveFiles.Clear();
			cbIncSub.Checked = false;
			_IsAppendMode = false;
		}
		#endregion

		#region Private Method : void btnExec_Click(object sender, EventArgs e)
		private void btnExec_Click(object sender, EventArgs e)
		{
			string fn = txtExec.Text;
			string gPath = txtPath.Text;
			if (string.IsNullOrEmpty(gPath))
				gPath = Path.Combine(Program.Config["GameRoot"], txtGameID.Text);
			//else
			//    gPath = gPath.Replace("%GameRoot%", Program.Config["GameRoot"]);
			//fn = fn.Replace("%Game%", gPath);
			if (!Path.IsPathRooted(fn))
				fn = Path.Combine(gPath, fn);

			ofDialog.Filter = "可執行的檔案(*.exe;*.lnk;*.bat)|*.exe;*.lnk;*.bat|Flash 檔案(*.swf)|*.swf|所有檔案(*.*)|*.*";
			ofDialog.FileName = fn;
			ofDialog.InitialDirectory = Path.GetDirectoryName(fn);
			ofDialog.Title = "請選擇遊戲執行檔";
			if (ofDialog.ShowDialog(this) == DialogResult.OK)
			{
				fn = ofDialog.FileName;
				if (Path.GetDirectoryName(fn).Equals(Path.Combine(Program.Config["GameRoot"], txtGameID.Text), StringComparison.OrdinalIgnoreCase))
					fn = Path.GetFileName(fn);
				//else if (fn.StartsWith(txtPath.Text, StringComparison.OrdinalIgnoreCase))
				//    fn = "%Game%" + fn.Substring(Path.GetFullPath(txtPath.Text).Length);
				txtExec.Text = fn;
			}
		}
		#endregion

		#region Private Method : void GameInfo_Changed(object sender, EventArgs e)
		private void GameInfo_Changed(object sender, EventArgs e)
		{
			_GameInfoChanged = true;
		}
		#endregion

		#region Private Method : void btnSaveFiles_Click(object sender, EventArgs e)
		private void btnSaveFiles_Click(object sender, EventArgs e)
		{
			string gPath = txtPath.Text;
			if (string.IsNullOrEmpty(gPath))
				gPath = Path.Combine(Program.Config["GameRoot"], txtGameID.Text);
			string savesPath = txtGameSave.Text;//.Replace("%GameRoot%", Program.Config["GameRoot"]).Replace("%Game%", gPath);
			if (!Directory.Exists(savesPath))
			{
				MessageBox.Show(string.Format("找不到目錄 \"{0}\"，請先確定目錄位置!!", savesPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			using (FileView fv = new FileView())
			{
				fv.SearchFile(savesPath, txtSaveFiles.Text, cbIncSub.Checked);
				if (fv.ShowDialog(this) == DialogResult.OK)
				{
					//txtGameSave.Text = fv.Folder.Replace(gPath, "%Game%");
					txtGameSave.Text = fv.Folder;
					txtSaveFiles.Text = fv.ExtName;
					cbIncSub.Checked = fv.IncSubDir;
				}
			}
		}
		#endregion

		#endregion

		#endregion

		#region User List Tab

		#region Private Method : void LoadUsers()
		private void LoadUsers()
		{
			DataTable dt = DataTier.LoadUsers();
			if (dt == null)
			{
				MessageBox.Show(string.Format("無法讀取使用者帳號資料!!\n\n請先確定 {0} 存放在「備份存放」的路徑 \"{1}\" 中",
					Program.USER_DB, Program.Config["SavesPath"]), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			else
			{
				if (_UserSource == null)
					_UserSource = new BindingSource();
				_UserSource.DataSource = dt;
				gvUsers.DataSource = dt;
				gvUsers.Columns["RowID"].Visible = false;
				gvUsers.Columns["UserID"].HeaderText = "帳號";
				gvUsers.Columns["PWD"].HeaderText = "密碼";
				gvUsers.Columns["LastLogin"].HeaderText = "上次登入日期";
				gvUsers.Columns["Created"].HeaderText = "帳號建立日期";
				gvUsers.Columns["LastLogin"].ReadOnly = true;
				gvUsers.Columns["LastLogin"].Width = 160;
				gvUsers.Columns["Created"].ReadOnly = true;
				gvUsers.Columns["Created"].Width = 160;
				gvUsers.Refresh();
				_UserDataChanged = false;
			}
		}
		#endregion

		#region Private Method : void gvUsers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		private void gvUsers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			DataGridViewRow dgvr = e.Row;
			if (MessageBox.Show(string.Format("是否刪除此帳號 \"{0}\" ?", dgvr.Cells["UserID"].Value.ToString()), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				e.Cancel = true;
			else
				_UserDataChanged = true;
		}
		#endregion

		#region Private Method : void gvUsers_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
		private void gvUsers_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
		{
			DataGridViewRow dgvr = e.Row;
			dgvr.Cells["RowID"].Value = DateTime.Now.Ticks;
			dgvr.Cells["Created"].Value = DateTime.Now;
		}
		#endregion

		#region Private Method : void gvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		private void gvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			string cn = gvUsers.Columns[e.ColumnIndex].Name;
			if (cn.Equals("Created") || cn.Equals("RowID", StringComparison.OrdinalIgnoreCase)) return;
			_UserDataChanged = true;
		}
		#endregion

		#region Private Method : void gvUsers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		private void gvUsers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			string nv = e.FormattedValue.ToString();
			if (gvUsers.Columns[e.ColumnIndex].Name.Equals("UserID"))
			{
				if (gvUsers.Rows[e.RowIndex].IsNewRow && string.IsNullOrEmpty(gvUsers.Rows[e.RowIndex].Cells["PWD"].Value.ToString()))
					return;
				else if (string.IsNullOrEmpty(nv))
				{
					MessageBox.Show("帳號為必要輸入，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					e.Cancel = true;
				}
				else if (nv.Length < 3 || nv.Length > 12)
				{
					MessageBox.Show("帳號長度必須大於(包含) 3 個字元且小於 12(包含) 個字元，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					e.Cancel = true;
				}
				else if (Encoding.Default.GetByteCount(nv) != nv.Length || Regex.Replace(nv, @"[^\w\.@-]", "") != nv)
				{
					MessageBox.Show("帳號必須為英數字且不得包含特殊符號，請重新輸入!!\n", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					e.Cancel = true;
				}
				else
				{
					long rowId = -1;
					rowId = Convert.ToInt64(gvUsers.Rows[e.RowIndex].Cells["RowID"].Value);
					DataTable dt = (DataTable)gvUsers.DataSource;
					DataRow[] drs = dt.Select("UserID='" + nv + "'");
					if (drs.Length != 0 && Convert.ToInt64(drs[0]["RowID"]) != rowId)
					{
						MessageBox.Show(string.Format("帳號 \"{0}\" 已存在，請重新輸入!!", nv), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
						e.Cancel = true;
					}
				}
			}
			else if (gvUsers.Columns[e.ColumnIndex].Name.Equals("PWD"))
			{
				if (gvUsers.Rows[e.RowIndex].IsNewRow && string.IsNullOrEmpty(gvUsers.Rows[e.RowIndex].Cells["UserID"].Value.ToString()))
					return;
				else if (string.IsNullOrEmpty(nv))
				{
					MessageBox.Show("密碼為必要輸入，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					e.Cancel = true;
				}
				else if (nv.Length < 3 || nv.Length > 12)
				{
					MessageBox.Show("密碼長度必須大於(包含) 3 個字元且小於 12(包含) 個字元，請重新輸入!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					e.Cancel = true;
				}
				else if (Encoding.Default.GetByteCount(nv) != nv.Length || nv.IndexOf(' ') != -1)
				{
					MessageBox.Show("密碼必須為英數字且不得包含空白字元，請重新輸入!!\n", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					e.Cancel = true;
				}
			}
		}
		#endregion

		#region Private Method : void tsbSave_Click(object sender, EventArgs e)
		private void tsbSave_Click(object sender, EventArgs e)
		{
			if (gvUsers.IsCurrentRowDirty)
			{
				MessageBox.Show("請先編輯完畢後再儲存!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (gvUsers.CurrentRow.IsNewRow)
				gvUsers.CancelEdit();
			DataTable dt = (DataTable)gvUsers.DataSource;
			DataTable nt = dt.GetChanges();
			if (nt != null)
			{
				int res = DataTier.UpdateUserList(nt);
				if (res != 0)
				{
					dt.AcceptChanges();
					MessageBox.Show(string.Format("已異動 {0} 筆使用者帳號!!", res), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
					MessageBox.Show("無任何使用者帳號需要更新或新增!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
				MessageBox.Show("無任何使用者帳號需要更新或新增!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			_UserDataChanged = false;
		}
		#endregion

		#region Private Method : void tsbSearch_Click(object sender, EventArgs e)
		private void tsbSearch_Click(object sender, EventArgs e)
		{
			int idx = _UserSource.Find("UserID", tstKeyword.Text);
			if (idx == -1)
				MessageBox.Show(string.Format("查無帳號 \"{0}\" !!", tstKeyword.Text), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				gvUsers.ClearSelection();
				gvUsers.Rows[idx].Selected = true;
			}
		}
		#endregion

		#region Private Method : void tstKeyword_KeyPress(object sender, KeyPressEventArgs e)
		private void tstKeyword_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals('\r'))
			{
				tsbSearch_Click(null, null);
				e.Handled = true;
			}
		}
		#endregion

		#region Private Method : void tsbReload_Click(object sender, EventArgs e)
		private void tsbReload_Click(object sender, EventArgs e)
		{
			LoadUsers();
		}
		#endregion

		#endregion

	}
}
