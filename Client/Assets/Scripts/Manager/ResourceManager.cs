using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
 
using UnityEngine;

 namespace RedStone
{
    public class AsyncResource
    {
        public bool isAssetBundleDone = true;

        public string assetName = "";
        public AsyncResource(string assetName, ResourceRequest op, System.Object param)
        {
            asyncOperation = op;
            this.param = param;

        }

        private ResourceRequest asyncOperation;

        public ResourceRequest AsyncOp
        {
            get
            {
                return asyncOperation;
            }
            set
            {
                if (asyncOperation == null)
                    asyncOperation = value;
                else
                {
                    throw new InvalidOperationException("asyncOp Already Has A Value");
                }
            }
        }
        public bool isLoadAssetBundle = false;
        public System.Object param;
        public UnityEngine.Object loadedAsset;
        public UnityEngine.Object[] allLoadedAssets;
        public AssetBundle ab;
    }

    public class SingleAsyncRes
    {
        public Action<AsyncResource> m_callBack;
        public AsyncResource m_res;

        public SingleAsyncRes(Action<AsyncResource> callBack, AsyncResource res)
        {
            m_callBack = callBack;
            m_res = res;
        }
    }

    public class GroupAsyncRes
    {
        public Action<Dictionary<string, AsyncResource>> m_callBack;
        public Dictionary<string, AsyncResource> m_res;

        public GroupAsyncRes(Action<Dictionary<string, AsyncResource>> callBack, Dictionary<string, AsyncResource> res)
        {
            m_callBack = callBack;
            m_res = res;
        }
    }

