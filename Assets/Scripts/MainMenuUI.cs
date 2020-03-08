using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayButton()
    {
        FadeHandler.instance.FadeOut("Game", 1);
    }

    public void CreditsButton()
    {
        FadeHandler.instance.FadeOut("Credits", 1);
    }

    public void QuitButton()
    {
        FadeHandler.instance.FadeOut("Quit", 1);
    }
}
