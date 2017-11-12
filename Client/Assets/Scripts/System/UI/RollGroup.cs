using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using RedStone.UI;
using uTools;

 namespace RedStone.UI
{
	[RequireComponent (typeof(RectTransform))]
	public class RollGroup : MonoBehaviour
	{
		struct RollItem
		{
			public int priority;
			public int sequence;
			public int curIndex;
			public RectTransform transform;
			public Type type;
			public bool isPermanent;
			public bool isFinished;
		}

		[Range (1, 100)]
		public int maxItemCount = 3;

		private Dictionary<Type, List<RollItem>> cachedItems = new Dictionary<Type, List<RollItem>> ();

		private List<RollItem> itemList = new List<RollItem> ();

		public Action onAllDisappear = null;
		public Action onItemDisappear = null;

		public Vector3 cellSize = Vector3.one;

		private int curSequence = 0;

		public float itemLifeTime = 1f;

		public float animationTime = 0.2f;

		public bool laterWhenFull = false;

		private Queue<Action> laterQueue = new Queue<Action> ();

		void Awake ()
		{
		}

		void OnEnable ()
		{

		}

		private RollItem CreateItem<T> (T itemTemplate, int _priority = -1, int _sequence = -1, int _index = -1, bool _isPermanent = false) where T : MonoBehaviour
		{
			return new RollItem () {
				transform = CreateTransform<T> (itemTemplate),
				curIndex = _index,
				priority = _priority,
				sequence = _sequence,
				isPermanent = _isPermanent,
				isFinished = false,
				type = typeof(T)
			};
		}

		private RectTransform CreateTransform<T> (T itemTemplate) where T : MonoBehaviour
		{
			var currentItem = GameObject.Instantiate (itemTemplate.gameObject).GetComponent<RectTransform> ();
			UIHelper.SetParent (GetComponent<RectTransform> (), true, currentItem);
			return currentItem;
		}

		public int Add<T, D> (T itemTemplate, D data, Action<T, D> setContentFunc, int priority = 0, bool isPermanent = false)
            where T : MonoBehaviour
		{
			return _Add<T, D> (itemTemplate, data, setContentFunc, priority, isPermanent);
		}

		private int _Add<T, D> (T itemTemplate, D data, Action<T, D> setContentFunc, int priority, bool isPermanent, int sequence = -1)
			where T : MonoBehaviour
		{
			if (itemTemplate == null)
				return -1;
			bool push = false;
			itemTemplate.gameObject.SetActive (false);
			RollItem currentItem = new RollItem ();
			if (sequence < 0)
				sequence = ++curSequence;
			var available = FindAvailable (priority);
			if (laterWhenFull && !IsAvailable () && available < 0)
			{
				laterQueue.Enqueue (() =>
				{
					_Add<T, D> (itemTemplate, data, setContentFunc, priority, isPermanent, sequence);
				});
			} else if (itemList.Count < maxItemCount)
			{
				currentItem = CreateItem (itemTemplate, priority, sequence, -1, isPermanent);
				itemList.Add (currentItem);
				currentItem.transform.gameObject.SetActive (true);
				push = true;
			} else if (available >= 0)
			{
				push = true;
				currentItem = itemList [available];
				if (currentItem.type != typeof(T))
				{
					Type type = typeof(T);
					if (cachedItems.ContainsKey (type) && cachedItems [type].Count > 0)
					{
						var cache = cachedItems [type];
						currentItem = cache [cache.Count - 1];
						cache.RemoveAt (cache.Count - 1);
					} else
					{

						currentItem.transform = CreateTransform (itemTemplate);
						currentItem.type = typeof(T);
					}
					var cacheType = itemList [available].type;
					if (!cachedItems.ContainsKey (cacheType))
						cachedItems.Add (cacheType, new List<RollItem> ());
					
					cachedItems [cacheType].Add (itemList [available]);
				}
				currentItem.sequence = sequence;
				currentItem.priority = priority;
				currentItem.curIndex = -1;
				currentItem.isPermanent = isPermanent;
				currentItem.isFinished = false;
				currentItem.type = typeof(T);
				itemList [available].transform.gameObject.SetActive (false);
				itemList [available] = currentItem;

				currentItem.transform.gameObject.SetActive (true);
			}
			if (push)
			{
				PushupItems ();
				if (setContentFunc != null)
				{
					setContentFunc (currentItem.transform.GetComponent<T> (), data);
				}
			}
			return sequence;
		}

		private int FindAvailable (int priority)
		{
			for (int i = itemList.Count - 1; i >= 0; --i)
			{
				if (itemList [i].priority < priority || itemList[i].isFinished)
					return i;
			}

			for (int i = itemList.Count - 1; i >= 0; --i)
			{
				if (itemList [i].priority == priority)
					return i;
			}
			return -1;
		}

