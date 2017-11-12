using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

 namespace RedStone.UI
{
    /// <summary>
    /// This script adds the UI menu options to the Unity Editor.
    /// </summary>
    static public class MenuOptions
    {
        private const string kUILayerName = "UI";

        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        private const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        private const string kKnobPath = "UI/Skin/Knob.psd";
        private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";
        private const string kDropdownArrowPath = "UI/Skin/DropdownArrow.psd";
        private const string kMaskPath = "UI/Skin/UIMask.psd";

        static private DefaultControls.Resources s_StandardResources;

        static public DefaultControls.Resources GetStandardResources()
        {
            if (s_StandardResources.standard == null)
            {
                s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
                s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
                s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
                s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
                s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
                s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
                s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
            }
            return s_StandardResources;
        }

        private static void SetPositionVisibleinSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
        {
            // Find the best scene view
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView == null && SceneView.sceneViews.Count > 0)
                sceneView = SceneView.sceneViews[0] as SceneView;

            // Couldn't find a SceneView. Don't set position.
            if (sceneView == null || sceneView.camera == null)
                return;

            // Create world space Plane from canvas position.
            Vector2 localPlanePosition;
            Camera camera = sceneView.camera;
            Vector3 position = Vector3.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2), camera, out localPlanePosition))
            {
                // Adjust for canvas pivot
                localPlanePosition.x = localPlanePosition.x + canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
                localPlanePosition.y = localPlanePosition.y + canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;

                localPlanePosition.x = Mathf.Clamp(localPlanePosition.x, 0, canvasRTransform.sizeDelta.x);
                localPlanePosition.y = Mathf.Clamp(localPlanePosition.y, 0, canvasRTransform.sizeDelta.y);

                // Adjust for anchoring
                position.x = localPlanePosition.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
                position.y = localPlanePosition.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;

                Vector3 minLocalPosition;
                minLocalPosition.x = canvasRTransform.sizeDelta.x * (0 - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
                minLocalPosition.y = canvasRTransform.sizeDelta.y * (0 - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;

                Vector3 maxLocalPosition;
                maxLocalPosition.x = canvasRTransform.sizeDelta.x * (1 - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
                maxLocalPosition.y = canvasRTransform.sizeDelta.y * (1 - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;

                position.x = Mathf.Clamp(position.x, minLocalPosition.x, maxLocalPosition.x);
                position.y = Mathf.Clamp(position.y, minLocalPosition.y, maxLocalPosition.y);
            }

            itemTransform.anchoredPosition = position;
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.localScale = Vector3.one;
        }

        private static void PlaceUIElementRoot(GameObject element, MenuCommand menuCommand)
        {
            //GameObject parent = menuCommand.context as GameObject;
            GameObject parent = Selection.activeGameObject;
            if (parent == null || parent.GetComponentInParent<Canvas>() == null)
            {
                parent = GetOrCreateCanvasGameObject();
            }

            string uniqueName = GameObjectUtility.GetUniqueNameForSibling(parent.transform, element.name);
            element.name = uniqueName;
            Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);
            Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);
            GameObjectUtility.SetParentAndAlign(element, parent);
            // if (parent != menuCommand.context) // not a context click, so center in sceneview
            //    SetPositionVisibleinSceneView(parent.GetComponent<RectTransform>(), element.GetComponent<RectTransform>());
			element.layer = (int)ELayer.UI;
            Selection.activeGameObject = element;
        }

        // Graphic elements

		[MenuItem("GameObject/Project/UI Create Empty", false, 0)]
        static public void AddGameObject(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateGameObject(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

		[MenuItem("GameObject/Project/UI Line Renderer", false, 0)]
		static public void AddLine(MenuCommand menuCommand)
		{
			GameObject go = DefaultControls.CreateLine(GetStandardResources());
			PlaceUIElementRoot(go, menuCommand);
		}

        [MenuItem("GameObject/Project/UI Text", false, 0)]
        static public void AddText(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateText(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }
		
        [MenuItem("GameObject/Project/UI Image", false, 0)]
        static public void AddImage(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateImage(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/Project/UI Raw Image", false, 0)]
        static public void AddRawImage(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateRawImage(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

		[MenuItem("GameObject/Project/UI Clickable Item", false, 0)]
		static public void AddClickableItem(MenuCommand menuCommand)
		{
			GameObject go = DefaultControls.CreateClickableNonDrawingGraphics(GetStandardResources());
			PlaceUIElementRoot(go, menuCommand);
		}
        // Controls

        // Button and toggle are controls you just click on.

        [MenuItem("GameObject/Project/UI Button", false, 0)]
        static public void AddButton(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateButton(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/Project/UI Toggle", false, 0)]
        static public void AddToggle(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateToggle(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // Slider and Scrollbar modify a number

		[MenuItem("GameObject/Project/UI Simple Slider", false, 0)]
		static public void AddSimpleSlider(MenuCommand menuCommand)
		{
			GameObject go = DefaultControls.CreateSimpleSlider(GetStandardResources());
			PlaceUIElementRoot(go, menuCommand);
		}

        [MenuItem("GameObject/Project/UI Slider", false, 0)]
        static public void AddSlider(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateSlider(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }
		[MenuItem("GameObject/Project/UI Anim Progress Bar", false, 0)]
		static public void AddAnimProgressBar(MenuCommand menuCommand)
		{
			GameObject go = DefaultControls.CreateAnimProgressBar(GetStandardResources());
			PlaceUIElementRoot(go, menuCommand);
		}


        [MenuItem("GameObject/Project/UI Scrollbar")]
        static public void AddScrollbar(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateScrollbar(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // More advanced controls below

        [MenuItem("GameObject/Project/UI List View")]
        static public void AddListView(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateListView(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }
		[MenuItem("GameObject/Project/UI Flexible List View")]
		static public void AddFlexibleListView(MenuCommand menuCommand)
		{
			GameObject go = DefaultControls.CreateFlexibleListView(GetStandardResources());
			PlaceUIElementRoot(go, menuCommand);
		}
        [MenuItem("GameObject/Project/UI Dropdown")]
        static public void AddDropdown(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateDropdown(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/Project/UI Input Field")]
        public static void AddInputField(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateInputField(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // Containers

        [MenuItem("GameObject/Project/UI Root")]
        static public void AddCanvas(MenuCommand menuCommand)
        {
            var go = CreateNewUI();
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            if (go.transform.parent as RectTransform)
            {
                RectTransform rect = go.transform as RectTransform;
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = Vector2.zero;
            }
            Selection.activeGameObject = go;
        }

        [MenuItem("GameObject/Project/UI Panel")]
        static public void AddPanel(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreatePanel(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);

            // Panel is special, we need to ensure there's no padding after repositioning.
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;
        }

        [MenuItem("GameObject/Project/UI Scroll View")]
        static public void AddScrollView(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls.CreateScrollView(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }
		
        static public GameObject CreateNewUI()
        {
            // Root for the UI
            var root = new GameObject("UI Root");
            root.layer = LayerMask.NameToLayer(kUILayerName);
            Canvas canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.pixelPerfect = true;
            root.AddComponent<UnityEngine.UI.CanvasScaler>();
            root.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            root.AddComponent<UIRoot>();
            Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

            // if there is no event system add one...
            CreateEventSystem(false);
            return root;
        }

        [MenuItem("GameObject/Project/UI Event System")]
        public static void CreateEventSystem(MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;
            CreateEventSystem(true, parent);
        }

        private static void CreateEventSystem(bool select)
        {
            CreateEventSystem(select, null);
        }

        private static void CreateEventSystem(bool select, GameObject parent)
        {
            var esys = Object.FindObjectOfType<EventSystem>();
            if (esys == null)
            {
                var eventSystem = new GameObject("EventSystem");
                GameObjectUtility.SetParentAndAlign(eventSystem, parent);
                esys = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();

                Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
            }

            if (select && esys != null)
            {
                Selection.activeGameObject = esys.gameObject;
            }
        }

        // Helper function that returns a Canvas GameObject; preferably a parent of the selection, or other existing Canvas.
        static public GameObject GetOrCreateCanvasGameObject()
        {
            GameObject selectedGo = Selection.activeGameObject;

            // Try to find a gameobject that is the selected GO or one if its parents.
            Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
            if (canvas != null && canvas.gameObject.activeInHierarchy)
                return canvas.gameObject;

            // No canvas in selection or its parents? Then use just any canvas..
            canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
            if (canvas != null && canvas.gameObject.activeInHierarchy)
                return canvas.gameObject;

            // No canvas in the scene at all? Then create a new one.
            return MenuOptions.CreateNewUI();
        }
    }
}
