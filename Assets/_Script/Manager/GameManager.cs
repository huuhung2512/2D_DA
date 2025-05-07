using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int playerScore { get; private set; }

    // Biến để theo dõi trạng thái pause
    public bool IsPaused { get; private set; } = false;


    private Vector3 lastCheckpointPosition = Vector3.zero;
    private bool hasCheckpoint = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        playerScore = 0;
        UIManager.Instance.UpdateScore(playerScore);
    }

    // Kiểm tra input để pause/unpause
    private void Update()
    {
        // Nếu nhấn phím Escape
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }



    // Phương thức pause game
    public void PauseGame()
    {
        if (!IsPaused)
        {
            Time.timeScale = 0f;
            IsPaused = true;
            UIManager.Instance.ShowPauseUI();
        }
    }

    // Phương thức tiếp tục game
    public void ResumeGame()
    {
        if (IsPaused)
        {
            Time.timeScale = 1f;
            IsPaused = false;
            UIManager.Instance.HideAllUI();
            UIManager.Instance.ShowGameHubUI();
        }
    }

    // Phương thức thoát game hoàn toàn
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Nếu đang chạy trên thiết bị thực
            Application.Quit();
#endif
    }

    // Các phương thức khác như cũ
    public void LoadLevel(int level)
    {
        if (IsPaused)
        {
            ResumeGame();
        }

        SaveScore();
        SceneManager.LoadScene(level);
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowGameHubUI();
        SoundManager.Instance.PlayMusic((GameEnum.EMusic)1);
    }

    public void AddScore(int points)
    {
        playerScore += points;
        Debug.Log($"Score increased by {points}. New score: {playerScore}");
        UIManager.Instance.UpdateScore(playerScore);
    }

    public void PlayerDie()
    {
        SaveScore();
        StartCoroutine(ShowGameOverAfterDelay(2f));
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIManager.Instance.ShowGameOverUI();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("LastScore", playerScore);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (playerScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }
        PlayerPrefs.Save();
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        hasCheckpoint = true;
    }

    // Reset player về checkpoint và hồi máu
    private void ResetToCheckpoint()
    {
        if (hasCheckpoint)
        {
            Player player = FindObjectOfType<Player>();
            player.Respawn(lastCheckpointPosition);
            // Reset máu của player
            Stats playerStats = player.GetComponentInChildren<Stats>();
            if (playerStats != null)
            {
                playerStats.Health.CurrentValue = playerStats.Health.MaxValue; // Giả sử Stats có phương thức reset máu
                PlayerHealthBar.Instance.UpdateHealthBar(); // Cập nhật UI máu
            }
            UIManager.Instance.ShowGameHubUI();
        }
    }

    public void RestartGame()
    {
        if (IsPaused) ResumeGame();

        if (hasCheckpoint)
        {
            ResetToCheckpoint(); // Reset về checkpoint nếu có
        }
        else
        {
            // Nếu không có checkpoint, reload scene
            playerScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            UIManager.Instance.ShowGameHubUI();
            PlayerHealthBar.Instance.UpdateHealthBar();
            UIManager.Instance.UpdateScore(playerScore);
            hasCheckpoint = false; // Reset checkpoint
        }
    }
    public void ResetScore()
    {
        playerScore = 0;
        PlayerHealthBar.Instance.UpdateHealthBar();
        UIManager.Instance.UpdateScore(playerScore);
    }

    public void ExitButton()
    {
        if (IsPaused)
        {
            ResumeGame();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UIManager.Instance.ShowGameStartUI();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            UIManager.Instance.HideAllUI();
        }
        else
        {
            UIManager.Instance.ShowGameHubUI();
        }
    }

    public void ReturnMainMenu()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        SceneManager.LoadScene(0);
        SoundManager.Instance.PlayMusic(GameEnum.EMusic.MusicIndex);
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowGameStartUI();
    }
}