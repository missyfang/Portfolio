using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingExplosion : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed;
    private bool isFading = false;

    void Update()
    {
        if (!isFading)
        {
            Debug.Log("FadingSmoke");
            isFading = true;
            StartCoroutine(FadeSmoke());
        }
    }

    IEnumerator FadeSmoke()
    {
        SpriteRenderer Smoke = gameObject.GetComponent<SpriteRenderer>();

        gameObject.GetComponent<SpriteRenderer>().color = new Color(Smoke.color.r, Smoke.color.b, Smoke.color.g, Random.Range(0f,1f));

        yield return new WaitForSeconds(fadeSpeed);
        isFading = false;
    }
}
