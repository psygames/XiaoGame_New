using UnityEngine;
using Coolfish.System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class FileHelper
{
    public static void CreateFileDirectory(string path)
    {
        if (!IsDirectoryExists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public static bool IsDirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public static bool FileExists(string path)
    {
        return File.Exists(path);
    }

	public static string GetPersistPath(string path, bool isFileLoad = false)
	{
        return null;
	}
	public static void WritePersistTextFile(string path, string text)
	{
		WriteTextFile (GetPersistPath (path), text);
	}
    public static void WriteTextFile(string path, string text)
    {
		var dir = path.Substring(0, path.LastIndexOf('/') + 1);
		if(!Directory.Exists(dir))
			Directory.CreateDirectory (dir);
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        using (StreamWriter sw = File.CreateText(path))
        {
            sw.Write(text);
        }
    }

	public static void WritePersistByteFile(string path, byte[] b)
	{
		WriteByteFile (GetPersistPath(path), b);
	}
    public static void WriteByteFile(string path, byte[] b)
    {
		var dir = path.Substring(0, path.LastIndexOf('/') + 1);
		if(!Directory.Exists(dir))
			Directory.CreateDirectory (dir);
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
        {
            fs.Write(b, 0, b.Length);
        }
    }
	public static string ReadPersistTextFile(string path)
	{
		return ReadTextFile (GetPersistPath(path));
	}
    public static string ReadTextFile(string path)
    {
        string s = "";

        if (File.Exists(path))
        {
            using (StreamReader sr = File.OpenText(path))
            {
                s = sr.ReadToEnd();
            }
        }

        return s;
    }

    /// <summary>
    /// 清空指定的文件夹，但不删除文件夹
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteFolder(string dir)
    {
        foreach (string d in Directory.GetFileSystemEntries(dir))
        {
            if (File.Exists(d))
            {
                FileInfo fi = new FileInfo(d);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;
                File.Delete(d);//直接删除其中的文件  
            }
            else
            {
                DirectoryInfo d1 = new DirectoryInfo(d);

                if (d1.GetFiles().Length != 0)
                {
                    DeleteFolder(d1.FullName);////递归删除子文件夹
                }

                if (FileExists(d + ".meta"))
                {
                    File.Delete(d + ".meta");
                }
                Directory.Delete(d);
            }
        }
    }

    /// <summary>
    /// 删除文件夹下及其内容
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteFolderAndFile(string dir)
    {
        foreach (string d in Directory.GetFileSystemEntries(dir))
        {
            string d1 = d.Replace("\\", "/");

            if (File.Exists(d1))
            {
                FileInfo fi = new FileInfo(d1);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;
                File.Delete(d1);//直接删除其中的文件  
            }
            else
            {
                DeleteFolder(d1);////递归删除子文件夹
                Directory.Delete(d1);
            }
        }
    }

    /// <summary>
    /// 给出当前目录下(包括子文件夹)的所有文件
    /// </summary>
    /// <param name="info"></param>
    public static List<string> ListFiles(FileSystemInfo info,bool recursive = true)
    {
        List<string> list = new List<string>();

        if (!(info.Exists && info is DirectoryInfo))
            return null;

        internalListFiles(info, list, recursive);

        return list;
    }
    private static void internalListFiles(FileSystemInfo info, List<string> list, bool recursive = true)
    {
        DirectoryInfo dir = info as DirectoryInfo;
        if (dir == null)
            return;
        FileSystemInfo[] files = dir.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i] as FileInfo;
            //是文件   
            if (file != null && !file.FullName.Contains(".meta"))
            {
                //Debug.Log(file.FullName + "\t " + file.Length);
                list.Add(file.FullName);
            }

            //对于子目录，进行递归调用   
            else if(recursive)
                internalListFiles(files[i], list);
        }
    }

    public static System.Xml.XmlDocument GetXmlFromDisk(string tableName)
    {
        string replaceTag = "-rplc-";
        string tableDir = Application.dataPath + "/../../Documentation/XML/";
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        //Debug.Log(tableDir + tableName + ".xml");
        string text = ReadTextFile(tableDir + tableName + ".xml");
        text = text.Replace(":", replaceTag);
        doc.LoadXml(text);
        return doc;
    }

    public static void WriteXmlToDisk(System.Xml.XmlDocument doc, string docName)
    {
        string replaceTag = "-rplc-";
        MemoryStream stream = new MemoryStream();
        doc.Save(stream);
        StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
        reader.BaseStream.Position = 0;
        string xmlString = reader.ReadToEnd();
        xmlString = xmlString.Replace(replaceTag, ":");
        stream.Close();
        reader.Close();
        string tableDir = Application.dataPath + "/../../Documentation/XML/";
        string path = tableDir + docName + ".xml";
        if (File.Exists(path))
            File.Delete(path);
        StreamWriter writer = File.CreateText(path);
        writer.Write(xmlString);
        writer.Flush();
        writer.Close();
    }
}
