using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float speed;
    public float duration;
    public int damage;
    bool canDamage = true;
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer sr;
    [HideInInspector]
    public Player owner;
    public AudioSource audioSource;

    void Start()
    {
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.Play();

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
        if (canDamage)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject != owner.gameObject)
                {
                    canDamage = false;
                    other.gameObject.GetComponent<Player>().TakeDamage(damage);
                }
            }
        }
    }
}
