using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 1.2f;
    [SerializeField] float immunityTimeInSec, extraImmunityAfterFlashing;

    [Header("Torch Degradation")]
    [SerializeField] float degradeAmount;
    [SerializeField][Range(0, 5f)] float degradeSpeed;

    [Header("FX")]
    [SerializeField] AudioClip hurtSFX;

    bool canBeDamaged = true;

    const string ENEMY_PROJECTILE_TAG = "Enemy_Projectile";
    const string TORCH_SPRITE = "Torch_Fire";
    const string TORCH_BODY = "Torch_Body";
    const string ENEMY_TAG = "Enemy";

    //Cache
    SpriteRenderer sr;
    [SerializeField] GameObject torch;
    SpriteRenderer torch_sr, torch_body_sr;
    FireFlicker fireScript;
    Animator anim;
    Rigidbody2D rb;
    LevelController levelController;



    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fireScript = GetComponentInChildren<FireFlicker>();
        var torch_fire = torch.gameObject.transform.Find(TORCH_SPRITE);
        var torch_body = torch.gameObject.transform.Find(TORCH_BODY);
        torch_sr = torch_fire.GetComponent<SpriteRenderer>();
        torch_body_sr = torch_body.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        levelController = FindObjectOfType<LevelController>().GetComponent<LevelController>();


        anim.SetBool("isDead", false);
        StartCoroutine(DegradeTorchByTime());
    }

    private void Update()
    {
        ClampHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeDamaged)
        {
            if (collision.gameObject.CompareTag(ENEMY_PROJECTILE_TAG))
            {
                Destroy(collision.gameObject);
                AudioSource.PlayClipAtPoint(hurtSFX, transform.position);
                GetDamage(collision.gameObject.GetComponent<EnemyProjectile>().GetDamage());
            }
        }
    }

    void ClampHealth()
    {
        if (health > 1)
        {
            health = 1;
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        StartCoroutine(PlayerGetsDamaged());
        CheckIfDead();
        fireScript.UpdateRadius();
    }

    void CheckIfDead()
    {
        if (health <= .2f)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        anim.SetBool("isDead", true);
        sr.color = Color.red;
        Vector2 backwards = -rb.velocity;
        rb.AddForce(backwards * 5);
        yield return new WaitForSeconds(1.5f);
        levelController.SetLatestLevel();
        levelController.LoadGameOver();
    }
    
    IEnumerator PlayerGetsDamaged()
    {
        StartCoroutine(FlashSprite());
        yield return new WaitForSeconds(extraImmunityAfterFlashing);
    }

    IEnumerator FlashSprite()
    {
        canBeDamaged = false;
        for (int i = 1; i <= immunityTimeInSec; i++) //start by 1 to get an odd loop, which always ends with the sprites enabled.
        {
            Debug.Log("Flashing");
            sr.enabled = !sr.enabled;
            torch_sr.enabled = !torch_sr.enabled;
            torch_body_sr.enabled = !torch_body_sr.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        canBeDamaged = true;
    }

    public float GetPlayerHealth()
    {
        return health;
    }

    public void SetPlayerHealth(float health)
    {
        this.health = health;
    }

    public void DecreasePlayerHealth(float decreaseAmount)
    {
        this.health -= decreaseAmount;
    }

    public void IncreasePlayerHealth(float increaseAmount)
    {
        this.health += increaseAmount;
    }

    IEnumerator DegradeTorchByTime()
    {
        health -= degradeAmount;
        fireScript.UpdateTorch();
        CheckIfDead();
        yield return new WaitForSeconds(degradeSpeed);
        StartCoroutine(DegradeTorchByTime());
    }
}
