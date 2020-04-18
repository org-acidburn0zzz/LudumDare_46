using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed, damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Autodestruction());
    }

    // Update is called once per frame
    void Update()
    {
     //   transform.position += transform.forward  * Time.deltaTime;

        transform.Translate(Vector3.right * speed *  Time.deltaTime); 

    }

    IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject, .5f);
    }

    public float GetDamage()
    {
        return damage;
    }
}
