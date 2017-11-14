using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TablePath
	{
		public TablePath() { }
		public TablePath(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.mapid = (int)dict["mapid"];
			this.sceneName = (string)dict["sceneName"];
			this.pathType = (int)dict["pathType"];
			this.Positions = (Vector3[])dict["Positions"];
			this.Position = (Vector3)dict["Position"];
			this.Figures = (float[])dict["Figures"];
			this.ConnectedIds = (int[])dict["ConnectedIds"];
			this.WayPointFigures = (int[])dict["WayPointFigures"];
		}

		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int id;
		/// <summary>
		/// 地图id
		/// </summary>
		public int mapid;
		/// <summary>
		/// 场景名称
		/// </summary>
		public string sceneName;
		/// <summary>
		/// 1路点 2掩体 3站点模式路点 4攻防战防守点 5攻防战中心 6攻防战中间点
		/// </summary>
		public int pathType;
		/// <summary>
		/// 掩体：射击点位置，掩体朝向。路点:蹲点的位置。攻防战中间点:AC朝向，BD朝向。
		/// </summary>
		public Vector3[] Positions;
		/// <summary>
		/// path自身位置
		/// </summary>
		public Vector3 Position;
		/// <summary>
		/// 路点中蹲点的数值，和positions中的位置相对应。掩体不使用
		/// </summary>
		public float[] Figures;
		/// <summary>
		/// 路点关联路点的id
		/// </summary>
		public int[] ConnectedIds;
		/// <summary>
		/// 路点:安全值和危险值，共4个。前两个是teamA的，后面2个是teamB的
		/// 防守点：波数。
		/// 攻防战中心:波数，队伍，区域。
		/// 攻防战中间点:波数。
		/// </summary>
		public int[] WayPointFigures;
	}
}