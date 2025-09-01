using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    public Button resumeButton;
    public Button quitButton;
    public Text pausedtext;
    public Text scoreText;


    public Button soundButton;          // 사운드 ON/OFF 버튼
    public Sprite soundOnSprite;        // ON 이미지
    public Sprite soundOffSprite;       // OFF 이미지
    public AudioSource bgmAudioSource;

    private bool isSoundOn = true;

    private int score = 0;
    private void Start()
    {
        pausePanel.SetActive(false); // 시작할 때 딱 한 번만 꺼줌
        resumeButton.onClick.AddListener(OnClickResume);
        quitButton.onClick.AddListener(OnClickExit);
        soundButton.onClick.AddListener(OnClickSoundToggle);
        
        SetUpdateScore(0);
    }
    public void OnEnable()
    {
       
       
    }
    public void OnClickResume()
    {
        TogglePause(); // 다시 게임 진행
    }
    public void OnClickSoundToggle()
    {
        isSoundOn = !isSoundOn;

        if (isSoundOn)
        {
            bgmAudioSource.Play();
            soundButton.image.sprite = soundOnSprite;
        }
        else
        {
            bgmAudioSource.Pause(); // 완전히 정지하려면 Stop()
            soundButton.image.sprite = soundOffSprite;
        }
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
    public void OnClickExit()
    {
        
        Application.Quit(); // 빌드된 게임에서는 프로그램 종료됨
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
