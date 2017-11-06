using System.Collections;

namespace RedStone
{
	public class ChessData : DataBase
	{
		private int m_num;
		private int m_x;
		private int m_y;
		private message.Enums.ChessType m_type;

		public int num { get { return m_num; } }
		public int x { get { return m_x; } }
		public int y { get { return m_y; } }
		public message.Enums.ChessType type { get { return m_type; } }

		public void SetData(int num,int x, int y, message.Enums.ChessType type)
		{
			m_num = num;
			m_x = x;
			m_y = y;
			m_type = type;
		}
	}
}