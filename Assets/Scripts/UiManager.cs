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

    public void GameOver(bool pause)
    {
        GameOverUi.SetActive(pause);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
