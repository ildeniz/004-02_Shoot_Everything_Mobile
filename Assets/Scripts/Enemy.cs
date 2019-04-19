// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float sfxVolume;
    float shotCounter;

    // if there will be a seperte projectile script
    /* 
    float projectileSpeed;
    */

    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Projectile Stats")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Sound & Visual Effects")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        sfxVolume = PlayerPrefsController.GetSFXVolume();
    }
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot(); // randomized enemy shooting for organic behaviour
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        // if there will be a seperate projectile script
        /* 
        projectileSpeed = projectile.GetComponent<Projectile>().GetProjectileSpeed();
        */

        GameObject laser = Instantiate(projectile,
                transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, transform.position, sfxVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

        // if there will be a seperate projectile script
        /*
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (!projectile) { return; }
        ProcessHit(projectile);
        */
    }

    // if there will be a seperate projectile script
    /*
    private void ProcessHit(Projectile projectile)
    {
        health -= projectile.GetDamage();
        projectile.Hit();

        if (health <= 0 && !isBoss)
        {
            Die();
            //FindObjectOfType<SceneLoader>().LoadGameOver();
        }
        else
        {
            Die();
            FindObjectOfType<SceneLoader>().LoadGameOver();
            //winLabel.SetActive(true); // put this in SceneLoader.cs
            //Time.timeScale = 0f;
        }
    }
    */

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        GameObject explosion = Instantiate(explosionVFX,
                        transform.position,
                        Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, sfxVolume);
    }
}
