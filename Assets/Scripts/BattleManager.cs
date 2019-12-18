using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private int playerHealth, score;
    private Text playerHealthText, scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        playerHealthText = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        //Showing score so that it's not empty
        scoreText.text = "Score: " + score;
        playerHealthText.text = "Health: " + playerHealth;
    }

    public void UpdateScore(int points)
    {
        //change the score
        score += points;
        //score = score + points;
        //update the text
        scoreText.text = "Score: " + score;
    }

    public void UpdatePlayerHealth(int healthChange)
    {
        //change health
        playerHealth += healthChange;
        //check if player is still alive
        if (playerHealth <= 0)
        {
            //GameOver
        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("Level2");
        }
        //update the text
        playerHealthText.text = "Health: " + playerHealth;
    }
}
