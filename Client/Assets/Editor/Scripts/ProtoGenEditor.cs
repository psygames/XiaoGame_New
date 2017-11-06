using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;
using System;

namespace RedStone
{
	public class ProtoGenEditor : Editor
	{
		[MenuItem(EditorConfig.PROTO_GEN)]
		public static void ProtoGen()
		{
			string progressTitle = "Generator";
			string progressInfo = "gen cs files by protobuf";

			EditorUtility.DisplayProgressBar(progressTitle, progressInfo, 0);

			EditorUtility.DisplayProgressBar(progressTitle, progressInfo, 1);
			EditorUtility.ClearProgressBar();
		}
	}
}
