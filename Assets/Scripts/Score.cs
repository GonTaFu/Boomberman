using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI Text element to display the score
                                      // private int totalscore = 0; // Player's score

    
    void Start()
    {
        // Initialize the score text
        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned in the inspector.");
            return;
        }
        scoreText.text = "Score: " + GameManager.Instance.totalscore.ToString();
    }

    public void AddScore(int score)
    {
        GameManager.Instance.totalscore += score;
        scoreText.text = "Score: " + GameManager.Instance.totalscore.ToString();
        // Update the UI or any other necessary components
        Debug.Log("Score added: " + score);
    }
}
