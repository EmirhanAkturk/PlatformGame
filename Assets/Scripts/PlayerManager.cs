using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : MonoBehaviour
{
    public float health, bulletSpeed, fireFrequency = 1f, nextFireTime;

    bool dead = false;

    Transform muzzle;

    public Transform bullet, floatingText, bloodParticle;

    public Slider slider;

    bool mauseIsNotOverUI;

    // Start is called before the first frame update
    void Start()
    {
        muzzle = transform.GetChild(1);
        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        mauseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;
        
        if (CrossPlatformInputManager.GetButtonDown("Fire") && (nextFireTime < Time.timeSinceLevelLoad))
        {
            nextFireTime = Time.timeSinceLevelLoad + fireFrequency;

            ShootBullet();
            
        }
    }

    public void GetDamage(float damage)
    {
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();

        if( (health - damage) >= 0)
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
        if(health <=0)
        {
            Destroy(Instantiate(bloodParticle, transform.position, Quaternion.identity),3f);
            DataManager.Instance.LoseProcess();
            dead = true;
            Destroy(gameObject);
        }
    }

    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        DataManager.Instance.ShotBullet++;
    }

}
