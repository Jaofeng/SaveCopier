using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SaveCopier
{
	public partial class RegForm : Form
	{
		public string UserID { get; private set; }
		public string Password { get; private set; }

		#region Public Method : RegForm()
		public RegForm()
		{
			InitializeComponent();
		}
		#endregion

		#region Private Method : void RegForm_Load(object sender, EventArgs e)
		private void RegForm_Load(object sender, EventArgs e)
		{
			txtUserID.Focus();
		}
		#endregion

		#region Private Method : void btnOK_Click(object sender, EventArgs e)
		private void btnOK_Click(object sender, EventArgs e)
		{
			string msg = string.Empty;
			string id = txtUserID.Text;
			string p1 = txtPwd1.Text;
			string p2 = txtPwd2.Text;
			if (id.Length < 3 || id.Length > 12)
			{
				MessageBox.Show("請輸入註冊帳號，且最少3個字，最多12個字!!", Program.Config["Caption"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.DialogResult = DialogResult.None;
				txtUserID.Focus();
			}
			else if (Encoding.Default.GetByteCount(id) != id.Length || Regex.Replace(id, @"[^\w\.@-]", "") != id)
			{
				MessageBox.Show("帳號必須為英數字且不得包含特殊符號，請重新輸入!!\n", Program.Config["Caption"], MessageBoxButtons.OK, MessageBoxIcon.Stop);
				this.DialogResult = DialogResult.None;
				txtUserID.Focus();
			}
			else if (p1.Length < 3 || p1.Length > 12)
			{
				MessageBox.Show("請輸入欲使用的密碼，且最少3個字，最多12個字!!", Program.Config["Caption"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.DialogResult = DialogResult.None;
				txtPwd1.Focus();
			}
			else if (Encoding.Default.GetByteCount(p1) != p1.Length || p1.IndexOf(' ') != -1)
			{
				MessageBox.Show("密碼必須為英數字且不得包含空白字元，請重新輸入!!\n", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				this.DialogResult = DialogResult.None;
				txtPwd1.Focus();
			}
			else if (!p1.Equals(p2))
			{
				MessageBox.Show("請再次輸入相同的帳號密碼，以確認您已記妥!!", Program.Config["Caption"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.DialogResult = DialogResult.None;
				txtPwd2.Focus();
			}
			else
			{
				if (DataTier.SaveNewUser(txtUserID.Text, txtPwd1.Text))
				{
					this.DialogResult = DialogResult.OK;
					this.UserID = txtUserID.Text;
					this.Password = txtPwd1.Text;
					this.Close();
				}
				else
				{
					MessageBox.Show("帳號已存在，請重新設定!!!", Program.Config["Caption"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtUserID.Focus();
				}
			}
		}
		#endregion

		#region Private Method : void RegForm_KeyDown(object sender, KeyEventArgs e)
		private void RegForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}
		#endregion

		#region Private Method : void TextBox_Enter(object sender, EventArgs e)
		private void TextBox_Enter(object sender, EventArgs e)
		{
			TextBox txt = (TextBox)sender;
			txt.SelectAll();
		}
		#endregion

	}
}
