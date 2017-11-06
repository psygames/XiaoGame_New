using UnityEngine;
using System.Collections;

namespace RedStone
{
	public enum ECamp
	{
		None = 1,
		White = 2, // 对应于 ChessType 可强转
		Black = 3,
	}

	public class PlayerData : DataBase
	{
		private int m_uuid;
		private string m_name;
		private ECamp m_camp;

		public int uuid { get { return m_uuid; } }
		public string name { get { return m_name; } }
		public ECamp camp { get { return m_camp; } }
		public ECamp enemyCamp
		{
			get
			{
				if (m_camp == ECamp.White)
					return ECamp.Black;
				else if (m_camp == ECamp.Black)
					return ECamp.White;
				return ECamp.None;
			}
		}

		public void SetData(message.LoginReply info)
		{
			m_uuid = 1;
			m_name = info.name;
			m_camp = ECamp.None;
		}

		public void UpdateCamp(message.Enums.Camp camp)
		{
			m_camp = (ECamp)camp;
		}
	}
}