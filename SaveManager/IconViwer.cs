using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SaveCopier
{
	public partial class IconViwer : Form
	{
		public IconViwer()
		{
			InitializeComponent();
		}

		public int SelectedIndex { get; set; }
		public string FileName
		{
			get { return txtFile.Text; }
			set { txtFile.Text = value; }
		}

		private void ReadIcons()
		{
			if (string.IsNullOrEmpty(txtFile.Text)) return;
			Icon[] icons = IconHandler.IconsFromFile(txtFile.Text, IconSize.Large);
			Label lab = null;
			panIcons.Controls.Clear();
			if (icons != null && icons.Length != 0)
			{
				if (this.SelectedIndex == -1) this.SelectedIndex = 0;
				int h = 0;
				int w = 0;
				foreach (Icon icon in icons)
				{
					h = Math.Max(icon.Height, h);
					w = Math.Max(icon.Width, w);
				}
				foreach (Icon icon in icons)
				{
					lab = new Label();
					lab.Name = "Icon_" + panIcons.Controls.Count.ToString();
					//lab.BorderStyle = BorderStyle.FixedSingle;
					lab.Size = new Size(w + 10, h + 10);
					lab.Margin = new Padding(3);
					lab.ImageAlign = ContentAlignment.MiddleCenter;
					lab.Image = icon.ToBitmap();
					lab.TabIndex = panIcons.Controls.Count;
					lab.Click += new EventHandler(Icon_Click);
					lab.DoubleClick += new EventHandler(Icon_DoubleClick);
					panIcons.Controls.Add(lab);
				}
				panIcons.Controls["Icon_" + this.SelectedIndex.ToString()].BackColor = SystemColors.Highlight;
			}
		}

		private void IconViwer_Shown(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(txtFile.Text))
				ReadIcons();
		}

		private void btnUse_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Icon_DoubleClick(object sender, EventArgs e)
		{
			Icon_Click(sender, e);
			btnUse_Click(btnUse, e);
		}

		private void Icon_Click(object sender, EventArgs e)
		{
			panIcons.Controls["Icon_" + this.SelectedIndex.ToString()].BackColor = Color.Transparent;
			Label lab = (Label)sender;
			this.SelectedIndex = lab.TabIndex;
			panIcons.Controls["Icon_" + this.SelectedIndex.ToString()].BackColor = SystemColors.Highlight;
		}

	}
}
