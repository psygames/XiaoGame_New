using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TablePage
	{
		public TablePage() { }
		public TablePage(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.title = (string)dict["title"];
			this.path = (string)dict["path"];
			this.prefab = (string)dict["prefab"];
			this.prefabName = (string)dict["prefabName"];
			this.atlasName = (string)dict["atlasName"];
		}

		/// <summary>
		/// 描述
		/// </summary>
		public int id;
		/// <summary>
		/// 页面名称
		/// </summary>
		public string name;
		/// <summary>
		/// 页面标题
		/// </summary>
		public string title;
		/// <summary>
		/// 页面路径
		/// </summary>
		public string path;
		/// <summary>
		/// 页面资源
		/// </summary>
		public string prefab;
		/// <summary>
		/// prefab Name 绑定 ctrl 使用
		/// </summary>
		public string prefabName;
		/// <summary>
		/// 图集名
		/// </summary>
		public string atlasName;
	}
}