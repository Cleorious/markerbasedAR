using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Projectile projectilePrefab;
    public Transform levelContainer;

    [HideInInspector]
    public GameManager gameManager;

    private List<Enemy> activeEnemies;

    private bool gameRunning;

    public void Start()
    {
        activeEnemies = new List<Enemy>();
        
        //instantiate 3 enemies at random locations within square boundary of the marker image
        for (int i = 0; i < Parameter.ENEMY_COUNT; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, levelContainer);
            Vector3 randomLocalPos = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(0f, 5f));
            enemy.Init(this, randomLocalPos);
            activeEnemies.Add(enemy);
        }
    
        gameRunning = true;
    }
    
    public void Update()
    {
        if (gameRunning)
        {
            if (activeEnemies.Count == 0)
            {
                gameRunning = false;
                //win level
                Debug.Log("win");
            }
    
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ShootProjectile();
            }
        }
    }

    public void Init(GameManager gameManager)
    {
        activeEnemies = new List<Enemy>();
        
        //instantiate 3 enemies at random locations within square boundary of the marker image
        for (int i = 0; i < Parameter.ENEMY_COUNT; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, levelContainer);
            Vector3 randomLocalPos = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(0f, 5f));
            enemy.Init(this, randomLocalPos);
            activeEnemies.Add(enemy);
        }

        gameRunning = true;
    }

    public void KillEnemy(Enemy enemy)
    {
        if (activeEnemies.Contains(enemy) && gameRunning)
        {
            activeEnemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    }

    public void ShootProjectile()
    {
        if (gameRunning)
        {
            Camera mainCam = Camera.main;

            Projectile projectile = Instantiate(projectilePrefab, mainCam.transform.position, Quaternion.identity);
            projectile.Init(mainCam.transform.forward);
        }

    }

    void DoUpdate()
    {
        if (gameRunning)
        {
            if (activeEnemies.Count == 0)
            {
                gameRunning = false;
                //win level
                Debug.Log("win");
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                ShootProjectile();
            }
        }
    }
}
