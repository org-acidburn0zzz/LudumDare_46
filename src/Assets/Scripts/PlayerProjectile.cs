using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float speed, damage;
    [SerializeField] [Range(1f, 5f)] float projectileDistance;

    void Start()
    {
        StartCoroutine(Autodestruction());
    }

    // Update is called once per frame
    void Update()
    {
        //   transform.position += transform.forward  * Time.deltaTime;

        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(projectileDistance);
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
