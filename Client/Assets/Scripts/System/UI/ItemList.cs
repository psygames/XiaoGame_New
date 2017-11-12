using System;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

 namespace RedStone
{
	public class ItemList<D, T> : MonoBehaviour
		where T : MonoBehaviour
    {
        private T m_template;
        private List<T> m_items = new List<T>();
        private Transform m_itemRoot;

        public T template { get { return m_template; } }
        public List<T> items { get { return m_items; } }
        public bool isItemLoaded { get { return m_template != null; } }
        public Transform itemRoot { get { return m_itemRoot; } }

		public void InitAsync(string prefabPath, Transform root)
        {
            m_itemRoot = root;
            GameObjectHelper.AddChildAsync<T>(root, prefabPath, (template) =>
            {
                m_template = template;
            });
        }

		public ItemList()
		{
		}
		public void Init(T template, Transform root)
		{
			m_itemRoot = root;
			m_template = template;
		}
		public void SetGeneralContent(ICollection<D> datas, Action<MonoBehaviour, object> setContentFunc)
		{            
			if (template == null || setContentFunc == null)
				return;
			GameObjectHelper.SetListContent(template, itemRoot, items, datas,
				(index, item, data) =>
			{
				setContentFunc.Invoke(item, data);
			});
		}
		public void SetListItemContent(ICollection<D> datas)
		{			
			if (template == null)
				return;
			GameObjectHelper.SetListContent(template, itemRoot, items, datas, null);
		}
        public void SetContent(ICollection<D> datas, Action<T, D> setContentFunc)
        {
			if (template == null || setContentFunc == null)
                return;
            GameObjectHelper.SetListContent(template, itemRoot, items, datas,
            (index, item, data) =>
            {
                setContentFunc.Invoke(item, data);
            });
        }
    }
}
