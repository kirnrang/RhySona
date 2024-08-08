using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonaExpantion : MonoBehaviour
{
    private GameObject sona = null;
    SpriteRenderer spriteRenderer = null;
    Transform tr = null;

    public Vector3 sonaScale = new Vector3(0.35f, 0.35f, 0.35f );
    public float extandeTime = 0.5f;
    public float fadeTime = .5f;

    void Start()
    {
        sona = this.gameObject;
        spriteRenderer = sona.GetComponent<SpriteRenderer>();
        tr = sona.GetComponent<Transform>();

        StartCoroutine(ExpandAndFade());
    }


    IEnumerator ExpandAndFade()
    {
        Vector3 initialScale = tr.localScale;
        Vector3 targetScale = sonaScale;
        float elapsedTime = 0;

        // Expand
        while (elapsedTime < extandeTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / extandeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tr.localScale = targetScale;

        // Fade
        elapsedTime = 0;
        Color initialColor = spriteRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        while (elapsedTime < fadeTime)
        {
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }

}
