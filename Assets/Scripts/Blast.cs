using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float speed;
    public float activeHitboxDuration;

    void Start()
    {
        LeanTween.scale(gameObject, Vector2.zero, 1).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(true);
        LeanTween.value(gameObject, speed, 0, 1).setEase(LeanTweenType.easeOutQuad).setOnUpdate((float value) =>
        {
            //speed = value;
        });
        LeanTween.delayedCall(gameObject, activeHitboxDuration, () =>
        {
            Destroy(GetComponent<BoxCollider2D>());
        });
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Die();
        }
    }
}
