using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float health;
    public float damage;

    bool colliderBusy = false;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.tag == "Player" && !colliderBusy)
        {
            colliderBusy = true;
            other.GetComponent<PlayerManager>().GetDamage(damage);

        }
        else if(other.tag == "Bullet")
        {
            GetDamage(other.GetComponent<BulletManager>().bulletDamage);
            Destroy(other.gameObject);
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        print("OnTriggerStay2D");
    }
    */

    private void OnTriggerExit2D(Collider2D other)
    {
        if ( other.tag == "Player" )
        {
            colliderBusy = false;
        }
    }

    public void GetDamage(float damage)
    {
        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }

        slider.value = health;
        AmIDead();
    }

    void AmIDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
