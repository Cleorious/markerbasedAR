using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform levelContainer;

    GameManager gameManager;

    List<Enemy> activeEnemies;

    bool gameRunning;
    bool isWithinView;
    bool gameEnded;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        
        ResetLevel();
    }

    public void ResetLevel()
    {
        isWithinView = false;
        gameRunning = false;
        gameEnded = false;
        
        if (activeEnemies != null)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                Destroy(enemy.gameObject);
            }
        }
        
        activeEnemies = new List<Enemy>();
    }

    public void StartLevel()
    {
        //instantiate 3 enemies at random locations within square boundary of the marker image
        for (int i = 0; i < Parameter.ENEMY_COUNT; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, levelContainer);
            //!TODO: randomize localpos based on boundaries of the scanned image?
            Vector3 randomLocalPos = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
            enemy.Init(this, randomLocalPos);
            activeEnemies.Add(enemy);
        }

        gameEnded = false;
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
        if (gameRunning && isWithinView)
        {
            Camera mainCam = Camera.main;

            Projectile projectile = Instantiate(projectilePrefab, mainCam.transform.position, Quaternion.identity);
            projectile.Init(mainCam.transform.forward);
        }

    }

    public void DoUpdate(float dt)
    {
        if (gameRunning && isWithinView && !gameEnded)
        {
            if (activeEnemies.Count == 0)
            {
                //win level
                gameEnded = true;
                gameManager.EndGame();
                Debug.Log("win");
            }
        }
    }

    public void Hide()
    {
        transform.localScale = Vector3.zero;
        isWithinView = false;
    }

    public void Show()
    {
        transform.localScale = Vector3.one;
        isWithinView = true;

        if (!gameRunning)
        {
            StartLevel();
        }
    }
}
