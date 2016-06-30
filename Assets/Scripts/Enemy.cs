using UnityEngine;
using System.Collections;

public class Enemy : Ship {
    
    public int chanceOfTakingTheShot = 10;
    public float secondsBetweenPossibleShots = 1f;
    public float bobDistance = 1f;

    [Header("1/x chance of rushing: ")]
    public int chanceToRushPlayer = 60;
    public bool currentlyRushingPlayer = false;
    public AudioClip rushSound;
    public Vector3 exhaustOffset;
    public GameObject exhaust;

    private AudioSource aud;

    private Vector3 iniPos;
    private bool movRight = false;

    // Unity Callbacks //

    public override void Start () {

        base.Start();

        iniPos = transform.position;

        aud = GetComponent<AudioSource>();

        InvokeRepeating("Shoot", secondsBetweenPossibleShots, secondsBetweenPossibleShots);
        InvokeRepeating("ChanceToRushPlayer", 1f, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            // Spawn Explosion Object
            Instantiate(deathAnimation, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);

            // Destroy this GameObject.
            Destroy();
            other.gameObject.GetComponent<Ship>().Destroy();
        }
    }

    void FixedUpdate()
    {
        Move();

        if(!IsShipWithinBounds())
        {
            Destroy();
            transform.position = Vector3.zero;
        }
    }

    // Controller Methods //

    void Shoot()
    {
        if (Random.Range(0, chanceOfTakingTheShot) == 0 && currentlyRushingPlayer == false && IsActive())
        {
            base.FireBullet();
        }
    }

    void ChanceToRushPlayer()
    {
        float x = Random.Range(0, chanceToRushPlayer);

        if (x == 0 )
        {
            currentlyRushingPlayer = true; print("Currently Rushing Player: " + transform.name);
            Vector3 movement = new Vector3(0.0f, -1.0f);
            rb.AddForce(movement * speed);

            GameObject flame = Instantiate(exhaust);

            flame.transform.SetParent(this.transform);
            flame.transform.position = this.transform.position + exhaustOffset;

            aud.clip = rushSound;
            aud.Play();
        }
    }

    void Move()
    {
        if(movRight)
        {
            transform.Translate(Vector3.right * thrusterSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector3.right * thrusterSpeed * Time.deltaTime);
        }

        if(transform.position.x <= (iniPos.x - bobDistance))
        {
            movRight = true;
        } 

        if(transform.position.x >= (iniPos.x + bobDistance))
        {
            movRight = false;
        }
    }

}
