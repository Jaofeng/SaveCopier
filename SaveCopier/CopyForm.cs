using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SaveCopier
{
	public partial class CopyForm : Form
	{
		bool _IsDownload = false;
		string _UserID = string.Empty;
		internal GameInfo GameInfo = null;
		bool _IsStop = false;

		#region Public Method : CopyForm(string userId, bool isDown)
		public CopyForm(string userId, bool isDown)
		{
			InitializeComponent();
			_IsDownload = isDown;
			_UserID = userId;
		}
		#endregion

		#region Private Method : void CopyForm_FormClosing(object sender, FormClosingEventArgs e)
		private void CopyForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_IsStop = true;
		}
		#endregion

		#region Private Method : void CopyForm_Shown(object sender, EventArgs e)
		private void CopyForm_Shown(object sender, EventArgs e)
		{
			if (_IsDownload)
				DownloadSaves(this.GameInfo);
			else
				UploadSaves(this.GameInfo);
		}
		#endregion

		#region Private Method : void DownloadSaves(GameInfo gi)
		private void DownloadSaves(GameInfo gi)
		{
			string gPath = gi.Path;
			//if (string.IsNullOrEmpty(gPath) || gPath.Equals("%GameRoot%"))
			//    gPath = Path.Combine(Program.Config["GameRoot"], gi.ID);
			if (string.IsNullOrEmpty(gPath))
				gPath = Path.Combine(Program.Config["GameRoot"], gi.ID);
			string savesPath = gi.SavePath;
			//savesPath = savesPath.Replace("%GameRoot%", Program.Config["GameRoot"]).Replace("%Game%", gPath);
			if (!Directory.Exists(savesPath))
				Directory.CreateDirectory(savesPath);
			string userSavesPath = Path.Combine(Path.Combine(Program.Config["SavesPath"], gi.ID), _UserID);
			string[] exts = gi.SaveFiles.Split(';');
			List<string> files = new List<string>();
			if (gi.IncSub)
				files.AddRange(Directory.GetFiles(userSavesPath, gi.SaveFiles, SearchOption.AllDirectories));
			else
			{
				foreach (string s in exts)
					files.AddRange(Directory.GetFiles(userSavesPath, s));
			}
			string fName = string.Empty;
			string tPath = string.Empty;
			string tFile = string.Empty;
			pbCopy.Maximum = files.Count;
			pbCopy.Value = 0;
			foreach (string f in files)
			{
				if (_IsStop) break;
				fName = f.Substring(userSavesPath.Length + 1);
				tPath = Path.GetDirectoryName(Path.Combine(savesPath, fName));
				tFile = Path.Combine(savesPath, fName);
				if (!Directory.Exists(tPath))
					Directory.CreateDirectory(tPath);
				labCopy.Text = string.Format("正在複製 {0} 個檔案到 {1}", files.Count, tPath);
				File.Copy(Path.Combine(userSavesPath, fName), tFile, true);
				pbCopy.Value++;
				Application.DoEvents();
			}
			if (!_IsStop)
				this.DialogResult = DialogResult.OK;
			this.Close();
		}
		#endregion

		#region Private Method : void UploadSaves(GameInfo gi)
		private void UploadSaves(GameInfo gi)
		{
			string userSavesPath = Path.Combine(Path.Combine(Program.Config["SavesPath"], gi.ID), _UserID);
			string gPath = gi.Path;
			//if (string.IsNullOrEmpty(gPath) || gPath.Equals("%GameRoot%"))
			//    gPath = Path.Combine(Program.Config["GameRoot"], gi.ID);
			if (string.IsNullOrEmpty(gPath))
				gPath = Path.Combine(Program.Config["GameRoot"], gi.ID);
			string savesPath = gi.SavePath;
			if (string.IsNullOrEmpty(savesPath))
				savesPath = gPath;
			//savesPath = savesPath.Replace("%GameRoot%", Program.Config["GameRoot"]).Replace("%Game%", gPath);
			if (!Path.IsPathRooted(savesPath))
				savesPath = Path.Combine(gPath, savesPath);
			if (!Directory.Exists(savesPath))
			{
				this.DialogResult = DialogResult.Abort;
				this.Close();
				return;
			}
			if (!Directory.Exists(userSavesPath))
				Directory.CreateDirectory(userSavesPath);
			string[] exts = gi.SaveFiles.Split(';');
			List<string> files = new List<string>();
			if (gi.IncSub)
			{
				foreach (string s in exts)
					files.AddRange(Directory.GetFiles(savesPath, s, SearchOption.AllDirectories));
			}
			else
			{
				foreach (string s in exts)
					files.AddRange(Directory.GetFiles(savesPath, s));
			}
			string fName = string.Empty;
			string tPath = string.Empty;
			string tFile = string.Empty;
			pbCopy.Maximum = files.Count;
			pbCopy.Value = 0;
			foreach (string f in files)
			{
				if (_IsStop) break;
				fName = f.Substring(savesPath.Length + 1);
				tPath = Path.GetDirectoryName(Path.Combine(userSavesPath, fName));
				tFile = Path.Combine(userSavesPath, fName);
				if (!Directory.Exists(tPath))
					Directory.CreateDirectory(tPath);
				labCopy.Text = string.Format("正在複製 {0} 個檔案到 {1}", files.Count, tPath);
				File.Copy(Path.Combine(savesPath, fName), tFile, true);
				pbCopy.Value++;
				Application.DoEvents();
			}
			if (!_IsStop)
				this.DialogResult = DialogResult.OK;
			this.Close();
		}
		#endregion
	}
}
