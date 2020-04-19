using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackWaitTime, damage;


    bool canAttack = true;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            Debug.Log("Attack");
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        canAttack = false;
        yield return new WaitForSeconds(attackWaitTime);
        canAttack = true;
    }

    public float GetPlayerDamage()
    {
        return damage;
    }

    void ActivateSwordCollider()
    {
        transform.Find("Sword").GetComponent<BoxCollider2D>().enabled = true;
    }

    void DeactivateSwordCollider()
    {
        transform.Find("Sword").GetComponent<BoxCollider2D>().enabled = false;
    }
}
