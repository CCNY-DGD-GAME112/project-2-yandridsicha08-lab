using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("HUD (visible during gameplay)")]
    public TMP_Text ScoreText;
    public TMP_Text TimerText;

    [Header("Game Over Screen")]
    public GameObject GameOverPanel;
    public TMP_Text FinalScoreText;
    public TMP_Text FinalTimeText;

    private float timeElapsed = 0f;
    private bool gameOver = false;

    void Update()
    {
        if (gameOver) return;

        if (Time.timeScale == 0f)
        {
            gameOver = true;
            ShowGameOver();
            return;
        }

        timeElapsed += Time.deltaTime;

        ScoreText.text = "Score: " + Enemy.Score;
        TimerText.text = "Time: " + Mathf.FloorToInt(timeElapsed) + "s";
    }

    void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
        FinalScoreText.text = "Orbs Collected: " + Enemy.Score;
        FinalTimeText.text = "Time Survived: " + Mathf.FloorToInt(timeElapsed) + "s";
    }

    public void Restart()
    {
    Time.timeScale = 1f;
    Enemy.Score = 0;
    Enemy.GhostSpeed = 5f;
    // Restart the music
    if (Enemy.MusicSource != null)
    {
        Enemy.MusicSource.Stop();
        Enemy.MusicSource.Play();
    }
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
