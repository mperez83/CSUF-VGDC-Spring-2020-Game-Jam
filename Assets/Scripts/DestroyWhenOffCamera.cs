using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenOffCamera : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance.IsTransformOffCamera(transform))
        {
            Destroy(gameObject);
        }
    }
}
