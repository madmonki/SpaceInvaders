using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPickups : MonoBehaviour
{
    public int seconds;

    void Update()
    {
        Destroy(gameObject, seconds);
    }
}
