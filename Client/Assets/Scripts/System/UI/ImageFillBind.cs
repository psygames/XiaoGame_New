using UnityEngine;
using System.Collections;
 namespace RedStone.UI
{
    [ExecuteInEditMode]
    public class ImageFillBind : MonoBehaviour
    {
        public Image bind;
        public SimpleSlider bindSlider;
        private UnityEngine.UI.Image m_target;
        void Awake()
        {
            m_target = GetComponent<Image>();
            if(m_target == null)
                m_target = GetComponent<UnityEngine.UI.Image>();
        }

        void Update()
        {
            if (m_target == null)
                return;

            if (bindSlider != null)
            {
                m_target.fillAmount = bindSlider.value;
            }
            else if (bind != null)
            {
                m_target.fillAmount = bind.fillAmount;
            }
        }
    }
}
