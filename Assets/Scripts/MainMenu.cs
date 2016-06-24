using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public string ButtonName = "Jump";
    public string NextScene = "Game";

    // Update is called once per frame
    void Update()
    {
        bool isPressed = Input.GetButtonDown(ButtonName);

        if (isPressed)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
