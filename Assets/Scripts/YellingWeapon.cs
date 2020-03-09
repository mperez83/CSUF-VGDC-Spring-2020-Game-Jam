using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellingWeapon : WeaponBase
{
    int audioPool;

    public AudioClip[] michaelYells;
    public AudioClip[] taraYells;
    public AudioClip[] nickYells;
    public AudioClip[] emmersonYells;

    protected override void Start()
    {
        base.Start();
        audioPool = Random.Range(0, 4);
    }

    protected override void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0) cooldownTimer = 0;

        if (cooldownTimer == 0 && Input.GetButton("P" + player.playerNum + "_Fire"))
        {
            CameraShakeHandler.instance.AddIntensity(screenShake);
            cooldownTimer = cooldownTimerLength;

            if (additiveKnockback)
                player.rb.AddForce(-player.transform.right * soundBlastPower, ForceMode2D.Impulse);
            else
                player.rb.velocity = -player.transform.right * soundBlastPower;

            GameObject newBlast = Instantiate(blastPrefab, new Vector2(blastSpawnPoint.position.x, blastSpawnPoint.position.y), Quaternion.identity);
            newBlast.transform.rotation = player.transform.rotation;
            newBlast.transform.localEulerAngles = new Vector3(newBlast.transform.localEulerAngles.x, newBlast.transform.localEulerAngles.y, newBlast.transform.localEulerAngles.z + Random.Range(-degreeOffset, degreeOffset));
            newBlast.GetComponent<Blast>().owner = player;
            AudioSource newBlastAudio = newBlast.GetComponent<AudioSource>();
            switch (audioPool)
            {
                case 0:
                    newBlastAudio.clip = michaelYells[Random.Range(0, michaelYells.Length)];
                    break;
                case 1:
                    newBlastAudio.clip = taraYells[Random.Range(0, taraYells.Length)];
                    break;
                case 2:
                    newBlastAudio.clip = nickYells[Random.Range(0, nickYells.Length)];
                    break;
                case 3:
                    newBlastAudio.clip = emmersonYells[Random.Range(0, emmersonYells.Length)];
                    break;
            }
            newBlastAudio.pitch = Random.Range(0.8f, 1.2f);
        }
    }
}
