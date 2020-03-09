using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApeshitPotion : MonoBehaviour
{
    bool triggered;
    float mainDeg;
    float sinVal;

    void Start()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 2).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            LeanTween.delayedCall(gameObject, 8f, () =>
            {
                LeanTween.scale(gameObject, Vector3.zero, 6f).setOnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        });
    }

    void Update()
    {
        mainDeg += (360 * (1f / 2f)) * Time.deltaTime;
        while (mainDeg > 360) mainDeg -= 360;
        sinVal = 5 * Mathf.Sin(mainDeg * Mathf.Deg2Rad);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, sinVal);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            LeanTween.cancel(gameObject);

            LeanTween.scale(gameObject, transform.localScale * 1.5f, 0.5f).setEase(LeanTweenType.easeOutCubic);
            LeanTween.alpha(gameObject, 0, 0.5f).setEase(LeanTweenType.easeOutCubic);

            WeaponBase weaponToMessUp = other.GetComponent<Player>().weaponBase;
            float preApeshitCooldownLength = weaponToMessUp.cooldownTimerLength;
            weaponToMessUp.cooldownTimerLength = 0;
            weaponToMessUp.cooldownTimer = 0;

            LeanTween.delayedCall(gameObject, 2, () =>
            {
                if (other != null)
                {
                    weaponToMessUp.cooldownTimerLength = preApeshitCooldownLength;
                    Destroy(gameObject);
                }
            });
        }
    }
}
