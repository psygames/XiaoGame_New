using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfire.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(InputField), true)]
    public class InputFieldEditor : UnityEditor.UI.InputFieldEditor
    {
    }
}
