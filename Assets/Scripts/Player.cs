using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    float sfxVolume;
    float shotCounter;

    #region if there will be a seperate projectile script
    /* 
    float projectileSpeed;
    */
    #endregion

    [Header("Player")]
    //SerializeField] float offset = 1f;
    [SerializeField] float health = 200f;
    [SerializeField] float waitForDOT = 2f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;

    [Header("Projectile Stats")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20f; //TODO prepare a seperate laser script
    [SerializeField] float projectileFiringPeriod = 0.1f; //TODO prepare a seperate laser script

    // Use this for initialization
    void Start()
    {
        shotCounter = projectileFiringPeriod;
        sfxVolume = PlayerPrefsController.GetSFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = projectileFiringPeriod;
        }
    }

    private void Fire()
    {
#region if there will be a seperate projectile script
        /* 
        projectileSpeed = projectile.GetComponent<Projectile>().GetProjectileSpeed();
        */
#endregion
        GameObject laser = Instantiate(projectile,
                transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed); //TODO prepare a seperate laser script
        AudioSource.PlayClipAtPoint(shootSound, transform.position, sfxVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

#region if there will be a seperte projectile script
        /* 
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (!projectile) { return; }
        ProcessHit(projectile);
        */
#endregion
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        GameObject otherGameobject = damageDealer.gameObject;

        if (!otherGameobject.GetComponent<BossBehaviour>())
        {
            damageDealer.Hit();
        }
        else if (otherGameobject.GetComponent<BossBehaviour>())
        {
            health = 0f;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    #region if there will be a seperate projectile script
    /* 
    private void ProcessHit(Projectile projectile)
    {
        health -= projectile.GetDamage();
        projectile.Hit();
        if (health <= 0)
        {
            Die();
        }
    }
    */
    #endregion

    private void Die()
    {
        FindObjectOfType<LevelController>().PlayerDied();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, sfxVolume);
    }

    public float GetHealth()
    {
        return Mathf.Max(health, 0f); //health;
    }
}
