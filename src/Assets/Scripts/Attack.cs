using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] GameObject projectilePrefab;
    bool doAttack = false;

    //Cache
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        //   Shoot();
        doAttack = true;
        animator.Play("Wand_Attack");
        yield return new WaitForSeconds(1 / rateOfFire);
        StartCoroutine(ShootRoutine());
    }

    void Shoot()
    {
        //shoot-a-boop
        var projectile = Instantiate(projectilePrefab, transform.GetChild(0).transform.position, Quaternion.identity);
    }
}
