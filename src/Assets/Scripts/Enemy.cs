using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const string PLAYER_SWORD_TAG = "Player_Sword";
    const string PLAYER_FIREBALL_TAG = "Fireball";
    const string PLAYER_TAG = "Player";
    const string PLAYER_RANGED_TAG = "Player_Projectile";

    [SerializeField] float health;
    [SerializeField] AudioClip[] deathSound;
    [SerializeField] GameObject explosionPrefab;

    //Cache
    GameObject player;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }

    private void Update()
    {
        LookAtPlayer();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_SWORD_TAG))
        {
            health -= collision.gameObject.GetComponent<Sword>().GetDamage();
            CheckHealth();
        }
        if (collision.gameObject.CompareTag(PLAYER_RANGED_TAG))
        {
            health -= collision.gameObject.GetComponent<PlayerProjectile>().GetDamage();
            Destroy(collision.gameObject);
            CheckHealth();
        }
    }

    void LookAtPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            IncresePlayerHealth();
            AudioSource.PlayClipAtPoint(deathSound[Random.Range(0, deathSound.Length)], transform.position);
            GameObject explosionObj = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
            Destroy(transform.parent.gameObject);
            Destroy(explosionObj, 0.3f);
        }
    }

    void IncresePlayerHealth()
    {
        player.GetComponent<PlayerHealth>().IncreasePlayerHealth(0.1f);
    }
}
