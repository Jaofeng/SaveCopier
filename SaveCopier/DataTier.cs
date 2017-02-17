using System;
using System.Data;
using System.Data.SQLite;

namespace SaveCopier
{
	class DataTier
	{
		#region Private Method : bool IsTableExist(SQLiteConnection conn, string name)
		private static bool IsTableExist(SQLiteConnection conn, string name)
		{
			return conn.GetSchema("Tables").Select("Table_Name = '" + name + "'").Length > 0;
		}
		#endregion

		#region Public Static Method : int CheckConfig()
		public static int CheckConfig()
		{
			try
			{
				if (!System.IO.File.Exists(Program.CONFIG_DB))
					return 1;
				else
				{
					using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
					{
						conn.Open();
						if (!IsTableExist(conn, "TB_Config")) return 2;
						if (!IsTableExist(conn, "TB_Menus")) return 2;
						if (!IsTableExist(conn, "TB_Games")) return 2;
						using (SQLiteCommand cmd = new SQLiteCommand("select value from TB_Config Where Key='SavesPath'", conn))
						{
							object o = cmd.ExecuteScalar();
							if (o == null || string.IsNullOrEmpty(o.ToString()))
								return 3;
							else
								Program.UserDB = System.IO.Path.Combine(o.ToString(), Program.USER_DB);
						}
						conn.Close();
					}
					if (!System.IO.File.Exists(Program.UserDB))
						return 4;
					else
					{
						using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.UserDB))
						{
							conn.Open();
							if (!IsTableExist(conn, "TB_Users")) return 5;
							conn.Close();
						}
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Public Static Method : DataTable LoadConfig()
		public static DataTable LoadConfig()
		{
			DataTable result = null;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					string sql = "select Key,Value from TB_Config";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						result = new DataTable();
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
							da.Fill(result);
					}
					conn.Close();
				}

			}
			catch { result = null; }
			return result;
		}
		#endregion

		#region Public Static Method : DataTable LoadMenus()
		public static DataTable LoadMenus()
		{
			DataTable result = null;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					string sql = "select MenuID,Name,Sort from TB_Menus order by Sort,RowID";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						result = new DataTable();
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
							da.Fill(result);
					}
					conn.Close();
				}

			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : DataTable LoadGames()
		public static DataTable LoadGames()
		{
			DataTable result = null;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					string sql = "select GameID,Name,MenuID,Path,Exec,Icon,HasSave,SavePath,SaveFiles,IncSub,Sort from TB_Games order by Sort,RowID";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						result = new DataTable();
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
							da.Fill(result);
					}
					conn.Close();
				}

			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool SaveNewUser(string userId, string pwd)
		public static bool SaveNewUser(string userId, string pwd)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.UserDB))
				{
					conn.Open();
					using (SQLiteTransaction tran = conn.BeginTransaction())
					{
						string sql = "select UserID from TB_Users where UserID=@UserID";
						using (SQLiteCommand cmd = new SQLiteCommand(sql, conn, tran))
						{
							cmd.Parameters.AddWithValue("@UserID", userId);
							object o = cmd.ExecuteScalar();
							if (o == null || string.IsNullOrEmpty(o.ToString()))
							{
								sql = "insert into TB_Users(UserID,PWD,Created) values(@UserID,@PWD,@Created)";
								cmd.CommandText = sql;
								cmd.Parameters.AddWithValue("@PWD", pwd);
								cmd.Parameters.AddWithValue("@Created", DateTime.Now);
								cmd.ExecuteNonQuery();
								result = true;
							}
						}
						tran.Commit();
					}
					conn.Close();
				}
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool UserLogin(string userId, string pwd)
		public static bool UserLogin(string userId, string pwd)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.UserDB))
				{
					conn.Open();
					string sql = "select PWD from TB_Users where UserID=@UserID";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@UserID", userId);
						object o = cmd.ExecuteScalar();
						if (o != null && !string.IsNullOrEmpty(o.ToString()))
							result = pwd.Equals(o.ToString());
						if (result)
						{
							cmd.CommandText = "update TB_Users set LastLogin=datetime() where UserID=@UserID";
							cmd.ExecuteNonQuery();
						}
					}
					conn.Close();
				}
			}
			catch { }
			return result;
		}
		#endregion
	}
}
