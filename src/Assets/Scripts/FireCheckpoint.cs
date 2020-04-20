using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireCheckpoint : MonoBehaviour
{
    [SerializeField] float minBonfireFlicker, maxBonfireFlicker, bonfireFlickerSpeed, fadeOutTime;

    bool bonfireUsed, bonfireActive;

    Light2D bonfire;
    [SerializeField] GameObject player, fireSprite;
    PlayerHealth playerHealth;
    AudioSource audioSource;
    
    

    void Start()
    {
        bonfire = GetComponent<Light2D>();
        playerHealth = player.GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        HideText();

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
        if (collision.gameObject.CompareTag("Player"))
        {
            bonfireActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bonfireActive = false;
        }
    }

    IEnumerator DestroyBonfire()
    {
        StartCoroutine(DeplenishFire());
        if (GetComponentInChildren<Canvas>() != null)
        {
            transform.Find("Canvas").gameObject.SetActive(false);
        }
        StartCoroutine(FadeOutAudio(audioSource));

        yield return new WaitForSeconds(5);
        Destroy(GetComponent<AudioSource>());
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
        if (GetComponentInChildren<Canvas>() != null)
        {
            GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    void HideText()
    {
        if (GetComponentInChildren<Canvas>() != null)
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    IEnumerator FadeOutAudio(AudioSource audioSource)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= audioSource.volume * Time.deltaTime / fadeOutTime;
            yield return null;
        }
    }
}