using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteHolder : MonoBehaviour {

	public string atlasName;
    public Sprite[] allSprites;

	private uint textureSize = 0;

	public uint GetTextureSize()
	{
		if (textureSize != 0)
			return textureSize;
		if (allSprites.Length == 0)
			return 0;
		List<string> textureList = new List<string>();
		for (int i = 0; i < allSprites.Length; ++i) 
		{
			if (allSprites [i] == null)
				continue;
			Texture2D texture = allSprites [i].texture as Texture2D;
			if (texture == null)
				continue;
			if (textureList.IndexOf (texture.name) == -1)
			{
				textureList.Add (texture.name);

				#if UNITY_5_4
				var memorySize = (uint)(UnityEngine.Profiler.GetRuntimeMemorySize (texture) / 4); // * 8 / 32
				#else
				var memorySize = (uint)(UnityEngine.Profiling.Profiler.GetRuntimeMemorySize (texture) / 4); // * 8 / 32
				#endif
				if (memorySize <= 0)
				{
					if (texture.format != TextureFormat.ARGB32 || texture.format != TextureFormat.RGBA32)
						textureSize += (uint)texture.width * (uint)texture.height / 4;
					else
						textureSize += (uint)texture.width * (uint)texture.height;
				} else
				{
					textureSize += memorySize;
				}
			}
		}
		textureList.Clear ();
		return textureSize;
	}
}
