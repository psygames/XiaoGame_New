using System;
using UnityEngine;

namespace Hotfire.UI
{
    static class UIUtils
    {
        public static void DispatchEvent(this Component sender, string eventId, params object[] args)
        {
            if (!String.IsNullOrEmpty(eventId))
            {
                IEventHandler handler = sender.GetComponentInParent<IEventHandler>();
                if (handler != null)
                    handler.OnEvent(eventId, args);
            }
        }
        public static void SetParentAndAlign(GameObject child, GameObject parent)
        {
            if (parent == null)
                return;

            child.transform.SetParent(parent.transform, false);
            SetLayerRecursively(child, parent.layer);
        }

        public static void SetLayerRecursively(GameObject go, int layer)
        {
            go.layer = layer;
            Transform t = go.transform;
            for (int i = 0; i < t.childCount; i++)
                SetLayerRecursively(t.GetChild(i).gameObject, layer);
        }

        public static void ResetRectTrasform(GameObject go)
        {
            RectTransform rectTransform = go.transform as RectTransform;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
        }
    }
}
