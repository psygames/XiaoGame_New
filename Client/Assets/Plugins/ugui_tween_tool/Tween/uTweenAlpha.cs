using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools {
	[AddComponentMenu("uTools/Tween/Tween Alpha(uTools)")]
	public class uTweenAlpha : uTween<float> {

		public GameObject target;
		public bool includeChildren = false;

		Transform mTransform;
		Graphic[] mGraphics;
        Graphic[] cachedGraphics
        {
            get
            {
                if (mGraphics == null)
                {
                    mTransform = GetComponent<Transform>();
                    if (target == null)
                    {
                        target = gameObject;
                    }
                    mGraphics = includeChildren ? target.GetComponentsInChildren<Graphic>() : target.GetComponents<Graphic>();
                }
                return mGraphics;
            }
        }

		float mAlpha = 0f;

		protected override void Start ()
		{		
			base.Start ();
		}

		public float alpha {
			get {
				return mAlpha;
			}
			set {
				SetAlpha(mTransform, value);
				mAlpha = value;
			}
		}

		protected override void OnUpdate (float factor, bool isFinished)
		{
			value = from + factor * (to - from);
			alpha = value;
		}

		void SetAlpha(Transform _transform, float _alpha) {
			foreach (var item in cachedGraphics) {
				Color color = item.color;
				color.a = _alpha;
				item.color = color;
			}
		}

        public static uTweenAlpha Begin(GameObject go, float from, float to, float duration = 1f, float delay = 0f, bool includeChildren = false,System.Action<GameObject> finishHandler=null)
        {
            uTweenAlpha comp = Begin<uTweenAlpha>(go, duration,finishHandler);
            comp.includeChildren = includeChildren;
            comp.value = from;
			comp.alpha = from;
            comp.from = from;
            comp.to = to;
            comp.duration = duration;
            comp.delay = delay;
            if (duration <= 0)
            {
                comp.Sample(1, true);
                comp.enabled = false;
            }
            return comp;
        }


    }

}