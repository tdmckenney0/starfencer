using UnityEngine;
using System.Collections;

public class StarFencer : MonoBehaviour {

    public static StarFencer S;

    [Header("Scene Management")]
    public float screenPadding = 0.5f;
    public string SceneToLoadOnDeath = "MainMenu";
    public string quit = "escape";

    [Header("Camera Bounds")]
    public float xMin, xMax, yMin, yMax;

    void Awake()
    {
        S = this;
        SetScreenBounds();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfPlayerWantsToQuit();
    }

    public void SetScreenBounds()
    {
        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist));

        xMax = topCorner.x - screenPadding;
        xMin = bottomCorner.x + screenPadding;
        yMax = topCorner.y - screenPadding;
        yMin = bottomCorner.y + screenPadding;
    }

    public void CheckIfPlayerWantsToQuit()
    {
        if (Input.GetKeyDown(quit))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoadOnDeath);
        }
    }
}
