using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SaveCopier
{
	#region Public Class : MenuCollection
	public class MenuCollection : ICollection<MenuInfo>
	{
		List<MenuInfo> _List = null;
		public MenuCollection(DataTable dt)
		{
			if (dt != null)
			{
				_List = new List<MenuInfo>();
				foreach (DataRow dr in dt.Rows)
					_List.Add(new MenuInfo(dr));
			}
			_List.Sort((m1, m2) => m1.Sort.CompareTo(m2.Sort));
		}

		public MenuInfo this[string id] { get { return Find(id); } }
		public MenuInfo this[int index] { get { return _List[index]; } }
		public int Count { get { return _List.Count; } }
		public bool IsReadOnly { get { return false; } }
		public MenuInfo[] ToArray() { return _List.ToArray(); }
		public MenuInfo Add(string id, string name, int sort)
		{
			MenuInfo mi = new MenuInfo(id, name, sort);
			_List.Add(mi);
			return mi;
		}

		#region ICollection<MenuItem> 成員
		public void Add(MenuInfo item) { _List.Add(item); }
		public void Clear() { _List.Clear(); }
		public MenuInfo Find(string menuId) { return _List.Find(mi => mi != null && mi.ID.Equals(menuId, StringComparison.OrdinalIgnoreCase)); }
		public bool Contains(MenuInfo item) { return _List.Exists(mi => mi != null && mi.ID.Equals(item.ID, StringComparison.OrdinalIgnoreCase)); }
		public void CopyTo(MenuInfo[] array, int arrayIndex) { _List.CopyTo(array, arrayIndex); }
		public int IndexOf(MenuInfo mi) { return _List.IndexOf(mi); }
		public bool Remove(MenuInfo item) { return _List.Remove(item); }
		#endregion

		#region IEnumerable<MenuItem> 成員
		public IEnumerator<MenuInfo> GetEnumerator() { return _List.GetEnumerator(); }
		#endregion

		#region IEnumerable 成員
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
		#endregion

		#region Public Method : int SortUp(MenuInfo mi)
		public int SortUp(MenuInfo mi)
		{
			int idx = this.IndexOf(mi);
			if (idx != 0)
			{
				mi.Sort--;
				if (mi.Sort <= _List[idx - 1].Sort)
				{
					_List[idx - 1].Sort++;
					_List.RemoveAt(idx);
					idx--;
					_List.Insert(idx, mi);
				}
			}
			return idx;
		}
		#endregion

		#region Public Method : int SortDn(MenuInfo mi)
		public int SortDn(MenuInfo mi)
		{
			int idx = this.IndexOf(mi);
			if (idx != _List.Count - 1)
			{
				mi.Sort++;
				if (mi.Sort <= _List[idx + 1].Sort)
				{
					_List[idx + 1].Sort--;
					_List.RemoveAt(idx);
					idx++;
					_List.Insert(idx, mi);
				}
			}
			return idx;
		}
		#endregion
	}
	#endregion

	#region Public Class : MenuInfo
	public class MenuInfo
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public int Sort { get; set; }

		public MenuInfo(string id, string name, int sort)
		{
			this.ID = id;
			this.Name = name;
			this.Sort = sort;
		}
		public MenuInfo(DataRow dr)
		{
			this.ID = dr["MenuID"].ToString();
			this.Name = dr["Name"].ToString();
			this.Sort = Convert.ToInt16(dr["Sort"]);
		}
	}
	#endregion

	#region Public Class : GameCollection
	public class GameCollection : ICollection<GameInfo>
	{
		List<GameInfo> _List = null;

		public GameCollection(DataTable dt)
		{
			if (dt != null)
			{
				_List = new List<GameInfo>();
				foreach (DataRow dr in dt.Rows)
					_List.Add(new GameInfo(dr));
			}
			_List.Sort((m1, m2) => m1.Sort.CompareTo(m2.Sort));
		}

		public GameInfo this[string id] { get { return Find(id); } }
		public GameInfo this[int index] { get { return _List[index]; } }
		public int Count { get { return _List.Count; } }
		public bool IsReadOnly { get { return false; } }
		public GameInfo[] ToArray() { return _List.ToArray(); }
		public int IndexOf(GameInfo gi) { return _List.IndexOf(gi); }

		#region ICollection<GameItem> 成員
		public void Add(GameInfo item) { _List.Add(item); }
		public void Clear() { _List.Clear(); }
		public GameInfo Find(string gameId) { return _List.Find(gi => gi != null && gi.ID.Equals(gameId, StringComparison.OrdinalIgnoreCase)); }
		public List<GameInfo> FindAll(Predicate<GameInfo> match) { return _List.FindAll(match); }
		public bool Contains(GameInfo item) { return _List.Exists(gi => gi != null && gi.ID.Equals(item.ID, StringComparison.OrdinalIgnoreCase)); }
		public void CopyTo(GameInfo[] array, int arrayIndex) { _List.CopyTo(array, arrayIndex); }
		public bool Remove(GameInfo item) { return _List.Remove(item); }
		#endregion

		#region IEnumerable<GameItem> 成員
		public IEnumerator<GameInfo> GetEnumerator() { return _List.GetEnumerator(); }
		#endregion

		#region IEnumerable 成員
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
		#endregion
	}
	#endregion

	#region Public Class : GameInfo
	public class GameInfo
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string MenuID { get; set; }
		public string Path { get; set; }
		public string Exec { get; set; }
		public string Icon { get; set; }
		public bool HasSave { get; set; }
		public string SavePath { get; set; }
		public string SaveFiles { get; set; }
		public bool IncSub { get; set; }
		public int Sort { get; set; }

		public GameInfo() { }
		public GameInfo(DataRow dr)
		{
			this.ID = dr["GameID"].ToString();
			this.Name = dr["Name"].ToString();
			this.MenuID = dr["MenuID"].ToString();
			this.Path = dr["Path"].ToString();
			this.Exec = dr["Exec"].ToString();
			this.Icon = dr["Icon"].ToString();
			this.HasSave = dr["HasSave"].ToString().Equals("Y", StringComparison.OrdinalIgnoreCase);
			if (this.HasSave)
			{
				this.SavePath = dr["SavePath"].ToString();
				this.SaveFiles = dr["SaveFiles"].ToString();
				this.IncSub = dr["IncSub"].ToString().Equals("Y", StringComparison.OrdinalIgnoreCase);
			}
			else
			{
				this.SavePath = string.Empty;
				this.SaveFiles = string.Empty;
				this.IncSub = false;
			}
			this.Sort = Convert.ToInt32(dr["Sort"]);
		}

		#region Public Static Method : string MimeType(string Filename)
		public static string MimeType(string Filename)
		{
			string mime = "application/octetstream";
			string ext = System.IO.Path.GetExtension(Filename).ToLower();
			Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
			if (rk != null && rk.GetValue("Content Type") != null)
				mime = rk.GetValue("Content Type").ToString();
			return mime;
		}
		#endregion

	}
	#endregion

}
