using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Text pausetext;

    public Text pausedtext;

    public Text scoretext;


    public GameObject gameOverUI;
    public Text gameOverText;
    public Button restartButton;

    private int enemyLeft;

    private int score;

    public void Awake()
    {
        //gameOverUI.SetActive(false);
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame); // 버튼 클릭 시 함수 실행
    }
    public void UpdateAmmo(int magAmmo, int remainAmmo)
    {
      
    }

    public void AddScore(int amount)
    {
        score += amount;
        //if (scoreText != null)
        //{
        //    scoreText.text = $"SCORE: {score}";
        //}
    }

    //public void UpdateScore(int score)
    //{
    //    if (scoreText != null)
    //        scoreText.text = $"SCORE: {score}";
    //}

    //웨이브/적 수 갱신
    public void UpdateWave(int wave, int enemyLeft)
    {
    
    }

    //좀비 카운트
    public void SetEnemyCount(int count)
    {
        enemyLeft = count;
        UpdateWave(currentWave, enemyLeft);
    }

    public void EnemyDied()
    {
        enemyLeft--;
        if (enemyLeft < 0) enemyLeft = 0;
        UpdateWave(currentWave, enemyLeft);
    }

    private int currentWave;
    public void SetWave(int wave)
    {
        currentWave = wave;
        UpdateWave(currentWave, enemyLeft);
    }
    public void ShowGameOver()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
