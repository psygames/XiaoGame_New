using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using Object = UnityEngine.Object;
using System.Threading;
 
using RedStone.UI;
using RedStone;
using uTools;

public class Logger : MonoBehaviour
{
	public Image showLogButton;
	public Transform transformParent;
	public Transform logPanel;
	public Text textLog;
	List<string> exceptionList = new List<string> ();
	static Logger instance;

	public enum ELogLevel
	{
		Error = 1,
		Warning = 2,
		Debug = 4,
		Statistics = 8,
		Bytes = 16,
		AssetBundle = 32,
		Network = 64,
		Task = 128,
		Profiler = 256,
	}
	private static MpscLinkedQueue<string> m_fileQueue = new MpscLinkedQueue<string> ();
	private static string outpath = "../../ClientLog.txt";

    public static bool isPrinToScreen = true;
	public static bool outputToFile = false;
	private static StreamWriter stream = null;

	void Start()
	{
		instance = this;
		UUIEventListener.Get (showLogButton.gameObject).onClick = _ => logPanel.gameObject.SetActive (!logPanel.gameObject.activeSelf);
		DontDestroyOnLoad (transformParent.gameObject);
	}
	void OnDestroy()
	{
		if (stream != null) {
			stream.Close ();
			stream = null;
		}
	}
	static StringBuilder byteBuilder = new StringBuilder();
	public static void LogBytes(object obj, string content, byte[] b, int length)
	{
		byteBuilder.Length = 0;
		byteBuilder.Append (length);
		byteBuilder.Append (":");
		for (int i = 0; i < length; ++i) {
			byteBuilder.Append (b [i].ToString ());
			byteBuilder.Append (",");
		}
		LogLevel (ELogLevel.Bytes, obj, content, byteBuilder.ToString ());
	}

	public static void Log(object obj, string content, params object[] param)
	{
		LogLevel(ELogLevel.Debug, obj, content, param);
	}


	public static void LogWarning(object obj, string content, params object[] param)
	{
		LogLevel(ELogLevel.Warning, obj, content, param);
	}

	public static void LogError(object obj, string content, params object[] param)
	{
		LogLevel(ELogLevel.Error, obj, content, param);
	}
	void Update()
	{
		if (outputToFile && stream == null)
		{
			if (File.Exists (outpath)) {
				File.Delete (outpath);
			}
			stream = new StreamWriter (File.Create (outpath));
		}
		if (outputToFile && stream != null && m_fileQueue.Count > 0) {
			stream.WriteLine (m_fileQueue.Dequeue ());
			stream.Flush ();
		}
	}

	public static void LogException(string str, bool refreshUI)
	{
		if (instance == null)
			return;
		instance._LogException (str, refreshUI);
	}

	private void _LogException(string str, bool refreshUI)
	{
		exceptionList.Add (str);
		if (refreshUI)
		{
			_RefreshException ();
		}
	}
	public static void RefreshException()
	{
		if (instance == null)
			return;
		instance._RefreshException ();
	}
	private void _RefreshException ()
	{
		if (!transformParent.gameObject.activeSelf)
			transformParent.gameObject.SetActive (true);
		uTweenScale.Begin (this.showLogButton.gameObject, Vector3.one * 1.5f, Vector3.one, 0.5f);
		StringBuilder builder = StringTools.GetStringBuilderFromPool ();
		for (int i = exceptionList.Count - 1; i >= 0; --i)
		{
			var text = exceptionList [i];
			builder.Append (text).Append ("\n\n");
		}
		textLog.text = builder.ToString ();

		StringTools.ReleaseStringBuilder (builder);
	}

	public static void LogLevel(ELogLevel logType, object obj, string content, params object[] param)
    {
		var stringBuilder = StringTools.GetStringBuilderFromPool ();
		stringBuilder.AppendFormat("[{0}]", Enum.GetName(typeof(ELogLevel), logType));
		if(obj != null)
		{
			stringBuilder.AppendFormat ("[<color=darkblue>{0}</color>]", obj.ToString());
		}
		stringBuilder.AppendFormat ("[<color=#990000>{0}.{1}</color>]"
            , "{0:HH:mm:ss}".FormatStr(DateTime.Now)
            , DateTime.Now.Millisecond.ToString());
		if (param == null || param.Length == 0)
			stringBuilder.Append (content);
		else
			stringBuilder.AppendFormat (content, param);
		var outputStr = stringBuilder.ToString ();
		if (outputToFile)
		{
			m_fileQueue.Enqueue (outputStr);
		}
		if (isPrinToScreen)
		{
			if ((logType & ELogLevel.Error) != 0)
				Debug.LogError (outputStr);
			else
			if ((logType & ELogLevel.Warning) != 0)
					Debug.LogWarning (outputStr);
			else
					Debug.Log (outputStr);
		}
		StringTools.ReleaseStringBuilder (stringBuilder);
	}

}