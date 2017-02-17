using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SaveCopier
{
	public partial class FileView : Form
	{
		public FileView()
		{
			InitializeComponent();
		}

		public string Folder { get { return txtFolder.Text; } }
		public string ExtName { get { return txtExt.Text; } }
		public bool IncSubDir { get { return cbIncSub.Checked; } }

		public void SearchFile(string folder, string ext, bool incSub)
		{
			txtFolder.Text = folder;
			txtExt.Text = ext;
			cbIncSub.Checked = incSub;
			ShowFiles(folder, ext, incSub);
		}

		private void ShowFiles(string folder, string ext, bool incSub)
		{
			string[] exts = ext.Split(';');
			List<string> files = new List<string>();
			if (incSub)
			{
				foreach (string s in exts)
					files.AddRange(Directory.GetFiles(folder, s, SearchOption.AllDirectories));
			}
			else
			{
				foreach (string s in exts)
					files.AddRange(Directory.GetFiles(folder, s));
			}
			lvFile.Items.Clear();
			lvFile.View = View.Details;
			lvFile.FullRowSelect = true;
			FileInfo fi = null;
			ListViewItem lvi = null;
			foreach (string s in files)
			{
				lvi = new ListViewItem(Path.GetFileName(s));
				lvi.SubItems.Add(Path.GetDirectoryName(s).Replace(folder.TrimEnd('\\') + "\\", ""));
				fi = new FileInfo(s);
				lvi.SubItems.Add(fi.LastWriteTime.ToString());
				lvFile.Items.Add(lvi);
			}
			lvFile.Refresh();
		}

		private void btnRefrash_Click(object sender, EventArgs e)
		{
			ShowFiles(txtFolder.Text, txtExt.Text, cbIncSub.Checked);
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
