using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Image HpBar;
    [SerializeField]
    private GameObject PauseUI;
    [SerializeField]
    private GameObject GameOverUi;

    private void OnEnable()
    {
        SetScoreText(0);
        PauseUI.SetActive(false);
        GameOverUi.SetActive(false);
        HpBar.fillAmount = 1f;
    }

    public void SetScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void SetHpBar(float hpPercent)
    {
        HpBar.fillAmount = hpPercent;
    }

    public void GamePause(bool pause)
    {
        if(pause == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        PauseUI.SetActive(pause);
    }

    public void GameOver(bool death)
    {
        GameOverUi.SetActive(death);
    }
}
