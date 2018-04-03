using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using RedStone;

[CustomEditor(typeof(GameManager), true)]
public class GameManagerEditor : UnityEditor.Editor
{
    const string PREFS_SELECTED_CUSTOM_ADDRESS = "EditorSelectedCustomAddress";

    [System.Serializable]
    class DataList
    {
        public string[] serverIPs;
        public string[] serverIPDescriptions;
    }

    DataList dataList;
    bool currentIP = false;
    int m_selected = 0;
    string uuidSuffix = "";
    string serverAddress = "";

    void Awake()
    {
        dataList = LocalDataUtil.FromJson<DataList>(File.ReadAllText(EditorConfig.CONFIG_PATH + "GameManagerToolConfig.json"));
    }

    void SaveFile()
    {
        File.WriteAllText(EditorConfig.CONFIG_PATH + "GameManagerToolConfig.json", LocalDataUtil.ToJson(dataList, true));
    }


    string tempName = "";
    string tempValue = "";
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        var rect = EditorGUILayout.GetControlRect();

        /*----------- uuid suffix -------------*/
        uuidSuffix = LocalDataUtil.Get(GameManager.PREFS_UUID_SUFFIX, "");
        uuidSuffix = EditorGUILayout.TextField("UUID SUFFIX", uuidSuffix);
        LocalDataUtil.Set(GameManager.PREFS_UUID_SUFFIX, uuidSuffix);


        /*----------- custom ip -----------------*/
        m_selected = LocalDataUtil.Get(PREFS_SELECTED_CUSTOM_ADDRESS, (int)0);
        rect = EditorGUILayout.GetControlRect();
        rect.width -= 40f;
        m_selected = EditorGUI.Popup(rect, "Main Server Address", m_selected, dataList.serverIPDescriptions);
        LocalDataUtil.Set(PREFS_SELECTED_CUSTOM_ADDRESS, m_selected);
        serverAddress = dataList.serverIPs[m_selected];
        LocalDataUtil.Set(GameManager.PREFS_SERVER_ADDRESS, serverAddress);


        rect.x += rect.width;
        rect.width = 20f;
        if (GUI.Button(rect, "+"))
        {
            currentIP = !currentIP;
            tempName = "";
            tempValue = "";
        }
        rect.x += 20f;
        if (GUI.Button(rect, "-"))
        {
            if (m_selected > 0 && m_selected < dataList.serverIPs.Length)
            {
                var ips = dataList.serverIPs.ToListFromPool();
                var names = dataList.serverIPDescriptions.ToListFromPool();
                ips.RemoveAt(m_selected);
                names.RemoveAt(m_selected);
                dataList.serverIPs = ips.ToArray();
                dataList.serverIPDescriptions = names.ToArray();
                ips.ReleaseToPool();
                names.ReleaseToPool();
                m_selected = 0;
                LocalDataUtil.Set(PREFS_SELECTED_CUSTOM_ADDRESS, m_selected);
                SaveFile();
            }
        }

        if (currentIP)
        {
            tempName = EditorGUILayout.TextField("New Name", tempName).Trim();
            tempValue = EditorGUILayout.TextField("New IP", tempValue).Trim();
            rect = EditorGUILayout.GetControlRect();
            if (GUI.Button(rect, "save"))
            {
                currentIP = false;
                var newips = dataList.serverIPs.ToListFromPool();
                if (!newips.Contains(tempValue))
                {
                    var newnames = dataList.serverIPDescriptions.ToListFromPool();
                    newnames.Add(tempName);
                    newips.Add(tempValue);
                    dataList.serverIPs = newips.ToArray();
                    dataList.serverIPDescriptions = newnames.ToArray();
                    newnames.ReleaseToPool();
                    SaveFile();
                }
                newips.ReleaseToPool();
            }
        }
    }
}

