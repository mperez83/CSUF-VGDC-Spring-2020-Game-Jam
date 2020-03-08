using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler instance;

    public Image threeImage;
    public Image twoImage;
    public Image oneImage;
    public Image goImage;

    public TextMeshProUGUI winText;

    public Player playerOne;
    public Player playerTwo;

    void Start()
    {
        instance = this;

        Color emptyColor = new Color(1, 1, 1, 0);

        LeanTween.delayedCall(gameObject, 0.5f, () =>
        {
            LeanTween.value(threeImage.gameObject, emptyColor, Color.white, 0.25f).setOnUpdate((Color value) =>
            {
                threeImage.color = value;
            }).setOnComplete(() =>
            {
                LeanTween.delayedCall(threeImage.gameObject, 0.25f, () =>
                {
                    LeanTween.value(threeImage.gameObject, Color.white, emptyColor, 0.25f).setOnUpdate((Color value) =>
                    {
                        threeImage.color = value;
                    }).setDestroyOnComplete(true);
                });
            });

            LeanTween.scale(threeImage.gameObject, Vector3.one, 0.25f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                LeanTween.delayedCall(threeImage.gameObject, 0.25f, () =>
                {
                    LeanTween.scale(threeImage.gameObject, Vector3.zero, 0.25f).setEase(LeanTweenType.easeInCubic);
                });
            });
        });

        LeanTween.delayedCall(gameObject, 1f, () =>
        {
            LeanTween.value(twoImage.gameObject, emptyColor, Color.white, 0.25f).setOnUpdate((Color value) =>
            {
                twoImage.color = value;
            }).setOnComplete(() =>
            {
                LeanTween.delayedCall(twoImage.gameObject, 0.25f, () =>
                {
                    LeanTween.value(twoImage.gameObject, Color.white, emptyColor, 0.25f).setOnUpdate((Color value) =>
                    {
                        twoImage.color = value;
                    }).setDestroyOnComplete(true);
                });
            });

            LeanTween.scale(twoImage.gameObject, Vector3.one, 0.25f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                LeanTween.delayedCall(twoImage.gameObject, 0.25f, () =>
                {
                    LeanTween.scale(twoImage.gameObject, Vector3.zero, 0.25f).setEase(LeanTweenType.easeInCubic);
                });
            });
        });

        LeanTween.delayedCall(gameObject, 1.5f, () =>
        {
            LeanTween.value(oneImage.gameObject, emptyColor, Color.white, 0.25f).setOnUpdate((Color value) =>
            {
                oneImage.color = value;
            }).setOnComplete(() =>
            {
                LeanTween.delayedCall(oneImage.gameObject, 0.25f, () =>
                {
                    LeanTween.value(oneImage.gameObject, Color.white, emptyColor, 0.25f).setOnUpdate((Color value) =>
                    {
                        oneImage.color = value;
                    }).setDestroyOnComplete(true);
                });
            });

            LeanTween.scale(oneImage.gameObject, Vector3.one, 0.25f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                LeanTween.delayedCall(oneImage.gameObject, 0.25f, () =>
                {
                    LeanTween.scale(oneImage.gameObject, Vector3.zero, 0.25f).setEase(LeanTweenType.easeInCubic);
                });
            });
        });

        LeanTween.delayedCall(gameObject, 2f, () =>
        {
            LeanTween.value(goImage.gameObject, emptyColor, Color.white, 0.25f).setOnUpdate((Color value) =>
            {
                goImage.color = value;
            }).setOnComplete(() =>
            {
                LeanTween.delayedCall(goImage.gameObject, 0.25f, () =>
                {
                    LeanTween.value(goImage.gameObject, Color.white, emptyColor, 0.25f).setOnUpdate((Color value) =>
                    {
                        goImage.color = value;
                    }).setOnComplete(() =>
                    {
                        playerOne.enabled = true;
                        playerTwo.enabled = true;
                        Destroy(goImage.gameObject);
                    });
                });
            });

            LeanTween.scale(goImage.gameObject, Vector3.one, 0.25f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                LeanTween.delayedCall(goImage.gameObject, 0.25f, () =>
                {
                    LeanTween.scale(goImage.gameObject, Vector3.zero, 0.25f).setEase(LeanTweenType.easeInCubic);
                });
            });
        });
    }

    public void EndGame(int playerThatDidNotWin)
    {
        GameObject winPlayerObject = (playerThatDidNotWin == 2) ? playerOne.gameObject : playerTwo.gameObject;
        Player winPlayer = winPlayerObject.GetComponent<Player>();
        winPlayer.weaponBase.cooldownTimerLength = 0;
        winText.gameObject.SetActive(true);
        winText.text = "Player " + winPlayer.playerNum + " wins!!!";
        winText.color = winPlayer.GetComponent<SpriteRenderer>().color;
        CameraShakeHandler.instance.maxIntensity = 0.01f;

        LeanTween.delayedCall(gameObject, 5, () =>
        {
            FadeHandler.instance.FadeOut("MainMenu", 2);
        });
    }
}
