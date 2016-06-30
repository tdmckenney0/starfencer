using UnityEngine;
using System.Collections;

public class StarFencer : MonoBehaviour {

    public string SceneToLoadOnDeath = "MainMenu";
    public string quit = "escape";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfPlayerWantsToQuit();
    }

    public void CheckIfPlayerWantsToQuit()
    {
        if (Input.GetKeyDown(quit))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoadOnDeath);
        }
    }
}
