using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 1;
    [SerializeField] float immunityTimeInSec, extraImmunityAfterFlashing;

    bool canBeDamaged = true;

    const string ENEMY_PROJECTILE_TAG = "Enemy_Projectile";

    //Cache
    SpriteRenderer sr, torch_sr;
    FireFlicker fireScript;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fireScript = GetComponentInChildren<FireFlicker>();
        //torch_sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
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

    void GetDamage(float damage)
    {
        health -= damage;
        PlayHurtEffect();
        CheckDeath();
        fireScript.UpdateRadius();
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            //todo,.kill 
        }
    }

    void PlayHurtEffect()
    {
        StartCoroutine(PlayerGetsDamaged());
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
            sr.enabled = !sr.enabled;
           // torch_sr.enabled = !torch_sr.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        canBeDamaged = true;
    }

    public float GetPlayerHealth()
    {
        return health;
    }
}
