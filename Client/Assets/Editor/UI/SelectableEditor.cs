using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.AnimatedValues;
using UnityEditor;

 namespace RedStone.UI
{
    [CustomEditor(typeof(Selectable), true)]
    public class SelectableEditor : UnityEditor.UI.SelectableEditor
    {
    }
}
