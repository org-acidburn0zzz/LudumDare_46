using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFader : MonoBehaviour
{
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void FadeSprite()
    {
        if (transform.localScale.x > 0)
        {
            Vector3 newScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, 1);
            transform.localScale = newScale;
        }

    }
}
