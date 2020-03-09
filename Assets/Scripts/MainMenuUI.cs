using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Toggle freeAimToggle;

    void Start()
    {
        freeAimToggle.isOn = GameManager.instance.freeAim;
    }

    public void PlayButton()
    {
        GameManager.instance.selectAudioSource.Play();
        GameManager.instance.freeAim = freeAimToggle.isOn;
        FadeHandler.instance.FadeOut("Map_2", 0.5f);
    }

    public void CreditsButton()
    {
        GameManager.instance.selectAudioSource.Play();
        FadeHandler.instance.FadeOut("Credits", 0.5f);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
