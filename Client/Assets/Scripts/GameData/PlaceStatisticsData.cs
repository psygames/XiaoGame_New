using UnityEngine;
using System.Collections;

namespace RedStone
{
	public class PlaceStatisticsData : DataBase
	{
		private int m_num;
		private int m_x;
		private int m_y;
		private float m_ratio;

		public int num { get { return m_num; } }
		public int x { get { return m_x; } }
		public int y { get { return m_y; } }
		public float ratio { get { return m_ratio; } }

		public void SetData(int num,float ratio )
		{
			m_num = num;
			m_ratio = ratio;

			m_x = num / 12;
			m_y = y % 12;
		}
	}
}