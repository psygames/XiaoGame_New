using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hotfire;

[ExecuteInEditMode]
public class ModuleEdit : MonoBehaviour
{
	#if UNITY_EDITOR
	public static ModuleEdit instance;

	public UnityEngine.UI.RawImage rawImage;

	public GameObject navigation;

	public GameObject bottomBar;

	void OnEnable()
	{
		instance = this;
		navigation.hideFlags |= HideFlags.HideInHierarchy;
		if(Localization.instance == null)
			Localization.CreateAndLoad ();
		Localization.instance.SetLanguageSync (1);
		RefreshLanguage ();
	}

	public void RefreshLanguage()
	{
		UIHelper.ReloadFontStyle (gameObject);
	}
	void OnDisable()
	{
		instance = null;
	}


	#endif
}

