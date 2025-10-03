using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score;
    public float timer = 60;
    public int lives = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public RawImage playerSprite;
    public Texture playerSpriteNormal;
    public Texture playerSpriteDamaged;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (lives <= 0)
        {
            GameOver();
        }

        if (timer <= 0)
        {
            Win();
        }

        if (timerText != null)
        {
            timerText.text = "Tempo: " + timer.ToString("F0");
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag("Mariposa|Borboleta"))
                {
                    Destroy(hit.collider.gameObject);
                    AddScore(1);
                }
                if (hit.collider != null && hit.collider.CompareTag("Vespa"))
                {
                    StartCoroutine(ChangeSprite());
                    Destroy(hit.collider.gameObject);
                    TakeDamage(1);
                }
            }
        }
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void Win()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        Time.timeScale = 0;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Pontuação: " + score.ToString();
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
    }

    IEnumerator ChangeSprite()
    {
        playerSprite.texture = playerSpriteDamaged;
        yield return new WaitForSeconds(.5f);
        playerSprite.texture = playerSpriteNormal;
    }
}