    public class ResourceManager
    {
        private static ResourceManager s_instance;
        public bool forceUseResources = false;
        public static ResourceManager instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new ResourceManager();
                return s_instance;
            }
        }

        private List<GroupAsyncRes> m_asyncGroupLoadingRes = new List<GroupAsyncRes>();
        private List<SingleAsyncRes> m_asyncSingleLoadingRes = new List<SingleAsyncRes>();

        public const string EDITOR_RESOURCE_PREFIX = "Assets/AssetbundleResources/";
        public const string PRELOAD_PATH = "Preload";
        public const string PRELOAD_BUNDLE_FILE = "Preload/Preload";
        public const string PRELOAD_RULE_FILE = "PreloadRules";
        public const string ASSET_BUNDLE_FOLDER_INFO_FILE = "AssetBundleFolderInfo";
        private ResourceManager()
        {

        }

        public bool IsInited()
        {
            return false;
        }

        public UnityEngine.Object GetResourceByPath(string path)
        {
            return Resources.Load(path);
        }

        public void ReleaseMemory()
        {
            UnloadAllResource();
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        public void UnloadResouce(string url, bool unloadAsset = false, bool forceUnloadAll = false)
        {
        }

        public void UnloadAllResource()
        {

        }

        private static void LoadAssetBundleAsync(string resPath, string assetName, AsyncResource ar, bool loadAll = false)
        {
            ar.isAssetBundleDone = false;
            ar.isLoadAssetBundle = true;
        }

        public void GetResourceByPathAsync(List<string> path, Action<Dictionary<string, AsyncResource>> callBack = null, System.Object param = null)
        {
            if (path.Count == 0
                    || (path.Count == 1 && path[0] == "param"))
            {
                if (callBack != null)
                {
                    Dictionary<string, AsyncResource> nullMap = new Dictionary<string, AsyncResource>();
                    nullMap.Add("param", new AsyncResource("param", null, param));
                    callBack(nullMap);
                }
                return;
            }
            Dictionary<string, AsyncResource> item = new Dictionary<string, AsyncResource>();
            foreach (string resPath in path)
            {
                if (item.ContainsKey(resPath))
                {
                    continue;
                }

                var ar = new AsyncResource(resPath,null,param);
                ar.loadedAsset = Resources.Load(resPath);
                item.Add(resPath, ar);
            }
            callBack(item);
        }

        AsyncResource CreateAsyncResource(string resPath, object param, bool loadAll = false)
        {
            string assetName = null;
            resPath = resPath.ToLower();
            AsyncResource ar = new AsyncResource(resPath, null, param);
            ar.AsyncOp = (string.IsNullOrEmpty(resPath) ? null : Resources.LoadAsync(resPath));
            return ar;
        }

        public void GetResourceByPathAsync(string path, Action<AsyncResource> callBack = null, System.Object param = null, bool loadAll = false)
        {
            var ar = new AsyncResource(path, null, param);
            ar.loadedAsset = Resources.Load(path);
            callBack.Invoke(ar);
        }

        public void Update()
        {
            //List<GroupAsyncRes> toDelete = new List<GroupAsyncRes>();

            for (int i = 0; i < m_asyncGroupLoadingRes.Count; ++i)
            {
                bool bDone = true;
                var enumerator = m_asyncGroupLoadingRes[i].m_res.Keys.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var key = enumerator.Current;
                    var resource = m_asyncGroupLoadingRes[i].m_res[key];
                    if (resource.isLoadAssetBundle && !resource.isAssetBundleDone)
                    {
                        bDone = false;
                        break;
                    }
                    else if (resource.AsyncOp != null && !resource.AsyncOp.isDone)
                    {
                        bDone = false;
                        break;
                    }
                    if (resource.AsyncOp != null)
                    {
                        resource.loadedAsset = m_asyncGroupLoadingRes[i].m_res[key].AsyncOp.asset;
                    }
                }
                if (bDone)
                {
                    try
                    {
                        if (m_asyncGroupLoadingRes[i].m_callBack != null)
                            m_asyncGroupLoadingRes[i].m_callBack(m_asyncGroupLoadingRes[i].m_res);
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogError(e.ToString());
                    }
                    m_asyncGroupLoadingRes.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < m_asyncSingleLoadingRes.Count; i++)
            {
                bool isDone = false;
                SingleAsyncRes sr = m_asyncSingleLoadingRes[i];
                if (sr.m_res.isLoadAssetBundle && sr.m_res.isAssetBundleDone)
                {
                    isDone = true;
                }
                else if (sr.m_res.AsyncOp == null || sr.m_res.AsyncOp.isDone)
                {
                    isDone = true;
                    sr.m_res.loadedAsset = (sr.m_res.AsyncOp == null ? null : sr.m_res.AsyncOp.asset);
                }
                if (isDone)
                {
                    if (sr.m_callBack != null)
                    {
                        try
                        {
                            sr.m_callBack(sr.m_res);
                        }
                        catch (Exception e)
                        {
                            UnityEngine.Debug.LogError(e.ToString());
                        }
                    }
                    m_asyncSingleLoadingRes.RemoveAt(i);
                    i--;
                }

            }
            //foreach (var item in toDelete)
            //{
            //    m_asyncGroupLoadingRes.Remove(item);
            //}
        }


        public bool IsLoading()
        {
            return m_asyncSingleLoadingRes.Count != 0 || m_asyncGroupLoadingRes.Count != 0;
        }

        public T GetResourceByPath<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public float preloadProgress
        {
            get
            {
                if (m_preloadCount == 0 || m_preloadCount <= m_curPreloaded)
                    return 1.01f;
                return (float)m_curPreloaded / (float)m_preloadCount;

            }
        }

        private int m_preloadCount = 0;
        private int m_curPreloaded = 0;
        private event Action<AsyncResource> m_onResourcePreloaded;
        public void RegisterPreloadHandler(Action<AsyncResource> handler)
        {
            if (handler != null)
                m_onResourcePreloaded += handler;
        }

        public void UnRegisterPreloadHandler(Action<AsyncResource> handler)
        {
            if (handler != null)
                m_onResourcePreloaded -= handler;
        }
        private void OnPreloadedResource(AsyncResource ar)
        {
            ++m_curPreloaded;
            if (m_onResourcePreloaded != null && ar != null && ar.loadedAsset != null)
                m_onResourcePreloaded.Invoke(ar);
        }

        public class ColumnType
        {
            public static int ResourceId = 0;
            public static int ResourcePath = 1;
            public static int BundlePath = 2;
        };
    }
}