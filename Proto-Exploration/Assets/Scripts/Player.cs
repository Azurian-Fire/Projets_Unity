using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField] HealthBarUI healthBarUI;

    [FormerlySerializedAs("postProcessVolume")] [SerializeField]
    Volume postProcessVolumePos;

    [FormerlySerializedAs("postProcessVolume1")] [SerializeField]
    Volume postProcessVolumeNeg;

    [SerializeField] float transitionDuration;

    [SerializeField] Volume[] volumes;


    private void OnEnable()
    {
        InteractFruit.OnFruitEaten += HandleFruitEaten;
    }

    private void OnDisable()
    {
        InteractFruit.OnFruitEaten -= HandleFruitEaten;
    }

    private void HandleFruitEaten(int fruitEffect, InteractableColorKey interactableColorKey)
    {
        ChangeHealth(fruitEffect);
    }

    void Start()
    {
        ChangeHealth(0);
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            ChangeHealth(-10);
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            ChangeHealth(10);
        }

        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            StartCoroutine(ResetVignette(0));
        }

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            StartCoroutine(UpdateVignetteCoroutine(-1));
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            StartCoroutine(UpdateVignetteCoroutine(1));
        }
    }

    public void ChangeHealth(float healthChange)
    {
        if (healthChange > 0)
        {
            ChosenVignette(-1);
        }
        else if (healthChange < 0)
        {
            ChosenVignette(1);
        }

        StartCoroutine(ResetVignette(1));
        health = Mathf.Clamp(health + healthChange, 0, maxHealth);
        healthBarUI.SetHealthUI(health, maxHealth);
    }

    private void ChosenVignette(int chosenVolumeID)
    {
        int chosenVolumIndex = 1;

        if (chosenVolumeID == -1)
        {
            chosenVolumIndex = 0;
        }

        volumes[chosenVolumIndex].weight = 1;
        volumes[1 - chosenVolumIndex].weight = 0;
    }

    private IEnumerator ResetVignette(float resetDelay)
    {
        yield return new WaitForSeconds(transitionDuration + resetDelay);
        postProcessVolumePos.weight = 0f;
        postProcessVolumeNeg.weight = 0f;
    }

    private IEnumerator UpdateVignetteCoroutine(int volumeID)
    {
        yield return new WaitForSeconds(transitionDuration);
        ChosenVignette(volumeID);
    }
}