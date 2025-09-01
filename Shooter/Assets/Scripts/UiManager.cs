using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    public Text resumetext;
    public Text pausedtext;
    public Text scoreText;

    //public GameObject gameOverUi;

    private int score = 0;

    public void OnEnable()
    {
        SetUpdateScore(0);
        
        SetActiveGameOverUi(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 멈춤
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // 게임 재개
            pausePanel.SetActive(false);
        }
    }
    public void SetUpdateScore(int score)
    {
        scoreText.text = $"Score:{score:N0}";
    }
    public void AddScore(int value)
    {
        score += value;
        SetUpdateScore(score);
    }


    public void SetActiveGameOverUi(bool active)
    {
        pausePanel.SetActive(active);
    }

    public void OnClickRestart()
    {

    }
}
