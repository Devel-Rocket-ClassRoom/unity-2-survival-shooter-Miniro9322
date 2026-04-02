using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    [SerializeField]
    private UiManager uiManager;
    public bool isGameEnd {  get; private set; }
    public bool isPaused { get; private set; }

    private float restartTime = 3f;

    private void Awake()
    {
        score = 0;
        isGameEnd = false;
        restartTime = 3f;
    }

    private void Update()
    {
        if(isGameEnd == true)
        {
            restartTime -= Time.deltaTime;
            if(restartTime <= 0)
            {
                var name = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(name);
            }
        }
    }

    public void AddScore(int add)
    {
        score += add;

        uiManager.SetScoreText(score);
    }

    public void UpdateHpBar(float hpPercent)
    {
        uiManager.SetHpBar(hpPercent);
    }

    public void UpdatePauseUI(bool pause)
    {
        isPaused = pause;
        uiManager.GamePause(pause);
    }

    public void OpenGameoverUI(bool isDeath)
    {
        isGameEnd = isDeath;
        uiManager.GameOver(isDeath);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Debug.Log(isPaused);
        uiManager.GamePause(false);
    }

    public void VolumeControl(float volume)
    {
        Camera.main.GetComponent<AudioSource>().volume = volume;
    }
}