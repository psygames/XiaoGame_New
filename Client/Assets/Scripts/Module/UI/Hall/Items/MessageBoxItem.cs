using UnityEngine;
using System.Collections;
using System;
using uTools;
using UnityEngine.Events;
using RedStone.UI;

namespace RedStone
{
    public class MessageBoxItem : MonoBehaviour
    {
        public int id = -1;
        public Transform boxTrans;
        public Transform btnCancel;
        public Text title;
        public Image image;
        public Text message;
        string strMessage = "";
        public Image btnOKIcon;
        public Text btnOKText;
        public Text btnOKDesc;
        public Image btnCancelIcon;
        public Text btnCancelText;
        public Text btnCancelDesc;
        public UUIEventListener okEventTrigger;
        public UUIEventListener cancelEventTrigger;
        public UUIEventListener closeEventTrigger;
        public UUIEventListener maskEventTrigger;

        public GameObject btnOK;
        public GameObject btnClose;
        private MessageBoxStyle m_style;
        private float startTime = 0f;
        private float lastTime = 0f;
        public bool HasStyle(MessageBoxStyle style)
        {
            return (m_style & style) != 0;
        }
        public MessageBoxStyle style
        {
            get { return m_style; }
            set
            {
                m_style = value;
                btnClose.gameObject.SetActive(HasStyle(MessageBoxStyle.Close));
                btnCancel.gameObject.SetActive(HasStyle(MessageBoxStyle.Cancel));
                btnOK.gameObject.SetActive(HasStyle(MessageBoxStyle.OK));
            }
        }


        public bool isShowing { get { return m_isShowing; } }
        private bool m_isShowing = false;
        [HideInInspector]
        public Action<MessageBoxResult> callBackHandler = null;

        public const float showDuration = 0.2f;
        private UnityEvent onShow = new UnityEvent();
        private UnityEvent onHide = new UnityEvent();

        bool m_init = false;
        public void Start()
        {
            onHide.AddListener(OnHide);
            okEventTrigger.onClick += OnOKPress;
            cancelEventTrigger.onClick += OnCancelPress;
            closeEventTrigger.onClick += OnClosePress;
            maskEventTrigger.onClick += OnClosePress;
            m_init = true;

        }
        private void Callback(MessageBoxResultType result)
        {
            if (callBackHandler != null)
                callBackHandler(new MessageBoxResult(id, result));
        }
        private void OnOKPress(UUIEventListener listener)
        {
            Callback(MessageBoxResultType.OK);
            Hide();
        }

        private void OnCancelPress(UUIEventListener listener)
        {
            Callback(MessageBoxResultType.Cancel);
            Hide();
        }

        private void OnClosePress(UUIEventListener listener)
        {
            if (!HasStyle(MessageBoxStyle.Close))
                return;
            Callback(MessageBoxResultType.Close);
            Hide();
        }

        public void Hide()
        {
            m_isShowing = false;
            uTweenScale.Begin(boxTrans.gameObject, this.gameObject.transform.localScale, Vector3.zero, showDuration).SetOnFinished(onHide);
        }

        public void OnHide()
        {
            if (!m_isShowing)
                this.gameObject.SetActive(false);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            uTweenScale.Begin(boxTrans.gameObject, (m_init ? Vector3.zero : this.gameObject.transform.localScale), Vector3.one, showDuration).SetOnFinished(onShow);
            m_isShowing = true;
            if (HasStyle(MessageBoxStyle.ReturnID))
                Callback(MessageBoxResultType.ID);
        }

