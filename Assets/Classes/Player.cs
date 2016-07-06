using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Ship {

    public static Player player;

    public Text healthText;
    public GameObject gameOverUI;

    [Header("Control Setup")]
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";
    public string shoot = "Jump";    

    private bool gameOver = false;

    // Unity Callback Methods //

    public override void Awake()
    {
        base.Awake();
        player = this;
    }

    public override void Start()
    {
        base.Start();
        UpdateHealthText();
        
    }

    void FixedUpdate()
    {
        Move();
        UpdateHealthText();
    }

    void Update()
    {
        Shoot();
    }

    // Functions //
    
    public void UpdateHealthText()
    {
        healthText.text = "HEALTH: " + curHealth.ToString();
    }

    public void Move()
    {
        float moveHorizontal = Input.GetAxis(xAxis);
        float moveVertical = Input.GetAxis(yAxis);

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        CheckScreenBounds();

        rb.AddForce(movement * speed); 
    }

    public void Shoot()
    {
        if (Input.GetButtonDown(shoot) && !gameOver)
        {
            FireBullet();
        }
    }

    public void CheckScreenBounds()
    {
        Vector3 pos = transform.position;

        if (pos.x < StarFencer.S.xMin)
        {
            pos.x = StarFencer.S.xMin;
        }

        if (pos.x > StarFencer.S.xMax)
        {
            pos.x = StarFencer.S.xMax;
        }

        if (pos.y < StarFencer.S.yMin)
        {
            pos.y = StarFencer.S.yMin;
        }

        if (pos.y > StarFencer.S.yMax)
        {
            pos.y = StarFencer.S.yMax;
        }

        transform.position = pos;
    }

    public override void Destroy()
    {
        base.Destroy();
        GameOver();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverUI.gameObject.SetActive(true);
    }
}
