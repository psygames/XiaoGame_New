using UnityEngine;
using RedStone.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[ExecuteInEditMode]
public class ArcFill : MonoBehaviour
{
    public enum FillDir
    {
        Clockwise,
        Anticlockwise,
        Zoom,
    }

    public float radius = 100;
    public float maxAngle = 90;
    public float zRot;
    public Vector3 ChildOffset;
    public FillDir fillDir;
    [Range(0, 1)]
    public float fillAmount;

    private RectTransform rectTrans;
    private Image maskImage;
    private Mask mask;
    private RectTransform child;

    void Start()
    {
		Debug.LogError ("Warning : Use Arcfill may cause drawcall problems!!!");
    }

    void Update()
    {
        if (rectTrans == null)
            rectTrans = GetComponent<RectTransform>();
        if (maskImage == null)
            maskImage = GetComponent<Image>();
        if (mask == null)
            mask = GetComponent<Mask>();
        if (child == null)
        {
            if (transform.childCount == 0)
                UIHelper.SetParent(transform, new GameObject("child").AddComponent<RectTransform>().transform);
            child = transform.GetChild(0).GetComponent<RectTransform>();
        }

        // mask image
        float aFill = fillAmount * maxAngle / 360f;
        maskImage.fillAmount = aFill;
        maskImage.type = UnityEngine.UI.Image.Type.Filled;
        maskImage.fillMethod = UnityEngine.UI.Image.FillMethod.Radial360;
        maskImage.fillOrigin = 0;
        maskImage.fillClockwise = true;

        // mask type
        mask.showMaskGraphic = false;

        // rect size & rot
        rectTrans.sizeDelta = new Vector2(radius * 4, radius * 4);

        float angle = fillAmount * maxAngle * GetFillFactor();
        Quaternion qua = Quaternion.AngleAxis(angle + zRot, Vector3.forward);
        rectTrans.localRotation = qua;


        // child trans (warning update after rectTrans)
        child.localRotation = Quaternion.Inverse(qua);
        child.localPosition = child.localRotation * ChildOffset;

    }

    private float GetFillFactor()
    {
        switch (fillDir)
        {
            case FillDir.Clockwise:
                return 0;
            case FillDir.Anticlockwise:
                return 1f;
            case FillDir.Zoom:
                return 0.5f;
        }
        return 1;
    }
}
