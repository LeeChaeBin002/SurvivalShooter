using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public CanvasGroup gameOverCanvas;
    private float fadeDuration = 0.5f;// 페이드 인 시간
    private float waitTime = 2f;// 페이드 인 후 대기 시간
    private void Start()
    {
        gameOverCanvas.alpha = 0f;
    } 
    public void ShowGameOver()
    {
        StartCoroutine(FadeInAndRestart());
        //Time.timeScale = 0f; // 게임 일시정지
                            
    }
    private IEnumerator FadeInAndRestart()
    {
        float t = 0f;

        // 0 → 1 (0.5초 동안)
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            gameOverCanvas.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        // 알파값 확실히 1로 고정
        gameOverCanvas.alpha = 1f;

        // 1초 대기
        yield return new WaitForSecondsRealtime(waitTime);

        // 씬 재시작
        Time.timeScale = 1f; // 혹시 멈췄다면 되돌리기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
