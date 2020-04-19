using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireCheckpoint : MonoBehaviour
{
    [SerializeField] float minBonfireFlicker, maxBonfireFlicker, bonfireFlickerSpeed;

    bool bonfireUsed, bonfireActive;

    Light2D bonfire;
    [SerializeField] GameObject player, fireSprite;
    PlayerHealth playerHealth;
    
    

    void Start()
    {
        bonfire = GetComponent<Light2D>();
        playerHealth = player.GetComponent<PlayerHealth>();
        GetComponentInChildren<Canvas>().enabled = true;

        StartCoroutine(BonfireFlicker());
    }

    private void Update()
    {
        if (bonfireActive && !bonfireUsed)
        {
            ShowText();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                bonfireUsed = true;
                playerHealth.SetPlayerHealth(1);
                StartCoroutine(DestroyBonfire());
            }
        } else
        {
            HideText();
        }
    }      

    IEnumerator BonfireFlicker()
    {
        bonfire.intensity = Random.Range(minBonfireFlicker, maxBonfireFlicker);
        yield return new WaitForSeconds(1 / bonfireFlickerSpeed);
        StartCoroutine(BonfireFlicker());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //il giocatore è in range
        if (collision.gameObject.CompareTag("Player"))
        {
            bonfireActive = true;
            Debug.Log(bonfireActive);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //il giocatore è fuori dal range
        if (collision.gameObject.CompareTag("Player"))
        {
            bonfireActive = false;
            Debug.Log(bonfireActive);
        }
    }

    IEnumerator DestroyBonfire()
    {
        StartCoroutine(DeplenishFire());
        yield return new WaitForSeconds(5);
        Destroy(transform.Find("Canvas"));
        Destroy(GetComponent<FireCheckpoint>());
    }

    IEnumerator DeplenishFire()
    {
        fireSprite.GetComponent<TorchFader>().FadeSprite();
        bonfire.intensity -= 0.1f;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DeplenishFire());
    }

    void ShowText()
    {
       
        GetComponentInChildren<Canvas>().enabled = true;
    }

    void HideText()
    {
        GetComponentInChildren<Canvas>().enabled = false;
    }
}