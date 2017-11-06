#if UNITY_EDITOR
using System;
using System.Collections;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class EditTool : MonoBehaviour
{
	public bool showSketch;
	[HideInInspector]public string prefabPath;
	[HideInInspector]public string sketchPath;
	[HideInInspector]public Rect rect = new Rect(0,0,1280, 720);
	public Color color = new Color(1f, 1f, 1f, 0.2f);
	[HideInInspector]public Texture2D texture;
	// Use this for initialization
	void Start ()
	{
		if (string.IsNullOrEmpty (prefabPath))
			prefabPath = "/" + gameObject.name;
		int index = prefabPath.LastIndexOf ("/");
		sketchPath = "../Documentation/Design/UI/Fakes" + prefabPath.Substring (index, prefabPath.Length - index) + ".png";
	}

	void LoadTexture()
	{

		if (File.Exists (sketchPath)) {
			var stream = File.OpenRead (sketchPath);
			byte[] data = new byte[stream.Length];
			stream.Read (data, 0, data.Length);
			texture = new Texture2D (1280, 720);
			texture.LoadImage (data);
			data = null;
			stream.Close ();
		} else {
			texture = Texture2D.whiteTexture;
		}
	}
	void OnGUI()
	{
		if (!showSketch) 
			return;
		if (texture == null)
			LoadTexture ();
		rect.width = Screen.width;
		rect.height = Screen.height;
		GUI.color = color;
		GUI.DrawTexture (rect, texture);
	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnDisable()
	{
		texture = null;
	}
}
#endif
