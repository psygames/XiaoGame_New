using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CustomLightingMarmoset : MonoBehaviour {
    [SerializeField]
    private Material[] m_allMaterials;
    [SerializeField]
    private Light m_sceneMainLight;
	// Use this for initialization
	void Start () {
        //GameObject lightObj = new GameObject();
        //lightObj.name = "PlayerDefaultLight";
        //m_sceneMainLight = lightObj.AddComponent<Light>();
        //m_sceneMainLight.intensity = 1.32f;
        //m_sceneMainLight.type = LightType.Directional;
        //m_sceneMainLight.color = new Color(251.0f / 255, 246.0f / 255, 231.0f / 255);
        //m_sceneMainLight.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
        SkinnedMeshRenderer[] allSkin = GetComponentsInChildren<SkinnedMeshRenderer>();
        m_allMaterials = new Material[allSkin.Length];
        for(int i = 0; i < allSkin.Length; ++i)
        {
            if (!Application.isPlaying)
                m_allMaterials[i] = allSkin[i].sharedMaterial;
            else
                m_allMaterials[i] = allSkin[i].material;
        }
        Light[] allLight = GetComponentsInChildren<Light>();
        int lightNum = allLight.Length;
        //if(lightNum == 0)
        //{
         //   allLight = new Light[1];
          //  allLight[0] = m_sceneMainLight;
        //}
        if(lightNum > 0)
        {
            string keyword = "CUSTOMLIHGT" + lightNum;
            foreach (var mat in m_allMaterials)
            {
                mat.shaderKeywords = new string[]{keyword};
                //Shader.EnableKeyword(keyword);
                mat.EnableKeyword(keyword);
            }
        }else
        {
            return;
        }
        int lightIndex = 0;
        foreach(var light in allLight)
        {
            lightIndex++;
            if(light.type == LightType.Directional)
            {
                if (lightIndex == 1)
                {
                    foreach (var mat in m_allMaterials)
                    {
                        Vector3 lightDir = -light.transform.forward;
                        Color lightColor = light.color * light.intensity;
                        //Shader.SetGlobalVector("_customLight1Dir", lightDir);
                        //Shader.SetGlobalColor("_customLight1Color", lightColor);
                        mat.SetVector("_customLight1Dir", lightDir);
                        mat.SetColor("_customLight1Color", lightColor);
                    }
                }
                if(lightIndex == 2)
                {
                    foreach (var mat in m_allMaterials)
                    {
                        Vector3 lightDir = -light.transform.forward;
                        Color lightColor = light.color * light.intensity;
                        //Shader.SetGlobalVector("_customLight2Dir", lightDir);
                        //Shader.SetGlobalColor("_customLight2Color", lightColor);
                        mat.SetVector("_customLight2Dir", lightDir);
                        mat.SetColor("_customLight2Color", lightColor);
                    }
                }
                if(lightIndex == 3)
                {
                    foreach (var mat in m_allMaterials)
                    {
                        Vector3 lightDir = -light.transform.forward;
                        Color lightColor = light.color * light.intensity;
                        //Shader.SetGlobalVector("_customLight3Dir", lightDir);
                        //Shader.SetGlobalColor("_customLight3Color", lightColor);
                        mat.SetVector("_customLight3Dir", lightDir);
                        mat.SetColor("_customLight3Color", lightColor);
                    }
                }
            }else
            {
                if (lightIndex == 1)
                {
                    foreach (var mat in m_allMaterials)
                    {
                        Vector3 lightDir = (light.transform.position -  transform.position).normalized;
                        Color lightColor = light.color * light.intensity;
                        //Shader.SetGlobalVector("_customLight1Dir", lightDir);
                        //Shader.SetGlobalColor("_customLight1Color", lightColor);
                        mat.SetVector("_customLight1Dir", lightDir);
                        mat.SetColor("_customLight1Color", lightColor);
                    }
                }
                if (lightIndex == 2)
                {
                    foreach (var mat in m_allMaterials)
                    {
                        Vector3 lightDir = (light.transform.position - transform.position).normalized;
                        Color lightColor = light.color * light.intensity;
                        //Shader.SetGlobalVector("_customLight2Dir", lightDir);
                        //Shader.SetGlobalColor("_customLight2Color", lightColor);
                        mat.SetVector("_customLight2Dir", lightDir);
                        mat.SetColor("_customLight2Color", lightColor);
                    }
                }
                if (lightIndex == 3)
                {
                    foreach (var mat in m_allMaterials)
                    {
                        Vector3 lightDir = (light.transform.position - transform.position).normalized;
                        Color lightColor = light.color * light.intensity;
                        //Shader.SetGlobalVector("_customLight3Dir", lightDir);
                        //Shader.SetGlobalColor("_customLight3Color", lightColor);
                        mat.SetVector("_customLight3Dir", lightDir);
                        mat.SetColor("_customLight3Color", lightColor);
                    }
                }
            }
        }
	}
}
