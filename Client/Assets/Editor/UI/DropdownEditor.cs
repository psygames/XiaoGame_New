using UnityEditor;
using UnityEngine.UI;

 namespace RedStone.UI
{
    [CustomEditor(typeof(Dropdown), true)]
    [CanEditMultipleObjects]
    public class DropdownEditor : UnityEditor.UI.DropdownEditor
    {
    }
}
