using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firepoint;
    bool canShoot = false;

    private void Update()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(projectilePrefab, firepoint.transform.position, firepoint.transform.rotation);
        yield return new WaitForSeconds(1 / rateOfFire);
        canShoot = true;
    }

    public void ActivateWizard()
    {
        if (!canShoot)
        {
            canShoot = true;
        }
    }

    public void DeactivateWizard()
    {
        canShoot = false;
        StopAllCoroutines();
    }
}
