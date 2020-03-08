using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float soundBlastPower;
    public float cooldownTimerLength;
    float cooldownTimer;

    public GameObject blastPrefab;

    public Transform blastSpawnPoint;
    Player player;
    AudioSource audioSource;

    void Start()
    {
        player = GetComponentInParent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0) cooldownTimer = 0;

        if (cooldownTimer == 0 && Input.GetButton("P" + player.playerNum + "_Fire"))
        {
            audioSource.Play();
            CameraShakeHandler.instance.SetIntensity(0.1f);
            cooldownTimer = cooldownTimerLength;
            player.rb.velocity = -player.transform.right * soundBlastPower;
            GameObject newBlast = Instantiate(blastPrefab, new Vector2(blastSpawnPoint.position.x, blastSpawnPoint.position.y), Quaternion.identity);
            newBlast.transform.rotation = player.transform.rotation;
        }
    }
}
