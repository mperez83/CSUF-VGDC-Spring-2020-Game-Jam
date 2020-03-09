using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public Toggle freeAimToggle;
    public TMP_Dropdown mapDropdown;
    public TMP_Dropdown musicDropdown;
    public string[] maps;
    public AudioClip[] tunes;

    void Start()
    {
        freeAimToggle.isOn = GameManager.instance.freeAim;
    }

    public void OnMusicChange()
    {
        if (GameManager.instance.pizzaTime == false)
        {
            if (musicDropdown.value > 0)
            {
                GameManager.instance.musicAudioSource.clip = tunes[musicDropdown.value - 1];
                GameManager.instance.musicAudioSource.Play();
            }
        }
    }

    public void PlayButton()
    {
        GameManager.instance.selectAudioSource.Play();
        GameManager.instance.freeAim = freeAimToggle.isOn;
        if (mapDropdown.value == 0) FadeHandler.instance.FadeOut(maps[Random.Range(0, maps.Length)], 0.25f);
        else FadeHandler.instance.FadeOut(maps[mapDropdown.value - 1], 0.25f);
    }

    public void CreditsButton()
    {
        GameManager.instance.selectAudioSource.Play();
        FadeHandler.instance.FadeOut("Credits", 0.25f);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
