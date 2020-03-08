using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float speed;
    public float duration;
    Rigidbody2D rb;
    CircleCollider2D circleCollider2D;
    [HideInInspector]
    public Player owner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb.velocity = transform.right * speed;
        LeanTween.scale(gameObject, Vector2.zero, duration).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(true);
        GetComponent<SpriteRenderer>().color = owner.GetComponent<SpriteRenderer>().color;
        Physics2D.IgnoreCollision(owner.circleCollider2D, circleCollider2D);
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject != owner.gameObject)
            {
                other.gameObject.GetComponent<Player>().Die();
            }
        }
    }
}
