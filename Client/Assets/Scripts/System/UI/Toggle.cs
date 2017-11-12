using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

 namespace RedStone.UI
{
    /// <summary>
    /// Simple toggle -- something that has an 'on' and 'off' states: checkbox, toggle button, radio button, etc.
    /// </summary>
    [AddComponentMenu("Project UI/Toggle", 31)]
    [RequireComponent(typeof(RectTransform))]
    public class Toggle : UnityEngine.UI.Toggle
    {
        protected override void Start()
        {
			base.Start();
			#if UNITY_EDITOR
			if(Application.isPlaying)
			#endif
			onValueChanged.AddListener(new UnityAction<bool>(OnEvent));
            if (graphic != null)
            {
                var image = graphic.GetComponent<Image>();
                if (image != null)
                {
                    image.OnSetSpriteHandler = UpdateAlpha;
                }
            }
        }

		private void UpdateAlpha()
		{
			graphic.canvasRenderer.SetAlpha (isOn ? 1f : 0f);
		}

        /// <summary>
        /// 屏蔽 Allow Switch Off 
        /// </summary>
        /// <param name="isOn"></param>
		public void ForceSetIsOn(bool isOn)
		{
			if (this.group != null) {
				bool allow = this.group.allowSwitchOff;
				this.group.allowSwitchOff = true;
				this.isOn = isOn;
				this.group.allowSwitchOff = allow;
			} else {
				this.isOn = isOn;
			}
		}

		protected override void OnEnable ()
		{
			base.OnEnable ();
		}
        private void OnEvent(bool isOn)
        {
            if (string.IsNullOrEmpty(m_Event))
                return;
            this.DispatchEvent("OnToggle" + m_Event, isOn);
        }

        [SerializeField]
        private string m_Event;
        public string Event
        {
            get
            {
                return m_Event;
            }
            set
            {
                m_Event = value;
            }
        }

    }
}
