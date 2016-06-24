using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public Text scoreboard;
    public Player player; 
    private int score = 0;

	// Use this for initialization
	void Start () {
        scoreboard = GetComponent<Text>();
	}
	
    // Called by Ship to increase the death count. Player also calls this, but is dead anyway. //
	public void IncreaseScore(int amount)
    {
        score = score + amount;
        scoreboard.text = "SCORE: " + score.ToString();

        if(score % 100 == 0)
        {
            player.health++;
        }
    }
}
