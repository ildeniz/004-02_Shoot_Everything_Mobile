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

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f; //TODO prepare a seperate laser script
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    //float xMin;
    //float xMax;
    //float yMin;
    //float yMax;
    //float diffXPos;
    //float diffYPos;

    // Use this for initialization
    void Start () {
        //SetUpMoveBoundaries();
    }

   

    // Update is called once per frame
    void Update () {
        Fire();
    }

    /*void OnMouseDown()
    {
        var mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        diffXPos = transform.position.x - mousePosX;
        diffYPos = transform.position.y - mousePosY;
    }

    void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Clamp(mousePos.x + diffXPos, xMin, xMax), Mathf.Clamp(mousePos.y + diffYPos, yMin, yMax));
    }*/

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            // StopAllCoroutines(); // NOTE This stops all coroutines so we need a different approach such as below
            StopCoroutine(firingCoroutine); // we just stoppped the handled coroutines
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            
            GameObject laser = Instantiate(laserPrefab, 
                transform.position, 
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed); //TODO prepare a seperate laser script
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
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

    /*private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + offset;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - offset;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + offset;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - offset;
    }*/
}
