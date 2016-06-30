using UnityEngine;
using UnityEngine.UI;
using System.Collections;

struct CameraBounds
{
    public float xMin, xMax, yMin, yMax;
}

public class Player : Ship {

    public Text healthText;
    public GameObject gameOverUI;

    [Header("Control Setup")]
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";
    public string shoot = "Jump";

    [Header("Scene Management")]
    public float screenPadding = 0.5f;

    private CameraBounds bounds;

    private bool gameOver = false;

    // Unity Callback Methods //

    void Start()
    {
        UpdateHealthText();
        SetScreenBounds();
    }

    void FixedUpdate()
    {
        Move();
        UpdateHealthText();
    }

    public new void Update()
    {
        base.Update();
        Shoot();
    }

    // Functions //
    
    public void UpdateHealthText()
    {
        healthText.text = "HEALTH: " + health.ToString();
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

    public void SetScreenBounds()
    {
        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist));

        bounds.xMax = topCorner.x - screenPadding;
        bounds.xMin = bottomCorner.x + screenPadding;
        bounds.yMax = topCorner.y - screenPadding;
        bounds.yMin = bottomCorner.y + screenPadding;
    }

    public void CheckScreenBounds()
    {
        Vector3 pos = transform.position;
            
        if(pos.x < bounds.xMin)
        {
            pos.x = bounds.xMin;
        }

        if (pos.x > bounds.xMax)
        {
            pos.x = bounds.xMax;
        }

        if (pos.y < bounds.yMin)
        {
            pos.y = bounds.yMin;
        }

        if (pos.y > bounds.yMax)
        {
            pos.y = bounds.yMax;
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
