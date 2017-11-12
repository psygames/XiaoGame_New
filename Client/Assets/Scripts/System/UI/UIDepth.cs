using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//用来解决UI与3d特效之间的层叠，渲染顺序问题
[AddComponentMenu("uTools/UIDepth")]
public class UIDepth : MonoBehaviour
{
    public int depth;
    public bool isUI = true;
    // Use this for initialization
    void Start()
    {
        RefreshDepth();
    }
    public void RefreshDepth()
    {
        if (isUI)
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas == null)
                canvas = gameObject.AddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = depth;
        }
        else
        {
            Renderer[] renders = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renders.Length; i++)
                renders[i].sortingOrder = depth;
        }
    }
    public void SetDepth(int depth)
    {
        this.depth = depth;
        RefreshDepth();
    }

}