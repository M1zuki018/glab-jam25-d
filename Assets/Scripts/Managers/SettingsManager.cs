using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideSettings : MonoBehaviour
{

    // ----------------------------------------
    // REFERENCES
    // ----------------------------------------
    [Header("References")]
    public CanvasGroup mainMenuGroup;
    public CanvasGroup settingsGroup;
    public CanvasGroup creditsGroup;
    public float fadeTime = 1f;

    [Header("Volume")]
    public Slider bgmSlider;

    // ----------------------------------------
    // UNITY EVENTS
    // ----------------------------------------
    private void Start()
    {
        // Initialize settings

        if (settingsGroup != null)
        {
            settingsGroup.alpha = 0;
            settingsGroup.interactable = false;
            settingsGroup.blocksRaycasts = false;
        }

        if(creditsGroup != null)
        {
            creditsGroup.alpha = 0;
            creditsGroup.interactable = false;
            creditsGroup.blocksRaycasts = false;
        }
    }

    // ----------------------------------------
    // PANEL CONTROLS
    // ----------------------------------------

    public void ShowSettings()
    {
        if (settingsGroup == null) return;

        // Show settings panel, disable main menu interaction
        settingsGroup.alpha = 1;
        settingsGroup.interactable = true;
        settingsGroup.blocksRaycasts = true;

        if (mainMenuGroup != null)
            mainMenuGroup.interactable = false;

        SyncSliderWithValue();
    }

    public void HideSettings()
    {
        if (settingsGroup == null) return;

        // Hide settings panel, re-enable main menu interaction
        settingsGroup.alpha = 0;
        settingsGroup.interactable = false;
        settingsGroup.blocksRaycasts = false;

        if (mainMenuGroup != null)
            mainMenuGroup.interactable = true;

        SyncSliderWithValue();
    }

    public void ShowCredits()
    {
        if (creditsGroup == null) return;

        creditsGroup.alpha = 1;
        creditsGroup.interactable = true;
        creditsGroup.blocksRaycasts = true;

        if (mainMenuGroup != null)
            mainMenuGroup.interactable = false;
    }

    public void HideCredits()
    {
        if (creditsGroup == null) return;

        creditsGroup.alpha = 0;
        creditsGroup.interactable = false;
        creditsGroup.blocksRaycasts = false;

        if(mainMenuGroup != null)
            mainMenuGroup.interactable = true;
    }

    // ----------------------------------------
    // VOLUME SETTINGS
    // ----------------------------------------
    public void SetVolume()
    {
        if (bgmSlider == null) return;

        SoundManager.Instance.SetMusicVolume(bgmSlider.value);
    }

    public void SyncSliderWithValue()
    {
        if (bgmSlider == null || SoundManager.Instance == null)
            return;

        bgmSlider.value = SoundManager.Instance.GetMusicVolume();
    }
}
