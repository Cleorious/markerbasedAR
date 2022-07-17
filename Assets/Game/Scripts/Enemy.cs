using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    

    [SerializeField] Transform healthBar;

    GameplayManager gameplayManager;
    
    //!runtime data
    int currHealth;
    
    public void Init(GameplayManager gameplayManager, Vector3 localPos)
    {
        this.gameplayManager = gameplayManager;
        currHealth = Parameter.ENEMY_DEFAULT_HEALTH;
        transform.localPosition = localPos;

        healthBar.transform.localScale = Vector3.one;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entered");
        if (other.tag == "bullet")
        {
            currHealth -= Parameter.BULLET_DAMAGE;
            float percentageHealth = (float)currHealth / Parameter.ENEMY_DEFAULT_HEALTH;
            healthBar.transform.localScale = new Vector3(percentageHealth, 1f, 1f);

            if (currHealth < 0)
            {
                gameplayManager.KillEnemy(this);
            }

            Projectile projectile = other.GetComponent<Projectile>();
            projectile.Kill();
        }
        
    }
}
