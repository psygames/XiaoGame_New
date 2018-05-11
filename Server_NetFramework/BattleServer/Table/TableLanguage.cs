using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableLanguage
	{
		public TableLanguage() { }
		public TableLanguage(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.text = (string)dict["text"];
			this.fileName = (string)dict["fileName"];
			this.fontStyleName = (string)dict["fontStyleName"];
			this.fontName = (string)dict["fontName"];
			this.fontBoldName = (string)dict["fontBoldName"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 语言名称
		/// </summary>
		public string name;
		/// <summary>
		/// 显示文本
		/// </summary>
		public string text;
		/// <summary>
		/// 语言文件名
		/// </summary>
		public string fileName;
		/// <summary>
		/// 语言文本样式名
		/// </summary>
		public string fontStyleName;
		/// <summary>
		/// 字体名
		/// </summary>
		public string fontName;
		/// <summary>
		/// 加粗字体名
		/// </summary>
		public string fontBoldName;
	}
}