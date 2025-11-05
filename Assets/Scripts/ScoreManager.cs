using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public TMP_Text scoreText;
    public int currentScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateGUI()
    {
        scoreText.text = "Score: " + currentScore;
    }
}