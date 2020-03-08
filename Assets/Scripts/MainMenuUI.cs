using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayButton()
    {
        FadeHandler.instance.FadeOut("Game", 0.5f);
    }

    public void CreditsButton()
    {
        FadeHandler.instance.FadeOut("Credits", 0.5f);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
