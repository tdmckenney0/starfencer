using UnityEngine;
using UnityEngine.UI;
using System.Collections;

struct CameraBounds
{
    public float xMin, xMax, yMin, yMax;
}

public class Player : Ship {

    public Text healthText;

    [Header("Control Setup")]
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";
    public string shoot = "Jump";
    public string quit = "escape";

    [Header("Scene Management")]
    public string SceneToLoadOnDeath = "MainMenu";
    public float screenPadding = 0.5f;

    private CameraBounds bounds; 

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
        CheckIfPlayerWantsToQuit();
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

        // rb.transform.position = rb.transform.position + movement / 10;

        rb.AddForce(movement * speed); 
    }

    public void CheckIfPlayerWantsToQuit()
    {
        if (Input.GetKeyDown(quit))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoadOnDeath);
        }
    }

    public void Shoot()
    {
        if (Input.GetButtonDown(shoot))
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoadOnDeath);
    }
}
