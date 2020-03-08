using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float speed;
    public float duration;
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer sr;
    [HideInInspector]
    public Player owner;

    void Start()
    {
        rb.velocity = transform.right * speed;
        LeanTween.scale(gameObject, Vector2.zero, duration).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(true);
        sr.color = owner.sr.color;
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
