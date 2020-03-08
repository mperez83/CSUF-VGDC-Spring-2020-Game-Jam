using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler instance;

    public TextMeshProUGUI winText;

    void Start()
    {
        instance = this;
    }

    public void EndGame()
    {
        GameObject winPlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (winPlayerObject != null)
        {
            Player winPlayer = winPlayerObject.GetComponent<Player>();
            winPlayer.weaponBase.cooldownTimerLength = 0;
            winText.gameObject.SetActive(true);
            winText.text = "Player " + winPlayer.playerNum + " wins!!!\n(your firerate is now infinite)";
            winText.color = winPlayer.GetComponent<SpriteRenderer>().color;
            CameraShakeHandler.instance.maxIntensity = 0.01f;

            LeanTween.delayedCall(gameObject, 8, () =>
            {
                FadeHandler.instance.FadeOut("MainMenu", 2);
            });
        }
    }
}
