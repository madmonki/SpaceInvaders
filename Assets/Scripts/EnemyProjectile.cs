using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float speed = 3.5f;

    
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}
