using UnityEngine;
using System.Collections;

namespace Hotfire.UI
{
	public interface IListItem
	{
		void SetContent (int index, object data);
	}

}