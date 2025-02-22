using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentScore { get; private set; }
    public int highScore { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadHighScore();
        ResetGame();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }
    }


    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ResetGame()
    {
        currentScore = 0;
    }
}
