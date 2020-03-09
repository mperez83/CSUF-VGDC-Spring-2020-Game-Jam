using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    public AudioClip normalDeath;
    public AudioClip advancedDeath;
    public AudioSource audioSource;

    void Start()
    {
        if (Random.Range(0, 100) == 69) audioSource.PlayOneShot(advancedDeath);
        else audioSource.PlayOneShot(normalDeath);

        LeanTween.delayedCall(gameObject, 1f, () =>
        {
            Destroy(gameObject);
        });
    }
}
