using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    public Button resumeButton;
    public Text pausedtext;
    public Text scoreText;

    //public GameObject gameOverUi;

    private int score = 0;
    private void Start()
    {
        pausePanel.SetActive(false); // 시작할 때 딱 한 번만 꺼줌
        resumeButton.onClick.AddListener(OnClickResume);
        SetUpdateScore(0);
    }
    public void OnEnable()
    {
       
       
    }
    public void OnClickResume()
    {
        TogglePause(); // 다시 게임 진행
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
