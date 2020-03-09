using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public AudioClip pizzaTheme;

    public void BackButton()
    {
        GameManager.instance.selectAudioSource.Play();
        FadeHandler.instance.FadeOut("MainMenu", 0.5f);
    }

    public void SecretButton()
    {
        GameManager.instance.pizzaTime = true;
        GameManager.instance.musicAudioSource.clip = pizzaTheme;
        GameManager.instance.musicAudioSource.Play();
    }
}
