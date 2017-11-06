using UnityEngine;
using System.Collections;
using Hotfire.UI;

namespace Hotfire
{
    public class GetMaxWidthText : MonoBehaviour
    {
        public Transform targetRoot;

        private Text m_maxWidthText = null;
        public Text maxWidthText { get { return m_maxWidthText; } }
        private Text[] m_texts = null;
        void Start()
        {

        }

        void Update()
        {
            if (m_texts == null || targetRoot == null)
                return;
            for (int i = 0; i < m_texts.Length; i++)
            {
                if (m_maxWidthText == null 
                    || m_texts[i].preferredWidth > m_maxWidthText.preferredWidth)
                    m_maxWidthText = m_texts[i];
            }
        }

        public void FindTexts()
        {
            if (targetRoot == null)
                return;

            m_texts = targetRoot.GetComponentsInChildren<Text>();
            Update();
        }
    }
}