using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNum;
    public int lifeCount;
    public float speed;
    public float speedLimit;
    public float jumpPower;
    public float rotationSpeed;

    public LayerMask groundLayerMask;

    Vector2 playerInput;
    bool grounded;

    public Transform spawnPointContainer;

    [HideInInspector]
    public Rigidbody2D rb;
    CircleCollider2D circleCollider2D;
    SpriteRenderer sr;
    WeaponBase weaponBase;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        weaponBase = GetComponentInChildren<WeaponBase>();
    }

    void Update()
    {
        // Get player input
        playerInput = new Vector2(Input.GetAxisRaw("P" + playerNum + "_Horizontal"), Input.GetAxisRaw("P" + playerNum + "_Vertical"));

        // Jump control
        grounded = Physics2D.OverlapCircle(transform.position, circleCollider2D.radius, groundLayerMask);

        if (grounded && playerInput.y == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            if (playerInput.x == 1) rotationSpeed = Mathf.Abs(rotationSpeed) * -1;
            else if (playerInput.x == -1) rotationSpeed = Mathf.Abs(rotationSpeed);
        }

        // Speed limit
        if (rb.velocity.x > speedLimit) rb.velocity = new Vector2(speedLimit, rb.velocity.y);
        else if (rb.velocity.x < -speedLimit) rb.velocity = new Vector2(-speedLimit, rb.velocity.y);

        // Rotation
        if (!grounded)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + (rotationSpeed * Time.deltaTime));
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(playerInput.x * speed, 0), ForceMode2D.Force);
    }

    public void Die()
    {
        GameObject newWeaponDebris = Instantiate(weaponBase.gameObject, weaponBase.transform.position, Quaternion.identity);
        newWeaponDebris.layer = 10;
        Destroy(newWeaponDebris.GetComponent<WeaponBase>());
        BoxCollider2D newBC = newWeaponDebris.AddComponent<BoxCollider2D>();
        Rigidbody2D newRB = newWeaponDebris.AddComponent<Rigidbody2D>();
        newRB.AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(5f, 10f)), ForceMode2D.Impulse);
        newRB.AddTorque(Random.Range(10f, 40f));
        gameObject.SetActive(false);

        lifeCount--;
        if (lifeCount == 0)
        {
            LeanTween.delayedCall(gameObject, 3, () =>
            {
                FadeHandler.instance.FadeOut("MainMenu", 1);
            });
        }
        else
        {
            LeanTween.delayedCall(gameObject, 2, () =>
            {
                int randomChildIndex = Random.Range(0, spawnPointContainer.childCount);
                transform.position = spawnPointContainer.GetChild(randomChildIndex).position;
                gameObject.SetActive(true);
            });
        }
    }
}
