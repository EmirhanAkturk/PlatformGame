using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health, bulletSpeed;

    bool dead = false;

    Transform muzzle;

    public Transform bullet;
    // Start is called before the first frame update
    void Start()
    {
        muzzle = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)){

            ShootBullet();
        }
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

    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
    }

}