        public void SetMessage(string messageText = null, params object[] param)
        {
            if (string.IsNullOrEmpty(messageText))
                strMessage = "";
            else
                strMessage = LT.GetText(messageText, param);

            startTime = Time.time;
            lastTime = startTime;
            if (HasStyle(MessageBoxStyle.Seconds))
            {

                this.message.text = strMessage + "(0s)";
            }
            else
            {
                this.message.text = strMessage;
            }

        }
        void Update()
        {
            if (HasStyle(MessageBoxStyle.Seconds))
            {
                if (Time.time - lastTime >= 1f)
                {
                    this.message.text = strMessage + ("({0}s)".FormatStr((int)(Time.time - startTime)));
                    lastTime = Time.time;
                }
            }
        }
        public void SetBtnOKText(string btnText = null)
        {
            if (string.IsNullOrEmpty(btnText))
                btnText = "GENRAL_OK";
            this.btnOKText.text = LT.GetText(btnText);
        }

        public void SetBtnCancelText(string btnText = null)
        {
            if (string.IsNullOrEmpty(btnText))
                btnText = "GENRAL_CANCEL";
            this.btnCancelText.text = LT.GetText(btnText);
        }

        public void SetBtnOKDesc(string desc = null)
        {
            btnOKDesc.gameObject.SetActive(!string.IsNullOrEmpty(desc));
            if (btnOKDesc.gameObject.activeSelf)
            {
                this.btnOKDesc.text = LT.GetText(desc);
            }
        }

        public void SetBtnCancelDesc(string desc = null)
        {
            btnCancelDesc.gameObject.SetActive(!string.IsNullOrEmpty(desc));
            if (btnCancelDesc.gameObject.activeSelf)
            {
                this.btnCancelDesc.text = LT.GetText(desc);
            }
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                this.title.text = "";
            else
                this.title.text = LT.GetText(title);
        }

        public void SetImage(string imageName = null, string btnOKIconName = null, string btnCancelIconName = null)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                this.image.gameObject.SetActive(false);
                this.message.transform.localPosition = Vector3.zero;
                Vector2 size = this.message.rectTransform.sizeDelta;
                size.x = 1000;
                this.message.rectTransform.sizeDelta = size;
                this.message.alignment = TextAnchor.MiddleCenter;
            }
            else
            {
                this.image.gameObject.SetActive(true);
                this.image.SetSprite(imageName);
                this.message.transform.localPosition = new Vector3(150, 0, 0);
                Vector2 size = this.message.rectTransform.sizeDelta;
                size.x = 540;
                this.message.rectTransform.sizeDelta = size;
                this.message.alignment = TextAnchor.MiddleLeft;
            }

            if (string.IsNullOrEmpty(btnOKIconName))
                this.btnOKIcon.gameObject.SetActive(false);
            else
            {
                this.btnOKIcon.gameObject.SetActive(true);
                this.btnOKIcon.SetSprite(btnOKIconName);
            }

            if (string.IsNullOrEmpty(btnCancelIconName))
                this.btnCancelIcon.gameObject.SetActive(false);
            else
            {
                this.btnCancelIcon.gameObject.SetActive(true);
                this.btnCancelIcon.SetSprite(btnCancelIconName);
            }
        }


        public void OnDetroy()
        {
            okEventTrigger.onClick -= OnOKPress;
            cancelEventTrigger.onClick -= OnCancelPress;
            maskEventTrigger.onClick -= OnCancelPress;
        }
    }

    public enum MessageBoxStyle
    {
        None = 0,
        Close = 1,
        OK = 2,
        Cancel = 4,
        Buy = 8,
        ReturnID = 16,
        Seconds = 32,
        OKClose = OK | Close,
        CancelClose = Cancel | Close,
        OKCancelClose = OKClose | Cancel,
    }

    public enum MessageBoxResultType
    {
        OK,
        Cancel,
        Close,
        ID,
        None,
    }

    public class MessageBoxResult
    {
        public int id;
        public MessageBoxResultType result;

        public MessageBoxResult(int id, MessageBoxResultType result)
        {
            this.id = id;
            this.result = result;
        }
        public static bool operator ==(MessageBoxResult result, MessageBoxResultType resultEnum)
        {
            return result.result == resultEnum;
        }

        public static bool operator !=(MessageBoxResult result, MessageBoxResultType resultEnum)
        {
            return result.result != resultEnum;
        }
    }
}