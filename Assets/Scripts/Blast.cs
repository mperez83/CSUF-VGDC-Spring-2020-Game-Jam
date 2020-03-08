using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    void Start()
    {
        LeanTween.scale(gameObject, Vector2.zero, 1).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(true);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Die();
        }
    }
}
