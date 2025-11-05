using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -10)
        {
            ScoreManager.instance.AddHighScore("Player", ScoreManager.instance.currentScore);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
