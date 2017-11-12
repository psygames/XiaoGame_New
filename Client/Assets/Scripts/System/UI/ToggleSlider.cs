using UnityEngine;
using System.Collections;
 namespace RedStone.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleSlider : MonoBehaviour
    {
        public Transform slider;
        public Vector2 onPos;
        public Vector2 offPos;
        public bool isOn { get { return toggle.isOn; } }

        public Toggle toggle { get; private set; }
        void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.transition = UnityEngine.UI.Selectable.Transition.None;
        }

        void Update()
        {
            if (slider == null)
                return;
            Vector2 tarPos = isOn ? onPos : offPos;
            slider.localPosition = Vector3.Lerp(slider.localPosition, tarPos, Time.deltaTime * 15);
        }
    }
}