using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private ObjectPool motherShipObjectPool;
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

    public GameObject motherShipPrefab;
    private Vector3 motherShipSpawnPos = new Vector3(6,6.5f,0);
    private float motherShipTimer = 5f;
    private const float MOTHERSHIP_MIN = 15f;
    private const float MOTHERSHIP_MAX = 60f;

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
        if (motherShipTimer <= 0)
        {
            SpawnMotherShip();
        }
        moveTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
        motherShipTimer -= Time.deltaTime;
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

    private void SpawnMotherShip()
    {
        GameObject obj = motherShipObjectPool.GetPoolObject();
        obj.transform.position = motherShipSpawnPos;
        motherShipTimer = UnityEngine.Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);
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
