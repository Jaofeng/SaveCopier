using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;


namespace SaveCopier
{
	static class Program
	{
		[DllImport("User32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
		[DllImport("User32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		//1:normal
		//2:minimized
		//3:maximized
		private const int WS_SHOWNORMAL = 1;

		public const string CONFIG_DB = "SaveCopier.db3";
		public const string USER_DB = "Users.db3";
		public static MainForm MenuForm = null;
		public static MenuCollection Menus = null;
		public static GameCollection Games = null;
		public static NameValueCollection Config = null;
		public static bool IsExit = false;
		public static string UserDB = string.Empty;

		/// <summary>應用程式的主要進入點。</summary>
		[STAThread]
		static void Main()
		{
			Process instance = RunningInstance();
			if (instance != null)
			{
				//There is another instance of this process.  
				HandleRunningInstance(instance);
				return;
			}
			if (!Startup()) return;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MenuForm = new MainForm();
			Application.DoEvents();
			Application.Run(MenuForm);
		}

		#region Public Method : string GetCopyright()
		public static string GetCopyright()
		{
			System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
			object[] obj = asm.GetCustomAttributes(false);
			foreach (object o in obj)
				if (o.GetType() == typeof(System.Reflection.AssemblyCopyrightAttribute))
					return ((System.Reflection.AssemblyCopyrightAttribute)o).Copyright;
			return string.Empty;
		}
		#endregion

		#region Private Static Method : bool Startup()
		private static bool Startup()
		{
			bool isOK = false;
			int ret = -1;
			try
			{
				ret = DataTier.CheckConfig();
			}
			catch (Exception ex)
			{
				MessageBox.Show("程式錯誤，請聯絡管理員！\n\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			switch (ret)
			{
				case 0:
					#region 載入遊戲資料
					{
						using (DataTable dt = DataTier.LoadConfig())
						{
							if (dt == null)
							{
								MessageBox.Show("遊戲資料檔中，參數資料有誤，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								return false;
							}
							else
							{
								Config = new NameValueCollection();
								foreach (DataRow dr in dt.Rows)
									Config.Add(dr["Key"].ToString(), dr["Value"].ToString());
							}
							if (!Directory.Exists(Program.Config["SavesPath"]))
							{
								MessageBox.Show("找不到遊戲進度備份目錄，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								return false;
							}
							else if (!Directory.Exists(Program.Config["GameRoot"]))
							{
								MessageBox.Show("找不到遊戲目錄，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								return false;
							}
						}
						using (DataTable dt = DataTier.LoadMenus())
						{
							if (dt == null)
							{
								MessageBox.Show("遊戲資料檔中，選單資料有誤，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								return false;
							}
							Menus = new MenuCollection(dt);
						}
						using (DataTable dt = DataTier.LoadGames())
						{
							if (dt == null)
							{
								MessageBox.Show("遊戲資料檔中，遊戲資料有誤，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								return false;
							}
							Games = new GameCollection(dt);
						}
						isOK = true;
						break;
					}
					#endregion
				case 1:
					MessageBox.Show("找不到參數與遊戲資料檔，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				case 2:
				case 3:
					MessageBox.Show("參數遊戲資料檔錯誤，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				case 4:
					MessageBox.Show("找不到使用者帳號資料檔，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				case 5:
					MessageBox.Show("使用者帳號資料檔錯誤，請聯絡管理員！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				default:
					break;
			}
			return isOK;
		}
		#endregion

		#region Private Method : Process RunningInstance()
		private static Process RunningInstance()
		{
			Process current = Process.GetCurrentProcess();
			Process[] processes = Process.GetProcessesByName(current.ProcessName);

			//Loop through  the running processes in with the same name   
			foreach (Process process in processes)
			{
				//Ignore   the   current   process  
				if (process.Id != current.Id)
				{
					//Make sure that the process is running from the exe file.   
					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
					{
						//Return   the   other   process   instance.  
						return process;
					}
				}
			}
			//No other instance was found, return null. 
			return null;
		}
		#endregion

		#region Private Method : void HandleRunningInstance(Process instance)
		private static void HandleRunningInstance(Process instance)
		{
			//Make sure the window is not minimized or maximized 
			ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
			//Set the real intance to foreground window
			SetForegroundWindow(instance.MainWindowHandle);
		}
		#endregion

	}
}
