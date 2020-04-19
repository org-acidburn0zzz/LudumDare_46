using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 1;
    [SerializeField] float immunityTimeInSec, extraImmunityAfterFlashing;

    [Header("Torch Degradation")]
    [SerializeField][Range(0, 5f)] float degradationSpeed;

    bool canBeDamaged = true;

    const string ENEMY_PROJECTILE_TAG = "Enemy_Projectile";
    const string TORCH_SPRITE = "Torch_Fire";

    //Cache
    SpriteRenderer sr;
    [SerializeField] GameObject torch;
    SpriteRenderer torch_sr;
    FireFlicker fireScript;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fireScript = GetComponentInChildren<FireFlicker>();
        var torch_fire = torch.gameObject.transform.Find(TORCH_SPRITE);
        torch_sr = torch_fire.GetComponent<SpriteRenderer>();

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
            Debug.Log("Collision trigger");
            if (collision.gameObject.CompareTag(ENEMY_PROJECTILE_TAG))
            {
                Debug.Log("Collision trigger projectile");
                Destroy(collision.gameObject);
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

    void GetDamage(float damage)
    {
        health -= damage;
        StartCoroutine(PlayerGetsDamaged());
        CheckIfDead();
        fireScript.UpdateRadius();
    }

    void CheckIfDead()
    {
        if (health <= 0)
        {
            //todo,.kill 
        }
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
            yield return new WaitForSeconds(0.1f);
        }
        canBeDamaged = true;
    }

    public float GetPlayerHealth()
    {
        return health;
    }

    IEnumerator DegradeTorchByTime()
    {
        health -= 0.01f;
        fireScript.UpdateTorch();
        CheckIfDead();
        yield return new WaitForSeconds(degradationSpeed);
        StartCoroutine(DegradeTorchByTime());
    }
}
