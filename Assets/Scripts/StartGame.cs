using UnityEngine;

public class StartGame : MonoBehaviour
{
    public int initialTimer;

    void Start()
    {
        GameManager.Instance.winPanel = GameObject.Find("Win");
        GameManager.Instance.gameOverPanel = GameObject.Find("Lose");
        GameManager.Instance.gameOverPanel.SetActive(false);
        GameManager.Instance.winPanel.SetActive(false);
        GameManager.Instance.scoreText = GameObject.Find("Score").GetComponent<TMPro.TextMeshProUGUI>();
        if (GameObject.Find("Timer") != null)
            GameManager.Instance.timerText = GameObject.Find("Timer").GetComponent<TMPro.TextMeshProUGUI>();
        GameManager.Instance.score = 0;
        GameManager.Instance.timer = initialTimer;
        GameManager.Instance.lives = 3;
        Time.timeScale = 1;
        StartCoroutine(FindAnyObjectByType<InsectSpawner>().Spawn());
    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
