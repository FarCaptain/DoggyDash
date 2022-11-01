using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    Text text;
    private void OnEnable()
    {
        text = GetComponent<Text>();
        Color c = text.color;
        text.color = new Color(c.r, c.g, c.b, 0f);

        InvokeRepeating("FadeInFadeOutText", 1, 0.2f);
    }

    void FadeInFadeOutText()
    {
        Color c = text.color;
        float alpha = Mathf.Clamp01(c.a + 0.1f);
        text.color = new Color(c.r, c.g, c.b, alpha);

        if (alpha == 1f)
        {
            CancelInvoke();
            InvokeRepeating("FadeOutText", 2.2f, 0.2f);
        }
    }

    void FadeOutText()
    {
        Color c = text.color;
        float alpha = Mathf.Clamp01(c.a - 0.1f);
        text.color = new Color(c.r, c.g, c.b, alpha);

        if (alpha == 0f)
            CancelInvoke();
    }
}
