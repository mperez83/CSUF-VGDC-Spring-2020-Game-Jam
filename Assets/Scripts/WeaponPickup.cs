using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    BoxCollider2D bc;
    Rigidbody2D rb;
    WeaponBase wb;

    void Start()
    {
        gameObject.layer = 10;
        LeanTween.delayedCall(gameObject, 0.25f, () =>
        {
            gameObject.layer = 0;
        });
        wb = gameObject.GetComponent<WeaponBase>();
        bc = gameObject.AddComponent<BoxCollider2D>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        wb.enabled = false;
        rb.AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(5f, 10f)), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(10f, 40f));
        gameObject.AddComponent<DestroyWhenOffCamera>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().GiveWeapon(wb.weaponIndex);
            Destroy(gameObject);
        }
    }
}
