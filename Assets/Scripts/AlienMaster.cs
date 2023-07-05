using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    private Vector3 hMovDistance = new Vector3(0, 0, 0);
    private Vector3 vMoveDistance = new Vector3(0, 0, 0);
    private const float MAX_LEFT = -2;
    private const float MAX_RIGHT = 2;
    private const float MAX_MOVE_SPEED = 0.02f;
    public static List<GameObject> allAliens = new List<GameObject>();
    private bool movingRight;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;

    void Start()
    {
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
        moveTimer -= Time.deltaTime;
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
            if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
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
        // timer
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
