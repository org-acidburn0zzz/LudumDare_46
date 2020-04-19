using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireFlicker : MonoBehaviour
{
    [SerializeField] float minFlicker, maxFlicker, flickerSpeed;

    float defaultInnerRadius, defaultOuterRadius;

    //Cache
    [SerializeField] GameObject player;
    PlayerHealth playerHealth;

    new Light2D light;

    void Start()
    {
        light = GetComponent<Light2D>();
        playerHealth = player.GetComponent<PlayerHealth>();

        defaultInnerRadius = light.pointLightInnerRadius;
        defaultOuterRadius = light.pointLightOuterRadius;

        StartCoroutine(Flicker());
    }

    private void Update()
    {
        UpdateFlickerValues();
    }

    IEnumerator Flicker()
    {
        light.intensity = Random.Range(minFlicker, maxFlicker);
        yield return new WaitForSeconds(1 / flickerSpeed);
        StartCoroutine(Flicker());
    }

    public void UpdateFlickerValues()
    {
        minFlicker = Mathf.Clamp(playerHealth.GetPlayerHealth() - 0.2f, 0f, 2f);            
        maxFlicker = playerHealth.GetPlayerHealth() + 0.2f;
    }

    public void UpdateRadius()
    {
        light.pointLightInnerRadius = defaultInnerRadius * playerHealth.GetPlayerHealth();
        light.pointLightOuterRadius = defaultOuterRadius * playerHealth.GetPlayerHealth();
    }

    public void UpdateTorch()
    {
        UpdateFlickerValues();
        UpdateRadius();
    }
}
