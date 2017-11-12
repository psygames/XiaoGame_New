using UnityEngine;
using System.Collections;

 namespace RedStone.UI
{
	public interface IListItem
	{
		void SetContent (int index, object data);
	}

}