using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 
using System.Linq;

 namespace RedStone.UI
{
    // TODO REVIEW
    // Have material live under text
    // move stencil mask into effects *make an efects top level element like there is
    // paragraph and character

    /// <summary>
    /// Editor class used to edit UI Labels.
    /// </summary>

    [CustomEditor(typeof(Text), true)]
    [CanEditMultipleObjects]
    public class TextEditor : UnityEditor.UI.TextEditor
    {
        SerializedProperty m_TextKey;
        SerializedProperty m_Text;
        SerializedProperty m_TextStyle;
        SerializedProperty m_TextColor;

        bool isAddKey = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_TextKey = serializedObject.FindProperty("m_TextKey");
            m_Text = serializedObject.FindProperty("m_Text");
            m_TextStyle = serializedObject.FindProperty("m_TextStyle");
            m_TextColor = serializedObject.FindProperty("m_TextColor");

            if (Localization.instance == null)
                Localization.CreateAndLoad();
        }

        Text GetText(Text text, string key)
        {
            m_Text.stringValue = LT.Get(key, text.LTParent);
            return text;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Text text = target as Text;
            if (text == null)
                return;

            EditorGUILayout.PropertyField(m_TextKey);
            m_TextKey.stringValue = m_TextKey.stringValue;

            // KEY
            if (GUILayout.Button("Add Key"))
            {
                if (!isAddKey)
                {
                    isAddKey = true;
                }
                else
                {
                    OnKeyAdded();
                }
            }
            if (isAddKey)
            {
                AddKey(text);
            }

            if (GUILayout.Button("Translate Text"))
            {
                GetText(text, text.textKey);
            }

            // FONT_COLOR
            DrawFontColor();

            // FONT_COLOR_SELECTION
            DrawFontColorSelection();

            if (text)
            {
                text.spriteYOffset = EditorGUILayout.FloatField("image offset", text.spriteYOffset);
            }
            // FONT_STYLE
            EditorGUI.BeginChangeCheck();
            var allStyles = Localization.instance.GetAllFontStyles().ToListFromPool();
            var styleList = new string[allStyles.Count];
            int selectIndex = 0;
            int index = 0;
            foreach (var item in allStyles)
            {
                styleList[index] = item.name;
                if (text.textStyle == item.id)
                    selectIndex = index;
                index++;
            }
            selectIndex = EditorGUILayout.Popup("Text Style", selectIndex, styleList);
            if (EditorGUI.EndChangeCheck())
            {
                text.textStyle = allStyles[selectIndex].id;
                text.name = text.name; //TODO:什么鬼！要赋值一遍Name，Scene才会刷新，？？？ 
            }
            allStyles.ReleaseToPool();

            text.multiLine = EditorGUILayout.Toggle("Multiple Line", text.multiLine);
            if (GUILayout.Button("Set Best Fit Text Style"))
            {
                text.textStyle = Localization.instance.GetFitTextStyle(text);
                text.name = text.name;
            }
            //TODO: 不清楚该功能，暂时屏蔽。
            /*
            if (GUILayout.Button("Fit Text Size"))
            {
                Localization.instance.SetTextStyle(text, true);
                text.name = text.name;
            }
            */
            if (GUILayout.Button("Set Best RectTrans Size"))
            {
                text.rectTransform.sizeDelta = new Vector2((int)(text.preferredWidth + 1), (int)(text.preferredHeight + 1));
            }

            serializedObject.ApplyModifiedProperties(); // Apply Text Properties

            base.OnInspectorGUI();
            EditorGUILayout.Space();
            text.LTParent = (MonoBehaviour)(EditorGUILayout.ObjectField("Translate Parent", text.LTParent, typeof(MonoBehaviour), true));

            var outline = text.GetComponent<UnityEngine.UI.Outline>();
            if (GUILayout.Button(outline != null ? "RemoveOutline" : "Add Outline"))
            {
                if (outline != null)
                {
                    GameObject.DestroyImmediate(outline);
                }
                else
                    text.gameObject.AddComponent<UnityEngine.UI.Outline>();
            }
            EditorGUILayout.Space();

        }

