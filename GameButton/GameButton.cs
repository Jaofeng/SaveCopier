using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SaveCopier
{
	[DefaultProperty("Text")]
	public class GameButton : Control
	{
		public event EventHandler DownloadClick;
		public event EventHandler UploadClick;
		private Label _IconArea = new Label();
		private Label _NameLabel = new Label();
		private Panel _SavePanel = new Panel();
		private Panel _TextPanel = new Panel();
		private Label _DLSaveLabel = new Label();
		private Label _ULSaveLabel = new Label();
		private Control _OnMouseDownControl = null;

		#region Public Method : GameButton()
		public GameButton()
		{
			InitializeComponent();
		}
		#endregion

		#region Private Method : void InitializeComponent()
		private void InitializeComponent()
		{
			_SavePanel.Width = 80;
			_SavePanel.BackColor = Color.Transparent;
			_SavePanel.Dock = DockStyle.Right;
			_SavePanel.Padding = new Padding(0, 0, 0, 0);
			this.Controls.Add(_SavePanel);
			_SavePanel.SendToBack();
			_DLSaveLabel.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			_DLSaveLabel.AutoSize = false;
			_DLSaveLabel.AutoEllipsis = true;
			_DLSaveLabel.Location = new Point(_SavePanel.Padding.Left, _SavePanel.Padding.Top);
			_DLSaveLabel.Size = new Size(_SavePanel.Bounds.Width - _SavePanel.Padding.Horizontal, (_SavePanel.Bounds.Height - _SavePanel.Padding.Vertical) / 2);
			_DLSaveLabel.TextAlign = ContentAlignment.MiddleLeft;
			_DLSaveLabel.Image = null;
			_DLSaveLabel.ImageAlign = ContentAlignment.MiddleLeft;
			_DLSaveLabel.Paint += new PaintEventHandler(SaveButton_Paint);
			_DLSaveLabel.MouseClick += new MouseEventHandler(SaveLabel_MouseClick);
			_DLSaveLabel.MouseDown += new MouseEventHandler(SaveLabel_MouseDown);
			_DLSaveLabel.MouseUp += new MouseEventHandler(SaveLabel_MouseUp);
			_SavePanel.Controls.Add(_DLSaveLabel);
			_ULSaveLabel.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			_ULSaveLabel.AutoSize = false;
			_ULSaveLabel.AutoEllipsis = true;
			_ULSaveLabel.Location = new Point(_SavePanel.Padding.Left, (_SavePanel.Bounds.Height - _SavePanel.Padding.Vertical) / 2);
			_ULSaveLabel.Size = new Size(_SavePanel.Bounds.Width - _SavePanel.Padding.Horizontal, _SavePanel.Bounds.Height - _ULSaveLabel.Height);
			_ULSaveLabel.TextAlign = ContentAlignment.MiddleLeft;
			_ULSaveLabel.Image = null;
			_ULSaveLabel.ImageAlign = ContentAlignment.MiddleLeft;
			_ULSaveLabel.Paint += new PaintEventHandler(SaveButton_Paint);
			_ULSaveLabel.MouseClick += new MouseEventHandler(SaveLabel_MouseClick);
			_ULSaveLabel.MouseDown += new MouseEventHandler(SaveLabel_MouseDown);
			_ULSaveLabel.MouseUp += new MouseEventHandler(SaveLabel_MouseUp);
			_SavePanel.Controls.Add(_ULSaveLabel);
			_SavePanel.Resize += new EventHandler(SavePanel_Resize);

			_TextPanel.Dock = DockStyle.Fill;
			_TextPanel.BackColor = Color.Transparent;
			_TextPanel.Padding = new Padding(0);
			_TextPanel.Paint += new PaintEventHandler(TextPanel_Paint);
			this.Controls.Add(_TextPanel);
			_TextPanel.BringToFront();
			_IconArea.Dock = DockStyle.Left;
			_IconArea.ImageAlign = ContentAlignment.MiddleCenter;
			_IconArea.Width = 32;
			_IconArea.MouseEnter += new EventHandler(TextPanel_MouseEnter);
			_IconArea.MouseLeave += new EventHandler(TextPanel_MouseLeave);
			_IconArea.MouseDown += new MouseEventHandler(TextPanel_MouseDown);
			_IconArea.MouseUp += new MouseEventHandler(TextPanel_MouseUp);
			_IconArea.MouseClick += new MouseEventHandler(TextPanel_MouseClick);
			_TextPanel.Controls.Add(_IconArea);
			_IconArea.SendToBack();
			_NameLabel.Dock = DockStyle.Fill;
			_NameLabel.BackColor = Color.Transparent;
			_NameLabel.AutoSize = false;
			_NameLabel.TextAlign = ContentAlignment.MiddleLeft;
			_NameLabel.AutoEllipsis = true;
			_NameLabel.MouseEnter += new EventHandler(TextPanel_MouseEnter);
			_NameLabel.MouseLeave += new EventHandler(TextPanel_MouseLeave);
			_NameLabel.MouseDown += new MouseEventHandler(TextPanel_MouseDown);
			_NameLabel.MouseUp += new MouseEventHandler(TextPanel_MouseUp);
			_NameLabel.MouseClick += new MouseEventHandler(TextPanel_MouseClick);
			_TextPanel.Controls.Add(_NameLabel);
			_NameLabel.BringToFront();
			this.ShowSaveButton = false;
			this.ShowIcon = false;
		}
		#endregion

		#region Override Properties
		public override string Text
		{
			get { return _NameLabel.Text; }
			set { _NameLabel.Text = value; }
		}
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				_NameLabel.ForeColor = value;
			}
		}
		public override Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
				_NameLabel.Font = value;
			}
		}
		#endregion

		#region Not Browsable Properties
		[Browsable(false)]
		public override ContextMenu ContextMenu
		{
			get { return base.ContextMenu; }
			set { base.ContextMenu = value; }
		}
		[Browsable(false)]
		public override ContextMenuStrip ContextMenuStrip
		{
			get { return base.ContextMenuStrip; }
			set { base.ContextMenuStrip = value; }
		}
		[Browsable(false)]
		public override Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}
		[Browsable(false)]
		public override Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { base.MinimumSize = value; }
		}
		[Browsable(false)]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}
		[Browsable(false)]
		public new ImeMode ImeMode
		{
			get { return base.ImeMode; }
			set { base.ImeMode = value; }
		}
		#endregion

		#region Custom Properties
		[DefaultValue(typeof(Image), "null")]
		Image _Icon = null;
		public Image Icon
		{
			get { return _Icon; }
			set
			{
				_Icon = value;
				ResetIcon(_Icon);
			}
		}
		[DefaultValue(40)]
		public int IconWidth
		{
			get { return _IconArea.Width; }
			set { _IconArea.Width = value; 
			}
		}
		[DefaultValue(80)]
		public int SaveButtonWidth
		{
			get { return _SavePanel.Width; }
			set { _SavePanel.Width = value; }
		}
		[DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
		public ContentAlignment TextAlign
		{
			get { return _NameLabel.TextAlign; }
			set { _NameLabel.TextAlign = value; }
		}

		[DefaultValue(false)]
		public bool ShowSaveButton
		{
			get { return _SavePanel.Visible; }
			set { _SavePanel.Visible = value; }
		}
		[DefaultValue(false)]
		public bool ShowIcon
		{
			get { return _IconArea.Visible; }
			set { _IconArea.Visible = value; }
		}

		#region Property : string DownloadButtonText
		string _DownloadButtonText = string.Empty;
		[DefaultValue("")]
		public string DownloadButtonText
		{
			get { return _DownloadButtonText; }
			set
			{
				_DownloadButtonText = value;
				SetDLSaveLabel();
			}
		}
		#endregion

		#region Property : string UploadButtonText
		string _UploadButtonText = string.Empty;
		[DefaultValue("")]
		public string UploadButtonText
		{
			get { return _UploadButtonText; }
			set
			{
				_UploadButtonText = value;
				SetULSaveLabel();
			}
		}
		#endregion

		[DefaultValue(typeof(Image), "null")]
		public Image DownloadButtonImage
		{
			get { return _DLSaveLabel.Image; }
			set
			{
				_DLSaveLabel.Image = value;
				SetDLSaveLabel();
			}
		}
		[DefaultValue(typeof(Image), "null")]
		public Image UploadButtonImage
		{
			get { return _ULSaveLabel.Image; }
			set
			{
				_ULSaveLabel.Image = value;
				SetULSaveLabel();
			}
		}
		[DefaultValue(typeof(Font), "新細明體, 9pt")]
		public Font DownloadButtonFont
		{
			get { return _DLSaveLabel.Font; }
			set
			{
				_DLSaveLabel.Font = value;
				this.DownloadButtonText = _DownloadButtonText;
			}
		}
		[DefaultValue(typeof(Font), "新細明體, 9pt")]
		public Font UploadButtonFont
		{
			get { return _ULSaveLabel.Font; }
			set
			{
				_ULSaveLabel.Font = value;
				this.UploadButtonText = _UploadButtonText;
			}
		}
		[DefaultValue(true)]
		public bool DownloadButtonEnabled
		{
			get { return _DLSaveLabel.Enabled; }
			set
			{
				_DLSaveLabel.Enabled = value;
				DrawButtonStyle(_DLSaveLabel, false);
			}
		}
		[DefaultValue(true)]
		public bool UploadButtonEnabled
		{
			get { return _ULSaveLabel.Enabled; }
			set
			{
				_ULSaveLabel.Enabled = value;
				DrawButtonStyle(_ULSaveLabel, false);
			}
		}
		#endregion

		#region Private Method : void SetDLSaveLabel()
		private void SetDLSaveLabel()
		{
			if (string.IsNullOrEmpty(_DownloadButtonText))
				_DLSaveLabel.Text = string.Empty;
			else
			{
				if (_DLSaveLabel.Image != null)
				{
					Graphics g = _DLSaveLabel.CreateGraphics();
					SizeF strS = g.MeasureString(" ", _DLSaveLabel.Font);
					Size imgS = _DLSaveLabel.Image.Size;
					int pad = (int)Math.Ceiling(imgS.Width / strS.Width);
					_DLSaveLabel.TextAlign = ContentAlignment.MiddleLeft;
					_DLSaveLabel.Text = " ".PadLeft(pad + 1) + _DownloadButtonText;
				}
				else
				{
					_DLSaveLabel.TextAlign = ContentAlignment.MiddleCenter;
					_DLSaveLabel.Text = _DownloadButtonText;
				}
			}
		}
		#endregion

		#region Private Method : void SetULSaveLabel()
		private void SetULSaveLabel()
		{
			if (string.IsNullOrEmpty(_UploadButtonText))
				_ULSaveLabel.Text = string.Empty;
			else
			{
				if (_ULSaveLabel.Image != null)
				{
					Graphics g = _ULSaveLabel.CreateGraphics();
					SizeF strS = g.MeasureString(" ", _ULSaveLabel.Font);
					Size imgS = _ULSaveLabel.Image.Size;
					int pad = (int)Math.Ceiling(imgS.Width / strS.Width);
					_ULSaveLabel.TextAlign = ContentAlignment.MiddleLeft;
					_ULSaveLabel.Text = " ".PadLeft(pad + 1) + _UploadButtonText;
				}
				else
				{
					_ULSaveLabel.TextAlign = ContentAlignment.MiddleCenter;
					_ULSaveLabel.Text = _UploadButtonText;
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
			//if (c.Enabled)
			//{
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
			//}
			//else
			//{
			//    g.DrawLines(p3, ps1);
			//    g.DrawLines(p3, ps2);
			//}
		}
		#endregion

		#region Private Method : void TextPanel_Paint(object sender, PaintEventArgs e)
		private void TextPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen p1 = new Pen(Color.WhiteSmoke, 1F);
			Pen p2 = new Pen(Color.DimGray, 1F);
			Point[] ps1 = { new Point(_TextPanel.Width - 1, 0), new Point(0, 0), new Point(0, _TextPanel.Height - 1) };
			Point[] ps2 = { new Point(_TextPanel.Width - 1, 0), new Point(_TextPanel.Width - 1, _TextPanel.Height - 1), new Point(0, _TextPanel.Height - 1) };
			if (_OnMouseDownControl == null)
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
		#endregion

		#region Private Method : void TextPanel_MouseClick(object sender, MouseEventArgs e)
		private void TextPanel_MouseClick(object sender, MouseEventArgs e)
		{
			base.OnClick(new EventArgs());
		}
		#endregion

		#region Private Method : void TextPanel_MouseUp(object sender, MouseEventArgs e)
		private void TextPanel_MouseUp(object sender, MouseEventArgs e)
		{
			Control c = (Control)sender;
			_OnMouseDownControl = null;
			c.Parent.Refresh();
			base.OnMouseUp(e);
		}
		#endregion

		#region Private Method : void TextPanel_MouseDown(object sender, MouseEventArgs e)
		private void TextPanel_MouseDown(object sender, MouseEventArgs e)
		{
			Control c = (Control)sender;
			_OnMouseDownControl = c;
			c.Parent.Refresh();
			base.OnMouseDown(e);
		}
		#endregion

		#region Private Method : void TextPanel_MouseLeave(object sender, EventArgs e)
		private void TextPanel_MouseLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			c.Parent.Refresh();
			base.OnMouseLeave(e);
		}
		#endregion

		#region Private Method : void TextPanel_MouseEnter(object sender, EventArgs e)
		private void TextPanel_MouseEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			c.Parent.Refresh();
			base.OnMouseEnter(e);
		}
		#endregion

		#region Private Method : void SaveButton_Paint(object sender, PaintEventArgs e)
		private void SaveButton_Paint(object sender, PaintEventArgs e)
		{
			DrawButtonStyle((Control)sender, e.Graphics, false);
		}
		#endregion

		#region Private Method : void SavePanel_Resize(object sender, EventArgs e)
		private void SavePanel_Resize(object sender, EventArgs e)
		{
			_DLSaveLabel.Location = new Point(_SavePanel.Padding.Left, _SavePanel.Padding.Top);
			_DLSaveLabel.Size = new Size(_SavePanel.Bounds.Width - _SavePanel.Padding.Horizontal, (_SavePanel.Bounds.Height - _SavePanel.Padding.Vertical) / 2);
			_ULSaveLabel.Location = new Point(_SavePanel.Padding.Left, (_SavePanel.Bounds.Height - _SavePanel.Padding.Vertical) / 2);
			_ULSaveLabel.Size = new Size(_SavePanel.Bounds.Width - _SavePanel.Padding.Horizontal, _SavePanel.Bounds.Height - _DLSaveLabel.Height);
		}
		#endregion

		#region Private Method : void SaveLabel_MouseClick(object sender, MouseEventArgs e)
		private void SaveLabel_MouseClick(object sender, MouseEventArgs e)
		{
			Label lab = (Label)sender;
			if (lab.Equals(_ULSaveLabel))
			{
				if (UploadClick != null)
					UploadClick.Invoke(this, new EventArgs());
			}
			else if (lab.Equals(_DLSaveLabel))
			{
				if (DownloadClick != null)
					DownloadClick.Invoke(this, new EventArgs());
			}
		}
		#endregion

		#region Private Method : void SaveLabel_MouseUp(object sender, MouseEventArgs e)
		private void SaveLabel_MouseUp(object sender, MouseEventArgs e)
		{
			Label lab = (Label)sender;
			_OnMouseDownControl = null;
			DrawButtonStyle(lab, false);
		}
		#endregion

		#region Private Method : void SaveLabel_MouseDown(object sender, MouseEventArgs e)
		private void SaveLabel_MouseDown(object sender, MouseEventArgs e)
		{
			Label lab = (Label)sender;
			_OnMouseDownControl = lab;
			DrawButtonStyle(lab, true);
		}
		#endregion

		#region Override Method : void OnSizeChanged(EventArgs e)
		protected override void OnSizeChanged(EventArgs e)
		{
			ResetIcon(_Icon);
			base.OnSizeChanged(e);
		}
		#endregion

		#region Private Method : void ResetIcon(Image source)
		private void ResetIcon(Image source)
		{
			if (source == null)
				_IconArea.Image = null;
			else
			{
				Image img = source;
				if (img.Width > this.IconWidth || img.Height > this.Height)
					img = img.GetThumbnailImage(this.IconWidth, this.Height, null, IntPtr.Zero);
				_IconArea.Image = img;
			}
		}
		#endregion
	}
}
