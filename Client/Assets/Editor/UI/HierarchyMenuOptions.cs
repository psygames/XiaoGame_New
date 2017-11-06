using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
class HierarchyMenuOptions
{
    [InitializeOnLoadMethod]
    static void StartInitializeOnLoadMethod()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        EditorApplication.hierarchyWindowChanged += OnHierarchyWindowChanged;
    }

    static void OnHierarchyWindowChanged()
    {

    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        if (Event.current != null && selectionRect.Contains(Event.current.mousePosition)
            && Event.current.button == 1 && Event.current.type == EventType.mouseUp)
        {
            GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (selectedGameObject && selectedGameObject.GetComponent<RectTransform>() != null)
            {
                Vector2 mousePosition = Event.current.mousePosition;
                Rect popRect = new Rect(mousePosition.x, mousePosition.y, 0, 0);
                EditorUtility.DisplayPopupMenu(popRect, "GameObject/Project", null);
                Event.current.Use();
            }
        }
    }
}
