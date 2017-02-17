using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text;

namespace SaveCopier
{
	enum ResultType : ushort
	{
		None = 0,
		OK = 1,
		Fail = 2,
		Error = 3
	}
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
						//using (SQLiteCommand cmd = new SQLiteCommand("select value from TB_Config Where Key='SavesPath'", conn))
						//{
						//    object o = cmd.ExecuteScalar();
						//    if (o == null || string.IsNullOrEmpty(o.ToString()))
						//        return 3;
						//    else
						//        Program.UserDB = System.IO.Path.Combine(o.ToString(), Program.USER_DB);
						//}
						conn.Close();
					}
					//if (!System.IO.File.Exists(Program.UserDB))
					//    return 4;
					//else
					//{
					//    using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.UserDB))
					//    {
					//        conn.Open();
					//        if (!IsTableExist(conn, "TB_Users")) return 5;
					//        conn.Close();
					//    }
					//}
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
			catch { }
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

		#region Public Static Method : bool SaveConfig()
		public static bool SaveConfig()
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					using (SQLiteTransaction tran = conn.BeginTransaction())
					{
						string sql = "update TB_Config set Value=@Value where Key=@Key";
						using (SQLiteCommand cmd = new SQLiteCommand(sql, conn, tran))
						{
							cmd.Parameters.AddWithValue("@Key", "");
							cmd.Parameters.AddWithValue("@Value", "");
							foreach (string k in Program.Config.AllKeys)
							{
								cmd.Parameters["@Key"].Value = k;
								cmd.Parameters["@Value"].Value = Program.Config[k];
								cmd.ExecuteNonQuery();
							}
						}
						tran.Commit();
					}
					conn.Close();
				}
				result = true;
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool UpdateMenu(MenuInfo mi)
		public static bool UpdateMenu(MenuInfo mi)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					string sql = "update TB_Menus set Name=@Name,Sort=@Sort where MenuID=@ID";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@ID", mi.ID);
						cmd.Parameters.AddWithValue("@Name", mi.Name);
						cmd.Parameters.AddWithValue("@Sort", mi.Sort);
						cmd.ExecuteNonQuery();
					}
					conn.Close();
				}
				result = true;
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool AppendMenu(MenuInfo mi)
		public static bool AppendMenu(MenuInfo mi)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					string sql = "insert into TB_Menus(MenuID,Name,Sort) values(@ID,@Name,@Sort)";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@ID", mi.ID);
						cmd.Parameters.AddWithValue("@Name", mi.Name);
						cmd.Parameters.AddWithValue("@Sort", mi.Sort);
						cmd.ExecuteNonQuery();
					}
					conn.Close();
				}
				result = true;
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool DeleteMenu(MenuInfo mi)
		public static bool DeleteMenu(MenuInfo mi)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					using (SQLiteTransaction tran = conn.BeginTransaction())
					{
						string sql = "delete from TB_Menus where MenuID=@ID";
						using (SQLiteCommand cmd = new SQLiteCommand(sql, conn, tran))
						{
							cmd.Parameters.AddWithValue("@ID", mi.ID);
							cmd.ExecuteNonQuery();
						}
						sql = "update TB_Menus set Sort=Sort-1 where Sort>@Sort";
						using (SQLiteCommand cmd = new SQLiteCommand(sql, conn, tran))
						{
							cmd.Parameters.AddWithValue("@Sort", mi.Sort);
							cmd.ExecuteNonQuery();
						}
						tran.Commit();
					}
					conn.Close();
				}
				result = true;
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool UpdateGame(GameInfo gi)
		public static bool UpdateGame(GameInfo gi)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					string sql = "update TB_Games set Name=@Name,MenuID=@MenuID,Path=@Path,Exec=@Exec,Icon=@Icon,HasSave=@HasSave,SavePath=@SavePath,SaveFiles=@SaveFiles,IncSub=@IncSub,Sort=@Sort where GameID=@ID";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@ID", gi.ID);
						cmd.Parameters.AddWithValue("@Name", gi.Name);
						cmd.Parameters.AddWithValue("@MenuID", gi.MenuID);
						cmd.Parameters.AddWithValue("@Path", gi.Path);
						cmd.Parameters.AddWithValue("@Exec", gi.Exec);
						cmd.Parameters.AddWithValue("@Icon", gi.Icon);
						cmd.Parameters.AddWithValue("@HasSave", (gi.HasSave ? "Y" : "N"));
						cmd.Parameters.AddWithValue("@SavePath", gi.SavePath);
						cmd.Parameters.AddWithValue("@SaveFiles", gi.SaveFiles);
						cmd.Parameters.AddWithValue("@IncSub", (gi.IncSub ? "Y" : "N"));
						cmd.Parameters.AddWithValue("@Sort", gi.Sort);
						cmd.ExecuteNonQuery();
					}
					conn.Close();
				}
				result = true;
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool AppendGame(GameInfo gi)
		public static bool AppendGame(GameInfo gi)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					string sql = "insert into TB_Games(GameID,Name,MenuID,Path,Exec,Icon,HasSave,SavePath,SaveFiles,IncSub,Sort) values(@ID,@Name,@MenuID,@Path,@Exec,@Icon,@HasSave,@SavePath,@SaveFiles,@IncSub,@Sort)";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@ID", gi.ID);
						cmd.Parameters.AddWithValue("@Name", gi.Name);
						cmd.Parameters.AddWithValue("@MenuID", gi.MenuID);
						cmd.Parameters.AddWithValue("@Path", gi.Path);
						cmd.Parameters.AddWithValue("@Exec", gi.Exec);
						cmd.Parameters.AddWithValue("@Icon", gi.Icon);
						cmd.Parameters.AddWithValue("@HasSave", (gi.HasSave ? "Y" : "N"));
						cmd.Parameters.AddWithValue("@SavePath", gi.SavePath);
						cmd.Parameters.AddWithValue("@SaveFiles", gi.SaveFiles);
						cmd.Parameters.AddWithValue("@IncSub", (gi.IncSub ? "Y" : "N"));
						cmd.Parameters.AddWithValue("@Sort", gi.Sort);
						cmd.ExecuteNonQuery();
					}
					conn.Close();
				}
				result = true;
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : bool DeleteGame(GameInfo gi)
		public static bool DeleteGame(GameInfo gi)
		{
			bool result = false;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + Program.CONFIG_DB))
				{
					conn.Open();
					using (SQLiteTransaction tran = conn.BeginTransaction())
					{
						string sql = "delete from TB_Games where GameID=@ID";
						using (SQLiteCommand cmd = new SQLiteCommand(sql, conn, tran))
						{
							cmd.Parameters.AddWithValue("@ID", gi.ID);
							cmd.ExecuteNonQuery();
						}
						sql = "update TB_Games set Sort=Sort-1 where MenuID=@MenuID and Sort>@Sort";
						using (SQLiteCommand cmd = new SQLiteCommand(sql, conn, tran))
						{
							cmd.Parameters.AddWithValue("@MenuID", gi.MenuID);
							cmd.Parameters.AddWithValue("@Sort", gi.Sort);
							cmd.ExecuteNonQuery();
						}
						tran.Commit();
					}
					conn.Close();
				}
				result = true;
			}
			catch { }
			return result;
		}
		#endregion

		#region Public Static Method : DataTable LoadUsers()
		public static DataTable LoadUsers()
		{
			DataTable result = null;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + System.IO.Path.Combine(Program.Config["SavesPath"], Program.USER_DB)))
				{
					conn.Open();
					string sql = "select RowID,UserID,PWD,LastLogin,Created from TB_Users order by UserID";
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

		#region Public Static Method : ResultType AppendUser(string userId, string pwd, DateTime time)
		public static ResultType AppendUser(string userId, string pwd, DateTime time)
		{
			ResultType result = ResultType.None;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + System.IO.Path.Combine(Program.Config["SavesPath"], Program.USER_DB)))
				{
					conn.Open();
					using (SQLiteTransaction tran = conn.BeginTransaction())
					{
						string sql = "select UserID, PWD from TB_Users where UserID=@UserID";
						using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
						{
							cmd.Parameters.AddWithValue("@UserID", userId);
							object o = cmd.ExecuteScalar();
							if (o == null || string.IsNullOrEmpty(o.ToString()))
							{
								sql = "insert into TB_Users(UserID,PWD,Created) values(@UserID,@PWD,@Created)";
								cmd.CommandText = sql;
								cmd.Parameters.AddWithValue("@PWD", pwd);
								cmd.Parameters.AddWithValue("@Created", time);
								cmd.ExecuteNonQuery();
								result = ResultType.OK;
							}
							else
								result = ResultType.Fail;
						}
						tran.Commit();
					}
					conn.Close();
				}
			}
			catch { result = ResultType.Error; }
			return result;
		}
		#endregion

		#region Public Static Method : ResultType UpdateUser(int rowId, string userId, string pwd)
		public static ResultType UpdateUser(int rowId, string userId, string pwd)
		{
			ResultType result = ResultType.None;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + System.IO.Path.Combine(Program.Config["SavesPath"], Program.USER_DB)))
				{
					conn.Open();
					string sql = "select RowID from TB_Users where UserID=@UserID";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@UserID", userId);
						object o = cmd.ExecuteScalar();
						if (o != null)
							return ResultType.Fail;
						else
						{
							cmd.CommandText = "update TB_Users set UserID=@UserID where RowID=@RowID";
							cmd.Parameters.AddWithValue("@RowID", rowId);
							cmd.ExecuteNonQuery();
							result = ResultType.OK;
						}
					}
					conn.Close();
				}
			}
			catch { result = ResultType.Error; }
			return result;
		}
		#endregion

		#region Public Static Method : ResultType DeleteUser(int rowId)
		public static ResultType DeleteUser(int rowId)
		{
			ResultType result = ResultType.None;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + System.IO.Path.Combine(Program.Config["SavesPath"], Program.USER_DB)))
				{
					conn.Open();
					string sql = "delete from TB_Users where RowID=@RowID";
					using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@RowID", rowId);
						if (cmd.ExecuteNonQuery() == 0)
							result = ResultType.Fail;
						else
							result = ResultType.OK;
					}
					conn.Close();
				}
			}
			catch { result = ResultType.Error; }
			return result;
		}
		#endregion

		#region Public Static Method : int UpdateUserList(DataRow[] drs)
		public static int UpdateUserList(DataRow[] drs)
		{
			int result = 0;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + System.IO.Path.Combine(Program.Config["SavesPath"], Program.USER_DB)))
				{
					conn.Open();
					string sql = string.Empty;
					using (SQLiteTransaction trans = conn.BeginTransaction())
					{
						foreach (DataRow dr in drs)
						{
							switch (dr.RowState)
							{
								case DataRowState.Added:
									sql = "insert into TB_Users(UserID,PWD,Created) values(@UserID,@PWD,@Created)";
									using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
									{
										cmd.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
										cmd.Parameters.AddWithValue("@PWD", dr["PWD"].ToString());
										cmd.Parameters.AddWithValue("@Created", Convert.ToDateTime(dr["Created"]));
										result += cmd.ExecuteNonQuery();
									}
									break;
								case DataRowState.Modified:
									sql = "update TB_Users set UserID=@UserID,PWD=@PWD where RowID=@RowID";
									using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
									{
										cmd.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
										cmd.Parameters.AddWithValue("@PWD", dr["PWD"].ToString());
										cmd.Parameters.AddWithValue("@RowID", Convert.ToInt64(dr["RowID"]));
										result += cmd.ExecuteNonQuery();
									}
									break;
								case DataRowState.Deleted:
									sql = "delete from TB_Users where RowID=@RowID";
									using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
									{
										cmd.Parameters.AddWithValue("@RowID", Convert.ToInt64(dr["RowID"]));
										result += cmd.ExecuteNonQuery();
									}
									break;
							}
						}
						trans.Commit();
					}
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(string.Format("無法新增或修改使用者帳號資料!!\n\n原因:\n{0}", ex.Message), System.Windows.Forms.Application.ProductName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			}
			return result;
		}
		#endregion

		#region Public Static Method : int UpdateUserList(DataTable dt)
		public static int UpdateUserList(DataTable dt)
		{
			int result = 0;
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection("data source=" + System.IO.Path.Combine(Program.Config["SavesPath"], Program.USER_DB)))
				{
					conn.Open();
					string sql = string.Empty;
					using (SQLiteTransaction trans = conn.BeginTransaction())
					{
						foreach (DataRow dr in dt.Rows)
						{
							switch (dr.RowState)
							{
								case DataRowState.Added:
									sql = "insert into TB_Users(UserID,PWD,Created) values(@UserID,@PWD,@Created)";
									using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
									{
										cmd.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
										cmd.Parameters.AddWithValue("@PWD", dr["PWD"].ToString());
										cmd.Parameters.AddWithValue("@Created", Convert.ToDateTime(dr["Created"]));
										result += cmd.ExecuteNonQuery();
									}
									break;
								case DataRowState.Modified:
									sql = "update TB_Users set UserID=@UserID,PWD=@PWD where RowID=@RowID";
									using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
									{
										cmd.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
										cmd.Parameters.AddWithValue("@PWD", dr["PWD"].ToString());
										cmd.Parameters.AddWithValue("@RowID", Convert.ToInt64(dr["RowID"]));
										result += cmd.ExecuteNonQuery();
									}
									break;
								case DataRowState.Deleted:
									sql = "delete from TB_Users where RowID=@RowID";
									dr.RejectChanges();
									using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
									{
										cmd.Parameters.AddWithValue("@RowID", Convert.ToInt64(dr["RowID"]));
										result += cmd.ExecuteNonQuery();
									}
									break;
							}
						}
						trans.Commit();
					}
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(string.Format("無法新增或修改使用者帳號資料!!\n\n原因:\n{0}", ex.Message), System.Windows.Forms.Application.ProductName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				result = 0;
			}
			return result;
		}
		#endregion

	}
}
