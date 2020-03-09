using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneWeapon : WeaponBase
{
    public GameObject chargeCanvas;
    public Image chargeBar;
    float chargeAmount;
    public AudioClip smallYell;
    public AudioClip mediumYell;
    public AudioClip bigYell;
    public AudioSource audioSource;

    protected override void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0) cooldownTimer = 0;

        // Holding down charge
        if (cooldownTimer == 0 && Input.GetButton("P" + player.playerNum + "_Fire"))
        {
            if (!chargeCanvas.activeSelf) chargeCanvas.SetActive(true);
            chargeAmount += (Time.deltaTime / 2f);
            if (chargeAmount > 1) chargeAmount = 1;
            chargeBar.fillAmount = chargeAmount;
        }

        // Releasing charge
        else
        {
            if (chargeAmount > 0)
            {
                if (chargeAmount < 0.5f)
                {
                    CameraShakeHandler.instance.AddIntensity(0.25f);

                    Blast newBlast = Instantiate(blastPrefab, new Vector2(blastSpawnPoint.position.x, blastSpawnPoint.position.y), Quaternion.identity).GetComponent<Blast>();
                    newBlast.owner = player;
                    newBlast.duration = 0.75f;
                    newBlast.transform.rotation = player.transform.rotation;
                    newBlast.transform.localScale *= Mathf.Clamp(chargeAmount, 0.25f, 1f);

                    audioSource.clip = smallYell;
                    audioSource.Play();
                }
                else if (chargeAmount >= 0.5f && chargeAmount < 1f)
                {
                    CameraShakeHandler.instance.AddIntensity(1f);

                    Blast newBlast = Instantiate(blastPrefab, new Vector2(blastSpawnPoint.position.x, blastSpawnPoint.position.y), Quaternion.identity).GetComponent<Blast>();
                    newBlast.owner = player;
                    newBlast.duration = 1f;
                    newBlast.transform.rotation = player.transform.rotation;
                    newBlast.transform.localEulerAngles = new Vector3(newBlast.transform.localEulerAngles.x, newBlast.transform.localEulerAngles.y, newBlast.transform.localEulerAngles.z - 20);
                    newBlast.transform.localScale *= chargeAmount;

                    Blast newBlast2 = Instantiate(blastPrefab, new Vector2(blastSpawnPoint.position.x, blastSpawnPoint.position.y), Quaternion.identity).GetComponent<Blast>();
                    newBlast2.owner = player;
                    newBlast2.duration = 1f;
                    newBlast2.transform.rotation = player.transform.rotation;
                    newBlast2.transform.localScale *= chargeAmount;

                    Blast newBlast3 = Instantiate(blastPrefab, new Vector2(blastSpawnPoint.position.x, blastSpawnPoint.position.y), Quaternion.identity).GetComponent<Blast>();
                    newBlast3.owner = player;
                    newBlast3.duration = 1f;
                    newBlast3.transform.rotation = player.transform.rotation;
                    newBlast3.transform.localEulerAngles = new Vector3(newBlast3.transform.localEulerAngles.x, newBlast3.transform.localEulerAngles.y, newBlast3.transform.localEulerAngles.z + 20);
                    newBlast3.transform.localScale *= chargeAmount;

                    audioSource.clip = mediumYell;
                    audioSource.Play();
                }
                else
                {
                    CameraShakeHandler.instance.AddIntensity(2.5f);

                    for (int i = 0; i < 8; i++)
                    {
                        Blast newBlast = Instantiate(blastPrefab, new Vector2(blastSpawnPoint.position.x, blastSpawnPoint.position.y), Quaternion.identity).GetComponent<Blast>();
                        newBlast.owner = player;
                        newBlast.duration = Random.Range(1f, 1.5f);
                        newBlast.speed = Random.Range(12f, 18f);
                        newBlast.transform.rotation = player.transform.rotation;
                        newBlast.transform.localEulerAngles = new Vector3(newBlast.transform.localEulerAngles.x, newBlast.transform.localEulerAngles.y, newBlast.transform.localEulerAngles.z + Random.Range(-60f, 60f));
                        newBlast.transform.localScale *= chargeAmount;

                        audioSource.clip = bigYell;
                        audioSource.Play();
                    }
                }

                cooldownTimer = cooldownTimerLength;

                if (additiveKnockback)
                    player.rb.AddForce(-player.transform.right * soundBlastPower * chargeAmount, ForceMode2D.Impulse);
                else
                    player.rb.velocity = -player.transform.right * soundBlastPower * chargeAmount;
            }

            if (chargeCanvas.activeSelf) chargeCanvas.SetActive(false);
            chargeAmount = 0;
        }
    }
}
