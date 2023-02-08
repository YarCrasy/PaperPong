using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void Highlight()
    {
        transform.localScale *= 1.5f;
    }

    public void Unhighlight()
    {
        transform.localScale = originalScale;
    }

}