        private void DrawFontColorSelection()
        {
            if (m_isOpenTextColorSelection)
            {
                int count = 0;
                var tables = TableManager.instance.GetAllData<TableTextColor>().Values;
                var list = tables.ToListFromPool();
                list.Sort((a, b) => { return a.order.CompareTo(b.order); });
                foreach (var table in list)
                {
                    Rect rect = new Rect(m_textColorSelectionPos);
                    rect.y += EditorGUIUtility.singleLineHeight * (count + 1) + 2;
                    rect.height -= 2;
                    if (GUI.Button(rect, ""))
                    {
                        m_TextColor.intValue = table.id;
                        if (table.id != UIConfig.textColorNoneID)
                            m_Color.colorValue = table.value;
                        m_isOpenTextColorSelection = false;
                    }
                    EditorGUI.DrawRect(rect, table.value);
                    GUIStyle stl = new GUIStyle();
                    stl.alignment = TextAnchor.MiddleCenter;
                    stl.normal.textColor = UIHelper.SimpleColorInverse(table.value);
                    EditorGUI.LabelField(rect, table.name, stl);
                    Rect rect2 = new Rect(rect);
                    rect2.x -= 85;
                    rect2.width = 80;
                    EditorGUI.LabelField(rect2, UIHelper.FormatColorToHexStr(table.value));
                    GUILayout.Space(EditorGUIUtility.singleLineHeight);
                    count++;
                }
                list.ReleaseToPool();
                EditorGUILayout.Separator();
            }
        }

        private void DrawFontColor()
        {
            Rect __rect = EditorGUILayout.GetControlRect();

            // label
            Rect r1 = new Rect(__rect);
            r1.width = EditorGUIUtility.labelWidth;
            EditorGUI.LabelField(r1, "Text Color");

            // selector
            Rect r2 = new Rect(__rect);
            r2.x += r1.width;
            r2.width -= r1.width + 40;
            m_textColorSelectionPos = r2;
            if (GUI.Button(r2, ""))
            {
                m_isOpenTextColorSelection = !m_isOpenTextColorSelection;
            }

            // capture color
            EditorGUI.BeginChangeCheck();
            Rect r3 = new Rect(__rect);
            r3.height += 2f;
            r3.y -= 1f;
            Color color = EditorGUI.ColorField(r3, new GUIContent("  "), Color.white, true, false, false, null);
            if (EditorGUI.EndChangeCheck())
            {
                int typeID = UIHelper.GetNearestColorType(color);
                m_TextColor.intValue = typeID;
                if (typeID != UIConfig.textColorNoneID)
                {
                    var tab = TableManager.instance.GetData<TableTextColor>(typeID);
                    m_Color.colorValue = tab.value;
                }
                else
                {
                    m_Color.colorValue = color;
                }
            }

            // capture outline
            Rect r4 = new Rect(__rect);
            r4.x += __rect.width - 41;
            r4.width = 2;
            EditorGUI.DrawRect(r4, Color.black);


            // selector view
            EditorGUI.DrawRect(r2, m_Color.colorValue);
            var tableColor = TableManager.instance.GetData<TableTextColor>(m_TextColor.intValue);
            string colorName = tableColor.name;
            GUIStyle stl = new GUIStyle();
            stl.alignment = TextAnchor.MiddleCenter;
            stl.normal.textColor = UIHelper.SimpleColorInverse(m_Color.colorValue);
            EditorGUI.LabelField(r2, colorName, stl);
        }

        private bool m_isOpenTextColorSelection = false;
        private Rect m_textColorSelectionPos;
        private string currentKey = "";
        private Localization.TextTree tree = null;
        private void OnKeyAdded()
        {
            currentKey = "";
            tree = null;
            isAddKey = false;


        }
        private void AddKey(Text text)
        {
            EditorGUILayout.BeginVertical("box");
            if (!string.IsNullOrEmpty(currentKey))
            {

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(currentKey);
                EditorGUILayout.EndVertical();
            }
            if (tree == null)
                tree = Localization.instance.textTree;
            foreach (var child in tree.children)
            {
                if (!string.IsNullOrEmpty(child.Value.text))
                {
                    if (GUILayout.Button(child.Value.text))
                    {
                        m_TextKey.stringValue += "{" + "s:{0}".FormatStr(child.Value.text) + "}";
                        GetText(text, m_Text.stringValue);
                        OnKeyAdded();
                    }
                }
                if (!string.IsNullOrEmpty(child.Value.path))
                {
                    var rect = EditorGUILayout.GetControlRect();
                    rect.width -= 20f;
                    EditorGUI.LabelField(rect, child.Value.path);
                    rect.x += rect.width;
                    rect.width = 20f;

                    if (GUI.Button(rect, ">"))
                    {
                        currentKey += child.Value.path + "_";
                        tree = child.Value;
                        break;
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }

    }
}
