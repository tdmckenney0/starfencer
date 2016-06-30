using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    protected Rigidbody2D rb;
    
    [Header("Ship Properties")]
    public int maxHealth = 1;
    public int curHealth;
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

    public virtual void Start()
    {
        curHealth = maxHealth;
    }

    // Ship methods

    public void TakeDamage(int damage)
    {
        curHealth = curHealth - damage;

        if(curHealth <= 0)
        {
            if (scoreboard != null)
            {
                scoreboard.IncreaseScore(pointsWorth);
            }

            Destroy();
        }
    }

    protected void FireBullet()
    {
        Instantiate(bullet, new Vector3(transform.position.x + xMuzzleOffset, transform.position.y + yMuzzleOffset), Quaternion.identity);
    }

    public virtual void Destroy()
    {
        this.gameObject.SetActive(false); print("Killed Dead: " + transform.name);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public bool IsShipWithinBounds()
    {
        Vector3 pos = transform.position;

        if (pos.x < StarFencer.S.xMin)
        {
            return false;
        }

        if (pos.x > StarFencer.S.xMax)
        {
            return false;
        }

        if (pos.y < StarFencer.S.yMin)
        {
            return false;
        }

        if (pos.y > StarFencer.S.yMax)
        {
            return false;
        }

        return true;
    }
}
