using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text killText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTimeText;

    private float elapsed;
    private int kills;
    private bool isOver;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        UpdateHud();
    }

    private void Update()
    {
        if (isOver) return;
        elapsed += Time.deltaTime;
        UpdateHud();
    }

    public void AddKill()
    {
        if (isOver) return;
        kills++;
        UpdateHud();
    }

    public void GameOver()
    {
        if (isOver) return;
        isOver = true;
        Time.timeScale = 0f;
        finalScoreText.text = kills.ToString();
        finalTimeText.text = FormatTime(elapsed);
        gameOverPanel.SetActive(true);
    }

    public void Restart()  
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHud()
    {
        timerText.text = FormatTime(elapsed);
        killText.text = kills.ToString();
    }

    private static string FormatTime(float t)
    {
        int m = (int)(t / 60f);
        int s = (int)(t % 60f);
        return $"{m:00}:{s:00}";
    }
}