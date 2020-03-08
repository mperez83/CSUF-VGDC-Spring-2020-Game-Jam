using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeHandler : MonoBehaviour
{
    public static CameraShakeHandler instance;

    Vector2 homePos;
    float intensity;

    void Start()
    {
        instance = this;
        homePos = transform.position;
    }

    void Update()
    {
        intensity -= Time.deltaTime;
        if (intensity < 0) intensity = 0;

        transform.position = new Vector3(homePos.x + Random.Range(-intensity, intensity), homePos.y + Random.Range(-intensity, intensity), transform.position.z);
    }

    public void SetIntensity(float newIntensity)
    {
        intensity = newIntensity;
    }
}
