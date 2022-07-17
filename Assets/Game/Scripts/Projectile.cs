using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Update is called once per frame
    float currLifetime;

    bool launched;

    Vector3 direction;
    
    public void Init(Vector3 direction)
    {
        currLifetime = 0;
        launched = true;
        this.direction = direction;
    }
    
    void Update()
    {
        if (launched)
        {
            transform.position = transform.position + (direction * Parameter.BULLET_SPEED * Time.deltaTime);
            currLifetime += Time.deltaTime;

            if (currLifetime >= Parameter.BULLET_LIFETIME)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
