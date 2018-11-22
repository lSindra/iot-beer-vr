using System.Collections;
using UnityEngine;

public class FadeInAndOut : MonoBehaviour {
    public Material materialFaded;

    void Start () 
    {
        FadeTransparent();
	}

    public void FadeOpaque() 
    {
        Color transparentColor = materialFaded.color;
        transparentColor.a = 0;
        materialFaded.color = transparentColor;
        StartCoroutine(FadeTo(1.0f, 2.0f));
    }

    public void FadeTransparent()
    {
        Color opaqueColor = materialFaded.color;
        opaqueColor.a = 1;
        materialFaded.color = opaqueColor;
        StartCoroutine(FadeTo(0f, 2.0f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = materialFaded.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = materialFaded.color;
            newColor.a = Mathf.Lerp(alpha, aValue, t);
            materialFaded.color = newColor;
            yield return null;
        }
    }
}
