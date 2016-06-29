using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    protected Rigidbody2D rb;
    
    [Header("Ship Properties")]
    public int health = 1;
    public float speed = 50.0f;
    public float thrusterSpeed = 5.0f;
    public int pointsWorth = 2;
    public Explosion deathAnimation;

    [Header("Scoreboard:")]
    public Score scoreboard = null;

    [Header("Ballistics Settings:")]
    public Bullet bullet;
    public float xMuzzleOffset = 0.0f;
    public float yMuzzleOffset = 0.0f;

    // Unity Callbacks

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update () {
        KillOnNoHealth();
    }

    // Ship methods

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }

    protected void FireBullet()
    {
        Instantiate(bullet, new Vector3(transform.position.x + xMuzzleOffset, transform.position.y + yMuzzleOffset), Quaternion.identity);
    }

    public virtual void Destroy()
    {
        Object.Destroy(gameObject);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    protected void KillOnNoHealth()
    {
        if(health <= 0)
        {
            if(scoreboard != null)
            {
                scoreboard.IncreaseScore(pointsWorth);
            }

            Destroy();
        }
    }
}
