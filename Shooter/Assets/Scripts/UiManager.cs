using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text pausetext;
    public Text pausedtext;
    public Text scoretext;

    public GameObject gameOverUi;

    private int score = 0;

    public void OnEnable()
    {
        SetUpdateScore(0);
        
        SetActiveGameOverUi(false);
    }
   
    public void SetUpdateScore(int score)
    {
        //scoreText.text = $"Score:{score:N0}";
    }
    public void AddScore(int value)
    {
        score += value;
        SetUpdateScore(score);
    }


    public void SetActiveGameOverUi(bool active)
    {
        gameOverUi.SetActive(active);
    }

    public void OnClickRestart()
    {

    }
}
