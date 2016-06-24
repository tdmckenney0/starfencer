using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Ship {

    public Text healthText;

    [Header("Control Setup")]
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";
    public string shoot = "Jump";
    public string quit = "escape";

    [Header("Scene Management")]
    public string SceneToLoadOnDeath = "MainMenu";

    // Unity Callback Methods //

    void Start()
    {
        UpdateHealthText();
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

        //rb.transform.position = rb.transform.position + movement;

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

    public override void Destroy()
    {
        base.Destroy();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoadOnDeath);
    }
}
