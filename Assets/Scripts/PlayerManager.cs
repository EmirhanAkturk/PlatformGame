using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health;

    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        if( (health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }

        AmIDead();
    }

    void AmIDead()
    {
        if(health <=0)
        {
            dead = true;
        }
    }

}
