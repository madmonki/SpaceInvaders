using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float bulletDeactivatePos;
    void Start()
    {
        // Destroy(gameObject, seconds);
    }

    void Update()
    {
        if (transform.position.y > bulletDeactivatePos || transform.position.y < -bulletDeactivatePos)
            gameObject.SetActive(false);
    }
}
