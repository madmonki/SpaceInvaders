using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public GameObject BulletPrefab;

    Camera cam;
    public float width;
    // private float speed = 3f;

    bool isShooting;
    // float coolDown = 0.5f;
    [SerializeField] private ObjectPool objectPool = null;

    public ShipStats shipStats;
    private Vector2 offScreenPos = new Vector2(0, -20);
    private Vector2 startPos = new Vector2(0, -6);
    private float dirX;

    private void Awake()
    {
        cam = Camera.main;
        width = ((1 / (cam.WorldToViewportPoint(new Vector3(1,1,0)).x - .5f) / 2) - 0.25f);
    }

    void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;
        transform.position = startPos;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);
    }

    
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > -width)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < width)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }        
        if (Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif

        dirX = Input.acceleration.x;
        if (dirX <= -0.1f && transform.position.x > -width)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        if (dirX >= 0.1f && transform.position.x < width)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
    }

    public void ShootButton()
    {
        if (!isShooting)
            StartCoroutine(Shoot());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPos;

        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxHealth;

        transform.position = startPos;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
    }

    private void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;
            UIManager.UpdateLives(shipStats.currentLives);

            if (shipStats.currentLives <= 0)
            {
                Debug.Log("Game Over");
            }
            else
            {
                StartCoroutine(Respawn());
            }
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;

        // Instantiate(BulletPrefab, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
        GameObject obj = objectPool.GetPoolObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStats.fireRate);

        isShooting = false;
    }
}
