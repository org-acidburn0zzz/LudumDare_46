using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobTriggerActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<Blob>().ActivateBlob();
    }
}
