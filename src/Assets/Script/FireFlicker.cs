using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireFlicker : MonoBehaviour
{
    [SerializeField] float minFlicker, maxFlicker, flickerSpeed;

    float defaultInnerRadius, defaultOuterRadius;

    [SerializeField] GameObject player;

    new Light2D light;

    void Start()
    {
        light = GetComponent<Light2D>();

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
        minFlicker = Mathf.Clamp(player.GetComponent<PlayerHealth>().GetPlayerHealth() - 0.2f, 0f, 2f);            
        maxFlicker = player.GetComponent<PlayerHealth>().GetPlayerHealth() + 0.2f;
    }

    public void UpdateRadius()
    {
        light.pointLightInnerRadius -= .5f;
        light.pointLightOuterRadius -= .5f;
    }
}
