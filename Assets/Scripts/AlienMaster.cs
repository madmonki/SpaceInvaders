using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool = null;
    public GameObject bulletPrefab;
    [SerializeField] Player _playerSc;
    private float width;
    private Vector3 hMovDistance = new Vector3(0.05f, 0, 0);
    private Vector3 vMoveDistance = new Vector3(0, 0.15f, 0);
    // private const float MAX_LEFT = -2.5f;
    // private const float MAX_RIGHT = 2.5f;
    private const float MAX_MOVE_SPEED = 0.02f;
    public static List<GameObject> allAliens = new List<GameObject>();
    private bool movingRight;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;
    private float shootTimer = 3f;
    private const float ShootTime = 3f;

    void Start()
    {
        width = _playerSc.width - 0.15f;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTimer <= 0)
        {
            MoveEnemies();
        }
        if (shootTimer <= 0)
        {
            Shoot();
        }
        moveTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        Vector2 pos = allAliens[UnityEngine.Random.Range(0, allAliens.Count)].transform.position;

        GameObject obj = objectPool.GetPoolObject();
        obj.transform.position = pos;

        shootTimer = ShootTime;
    }

    void MoveEnemies()
    {
        int hitMax = 0;
        for (int i = 0; i < allAliens.Count; i++)
        {
            if (movingRight)
            {
                allAliens[i].transform.position += hMovDistance;
            }
            else
            {
                allAliens[i].transform.position -= hMovDistance;
            }
            if (allAliens[i].transform.position.x > width || allAliens[i].transform.position.x < -width)
            {
                hitMax++;
            }
        }
        if (hitMax > 0)
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                allAliens[i].transform.position -= vMoveDistance;
            }
            movingRight = !movingRight;
        }
        moveTimer = GetMovedSpeed();
    }

    private float GetMovedSpeed()
    {
        float f = allAliens.Count * moveTime;

        if (f < MAX_MOVE_SPEED)
        {
            return MAX_MOVE_SPEED;
        }
        else
        {
            return f;
        }
    }
}
