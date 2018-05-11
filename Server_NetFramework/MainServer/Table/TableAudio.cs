using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableAudio
	{
		public TableAudio() { }
		public TableAudio(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.path = (string)dict["path"];
			this.volume = (float)dict["volume"];
			this.pitch = (float)dict["pitch"];
			this.type = (int)dict["type"];
			this.minDistance = (float)dict["minDistance"];
			this.maxDistance = (float)dict["maxDistance"];
			this.loop = (int)dict["loop"];
			this.prioity = (int)dict["prioity"];
			this.timeLength = (float)dict["timeLength"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 资源路径
		/// </summary>
		public string path;
		/// <summary>
		/// 音量0-1
		/// </summary>
		public float volume;
		/// <summary>
		/// 音调
		/// </summary>
		public float pitch;
		/// <summary>
		/// 1为2d,2为3d
		/// </summary>
		public int type;
		/// <summary>
		/// 3d音量开始衰减距离
		/// </summary>
		public float minDistance;
		/// <summary>
		/// 3d音量结束衰减距离
		/// </summary>
		public float maxDistance;
		/// <summary>
		/// 1循环模式，2播放1次停止
		/// </summary>
		public int loop;
		/// <summary>
		/// 基础权重，代表一个音效的重要程度0-255，依次增大，默认128普通重要性
		/// </summary>
		public int prioity;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public float timeLength;
	}
}