using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerNum;
    public Color playerColor;
    public int maxHealth;
    int health;
    public int lifeCount;
    public float speed;
    public float speedLimit;
    public float jumpPower;
    public float rotationSpeed;
    bool freeAim = true;

    bool invincible;
    public float iFrameDuration;

    public LayerMask groundLayerMask;

    Vector2 playerInput;
    bool grounded;
    float playerAngle;
    float vel;

    public Transform spawnPointContainer;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public CircleCollider2D circleCollider2D;
    [HideInInspector]
    public SpriteRenderer sr;

    [HideInInspector]
    public WeaponBase weaponBase;

    public GameObject[] weaponPrefabs;

    public GridLayoutGroup UILifeCounter;
    public GameObject lifeImagePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        weaponBase = GetComponentInChildren<WeaponBase>();
        GiveWeapon(Random.Range(0, weaponPrefabs.Length));
        freeAim = GameManager.instance.freeAim;
        health = maxHealth;

        for (int i = 0; i < lifeCount; i++)
        {
            Image newLifeImage = Instantiate(lifeImagePrefab, UILifeCounter.transform).GetComponent<Image>();
            newLifeImage.color = sr.color;
            if (playerNum == 2) newLifeImage.transform.localScale = new Vector3(-newLifeImage.transform.localScale.x, newLifeImage.transform.localScale.y, newLifeImage.transform.localScale.z);
        }
    }

    void Update()
    {
        if (invincible) sr.enabled = !sr.enabled;

        // Get player input
        playerInput = new Vector2(Input.GetAxisRaw("P" + playerNum + "_Horizontal"), Input.GetAxisRaw("P" + playerNum + "_Vertical"));

        // Jump control
        grounded = Physics2D.OverlapCircle(transform.position, circleCollider2D.radius * 1.5f, groundLayerMask);
        if (grounded && playerInput.y == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        // Speed limit
        if (rb.velocity.x > speedLimit) rb.velocity = new Vector2(speedLimit, rb.velocity.y);
        else if (rb.velocity.x < -speedLimit) rb.velocity = new Vector2(-speedLimit, rb.velocity.y);

        // Rotation
        if (freeAim)
        {
            if (playerInput != Vector2.zero)
            {
                playerAngle = -(TrigUtilities.VectorToDegrees(playerInput) - 90);
            }
        }
        else
        {
            if (!grounded)
            {
                playerAngle -= rotationSpeed * Time.deltaTime;
                if (playerAngle < 0) playerAngle += 360;
            }
        }

        // Off-screen death
        if (GameManager.instance.IsTransformOffCamera(transform))
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        rb.rotation = Mathf.SmoothDampAngle(rb.rotation, playerAngle, ref vel, 0.05f);
        rb.AddForce(new Vector2(playerInput.x * speed, 0), ForceMode2D.Force);
    }



    public void GiveWeapon(int weaponIndex)
    {
        if (weaponBase != null) Destroy(weaponBase.gameObject);
        GameObject createdWeapon = Instantiate(weaponPrefabs[weaponIndex], transform);
        weaponBase = createdWeapon.GetComponent<WeaponBase>();
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            sr.color = Color.red;
            if (health <= 0)
            {
                Time.timeScale = 0;
                LeanTween.delayedCall(gameObject, 0.25f, () =>
                {
                    Time.timeScale = 1;
                    CameraShakeHandler.instance.AddIntensity(0.4f);
                    sr.color = playerColor;
                    Die();
                }).setIgnoreTimeScale(true);
            }
            else
            {
                Time.timeScale = 0;
                LeanTween.delayedCall(gameObject, 0.05f, () =>
                {
                    Time.timeScale = 1;
                    CameraShakeHandler.instance.AddIntensity(0.3f);
                    sr.color = playerColor;
                }).setIgnoreTimeScale(true);
            }
        }
    }

    public void Die()
    {
        if (gameObject.activeSelf)
        {
            GameObject newWeaponDebris = Instantiate(weaponBase.gameObject, weaponBase.transform.position, Quaternion.identity);
            newWeaponDebris.AddComponent<WeaponPickup>();

            gameObject.SetActive(false);

            lifeCount--;
            if (playerNum == 1)
            {
                LeanTween.scale(UILifeCounter.transform.GetChild(UILifeCounter.transform.childCount - 1).gameObject, Vector3.zero, 1f)
                .setEase(LeanTweenType.easeOutExpo)
                .setDestroyOnComplete(true);
            }
            else
            {
                LeanTween.scale(UILifeCounter.transform.GetChild(0).gameObject, Vector3.zero, 1f)
                .setEase(LeanTweenType.easeOutExpo)
                .setDestroyOnComplete(true);
            }
            
            if (lifeCount == 0)
            {
                LeanTween.delayedCall(gameObject, 3, () =>
                {
                    MatchHandler.instance.EndGame(playerNum);
                });
            }
            else
            {
                LeanTween.delayedCall(gameObject, 2, () =>
                {
                    int randomChildIndex = Random.Range(0, spawnPointContainer.childCount);
                    transform.position = spawnPointContainer.GetChild(randomChildIndex).position;
                    GiveWeapon(Random.Range(0, weaponPrefabs.Length));

                    invincible = true;
                    LeanTween.delayedCall(gameObject, iFrameDuration, () =>
                    {
                        invincible = false;
                        sr.enabled = true;
                    });

                    health = maxHealth;

                    gameObject.SetActive(true);
                });
            }
        }
    }
}
