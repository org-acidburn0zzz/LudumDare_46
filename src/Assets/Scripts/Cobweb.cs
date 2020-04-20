using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobweb : MonoBehaviour
{
    [SerializeField] GameObject firePrefab;
    [SerializeField] AudioClip fireSFX;

    const string PLAYER_PROJECTILE = "Player_Projectile";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_PROJECTILE))
        {
            Destroy(collision.gameObject);
            StartCoroutine(DestroyCobweb());
        }
    }

    IEnumerator DestroyCobweb()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - 1);
        var fire = Instantiate(firePrefab, pos, Quaternion.identity);
        AudioSource.PlayClipAtPoint(fireSFX, transform.position);
        yield return new WaitForSeconds(1);
        Destroy(fire);
        Destroy(gameObject);
    }
}
