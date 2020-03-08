using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float speed;
    public float duration;
    Rigidbody2D rb;
    [HideInInspector]
    public Player owner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        LeanTween.scale(gameObject, Vector2.zero, duration).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(true);
        GetComponent<SpriteRenderer>().color = owner.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != owner.gameObject)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().Die();
            }
        }
    }
}
