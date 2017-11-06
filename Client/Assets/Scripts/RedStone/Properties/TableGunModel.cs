using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableGunModel
	{
		public TableGunModel() { }
		public TableGunModel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.gunName = (string)dict["gunName"];
			this.gunID = (int)dict["gunID"];
			this.promote = (int)dict["promote"];
			this.battleModelPath = (string)dict["battleModelPath"];
			this.modelPath = (string)dict["modelPath"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// gunName
		/// </summary>
		public string gunName;
		/// <summary>
		/// gunID
		/// </summary>
		public int gunID;
		/// <summary>
		/// promote
		/// </summary>
		public int promote;
		/// <summary>
		/// modelPath
		/// </summary>
		public string battleModelPath;
		/// <summary>
		/// modelPath
		/// </summary>
		public string modelPath;
	}
}