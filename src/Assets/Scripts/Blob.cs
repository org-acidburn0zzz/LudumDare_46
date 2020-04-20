using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    [SerializeField] float blobDamage;
    [SerializeField] AudioClip[] deathSound;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip hurtPlayerSFX;

    Animator anim;
    Rigidbody2D rb;
    GameObject player;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        //StartCoroutine(JumpAnim());
        StartCoroutine(JumpForce());
    }

    IEnumerator JumpForce()
    {
        if (isActive)
        {
            LookAtPlayer();
            AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
            rb.velocity = Vector3.zero;
            anim.SetTrigger("jump");
            rb.AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(JumpForce());
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 10)
        {
            isActive = true;
        }   
    }

    IEnumerator JumpAnim()
    {
        if (isActive)
        {
            LookAtPlayer();
            anim.SetTrigger("jump");
            AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(JumpAnim());
    }

    void LookAtPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (player.transform.position.x == transform.position.x)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(hurtPlayerSFX, transform.position);
            player.GetComponent<PlayerHealth>().GetDamage(blobDamage);
        }
        if (collision.gameObject.CompareTag("Player_Sword"))
        {
            Debug.Log("Sword trigger");
            KillThisEnemy();
        }
        if (collision.gameObject.CompareTag("Player_Projectile"))
        {
            Destroy(collision.gameObject);
            KillThisEnemy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(hurtPlayerSFX, transform.position);
            player.GetComponent<PlayerHealth>().GetDamage(blobDamage);
        }
    }


    void KillThisEnemy()
    {
        IncresePlayerHealth();
        AudioSource.PlayClipAtPoint(deathSound[Random.Range(0, deathSound.Length)], transform.position);
        GameObject explosionObj = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        Destroy(explosionObj, 0.3f);
    }

    void IncresePlayerHealth()
    {
        player.GetComponent<PlayerHealth>().IncreasePlayerHealth(0.1f);
    }

    public void ActivateBlob()
    {
        isActive = true;
    }
}
