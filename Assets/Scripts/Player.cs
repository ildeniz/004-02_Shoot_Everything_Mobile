using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    float sfxVolume;
    float shotCounter;
    
    // if there will be a seperte projectile script
    /* 
    float projectileSpeed;
    */

    [Header("Player")]
    //SerializeField] float offset = 1f;
    [SerializeField] float health = 200f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;

    [Header("Projectile Stats")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20f; //TODO prepare a seperate laser script
    [SerializeField] float projectileFiringPeriod = 0.1f; //TODO prepare a seperate laser script

    Coroutine firingCoroutine;

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
        // if there will be a seperte projectile script
        /* 
        projectileSpeed = projectile.GetComponent<Projectile>().GetProjectileSpeed();
        */

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

        // if there will be a seperte projectile script
        /* 
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (!projectile) { return; }
        ProcessHit(projectile);
        */
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    //if there will be a seperte projectile script
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

    private void Die()
    {
        FindObjectOfType<SceneLoader>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, sfxVolume);
    }

    public float GetHealth()
    {
        return Mathf.Max(health, 0f); //health;
    }
}
