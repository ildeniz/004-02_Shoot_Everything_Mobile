using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    //SerializeField] float offset = 1f;
    [SerializeField] float health = 200f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;

    [Header("Projectile Stats")]
    float shotCounter;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f; //TODO prepare a seperate laser script
    [SerializeField] float projectileFiringPeriod = 0.1f; //TODO prepare a seperate laser script

    Coroutine firingCoroutine;

    // Use this for initialization
    void Start()
    {
        shotCounter = projectileFiringPeriod;
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
        GameObject laser = Instantiate(laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed); //TODO prepare a seperate laser script
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
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

    private void Die()
    {
        FindObjectOfType<SceneLoader>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    public float GetHealth()
    {
        return Mathf.Max(health, 0f); //health;
    }
}
