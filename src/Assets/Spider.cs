using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        FollowPlayer();
    }

    void LookAtPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 1 * Time.deltaTime);
    }
}