		private void PushupItems ()
		{
			itemList.Sort ((a, b) =>
			{
				if(a.isFinished != b.isFinished)
					return a.isFinished.CompareTo(b.isFinished);
				if (a.priority != b.priority)
					return a.priority.CompareTo (b.priority);
				return b.sequence.CompareTo (a.sequence);

			});
			for (int i = 0; i < itemList.Count; ++i)
			{
				var item = itemList [i];
				if (item.curIndex >= 0)
				{
					var pos = uTweener.Begin<uTweenPosition> (itemList [i].transform.gameObject, animationTime);
					pos.from = itemList [i].transform.anchoredPosition;
					pos.to = cellSize * i;
				} else
				{
					uTweener.Stop<uTweenPosition> (item.transform.gameObject);
					item.transform.anchoredPosition = cellSize * i;
					var alpha = uTweenAlpha.Begin (item.transform.gameObject, 0, 1, animationTime, 0, true);
					if (itemLifeTime > 0)
						alpha.SetOnFinishedAction (SetItemDisappear);
					else
						alpha.SetOnFinishedAction (null);
				}
				item.curIndex = i;
				itemList [i] = item;
			}
		}
		public void RemoveItem(int id)
		{
			for (int i = 0; i < itemList.Count; ++i)
			{
				if (itemList [i].sequence == id)
				{
					OnItemDisappear (itemList [i].transform.gameObject);
					break;
				}
			}
			PushupItems ();
		}
		private void SetItemDisappear (GameObject go)
		{
			for (int i = 0; i < itemList.Count; ++i)
			{
				if (itemList [i].transform.gameObject == go && itemList [i].isPermanent)
					return;
			}
			var alpha = uTweenAlpha.Begin (go, 1, 0, animationTime, itemLifeTime, true);
			alpha.SetOnFinishedAction (OnItemDisappear);
		}

		public bool IsAvailable ()
		{
			if (itemList.Count < maxItemCount)
				return true;
			return !CheckAll ();
		}

		private bool CheckAll (bool enable = true)
		{

			for (int i = 0; i < itemList.Count; ++i)
			{
				if (!enable && !(itemList[i].isFinished))
					return false;
				else if (enable && itemList [i].isFinished)
					return false;
			}
			return true;
		}

		private void OnItemDisappear (GameObject go)
		{
			go.gameObject.SetActive (false);			
			for (int i = 0; i < itemList.Count; ++i)
			{
				if (itemList [i].transform.gameObject == go)
				{
					var item = itemList [i];
					item.isFinished = true;
					itemList [i] = item;
					break;
				}
			}
			if (onItemDisappear != null)
				onItemDisappear.Invoke ();
			if (CheckAll (false) && onAllDisappear != null)
				onAllDisappear.Invoke ();

			if (laterWhenFull && laterQueue.Count > 0 && IsAvailable ())
			{
				laterQueue.Dequeue ().Invoke ();
			}
		}
		/*
		public bool testADD = false;
		public bool testRemove = false;
		public int testRemoveSequence = -1;
		public bool testAddPermanent = false;
		public int testPriorityAdd = 0;
	public bool testInit = false;
	public int testIndex = 0;
		public Image testImage;
		public Text testText;
	void Update()
	{
		#if UNITY_EDITOR
		if (testInit)
		{
				if (testText == null)
			{
					testText = new GameObject ("Text").AddComponent<Text> ();
					UIHelper.SetParent (transform, testText.transform);
					testText.gameObject.SetActive (false);
					testText.font = Font.CreateDynamicFontFromOSFont("Arial", testText.fontSize);
					testText.alignment = TextAnchor.MiddleCenter;
			}
				if(testImage == null)
				{
					testImage = new GameObject("Image").AddComponent<Image>();
					UIHelper.SetParent (transform, testImage.transform);
					testImage.gameObject.SetActive (false);
					testImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 20f);
				}
			testInit = false;
		}
		if (testADD)
		{
				if((testIndex % 2) == 0)
				{
					Add (testText,"item" + testIndex,  (item, data)=> item.text = data, testPriorityAdd, testAddPermanent);
				}
				else
					Add(testImage,testIndex,  (item, data)=> item.color = new Color((data % 3) / 3f, data / 100f, data / 100f), testPriorityAdd, testAddPermanent);
				testAddPermanent = false;
			++testIndex;
			testADD = false;
		}
			if(testRemove)
			{
				RemoveItem(testRemoveSequence);
				testRemove = false;
			}
		#endif
	}
	*/
	}

}