using UnityEngine;

namespace RedStone.UI
{
    /// <summary>
    /// Image is a textured element in the UI hierarchy.
    /// </summary>

    [AddComponentMenu("Project UI/Image", 11)]
    [RequireComponent(typeof(RectTransform))]
    public class Image : UnityEngine.UI.Image
    {
        public System.Action OnSetSpriteHandler;

        static Material greyMat = null;
        [SerializeField]
        [HideInInspector]
        private bool m_pooled = true;


        public bool dontUsePool = false;
        public bool Pooled
        {
            get
            {
                return m_pooled;
            }
            set
            {
                m_pooled = value;
            }
        }
        public bool enableGrey;
        public bool Grey
        {
            set
            {
                if (!enableGrey)
                    return;
                else if (value)
                    material = greyMat;
                else
                    material = null;

            }
            get
            {
                return enableGrey ? (greyMat != null && greyMat == material) : false;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (greyMat == null)
                greyMat = Resources.Load<Material>("Materials/UI/GreyUI");
        }
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public void SetSprite(string spriteName, bool isSetNativeSize = true)
        {
            if (Application.isPlaying)
            {
                sprite = GF.GetProxy<SpriteProxy>().GetSprite(spriteName);
                if (isSetNativeSize)
                    SetNativeSize();
            }
        }

        public void SetAlpha(float alpha)
        {
            UIHelper.SetImageAlpha(this, alpha);
        }
    }
}
