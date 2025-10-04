using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int score;
    public float timer = 60;
    public int lives = 3;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI[] highScoreText;
    public GameObject[] lifeIcons;

    [Header("Player Settings")]
    public RawImage playerSprite;
    public Texture playerSpriteNormal;
    public Texture playerSpriteDamaged;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip SwooshSound;
    public AudioClip HurtSound;


    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
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
            timerText.text = timer.ToString("F0");
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(SwooshSound);

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
                    audioSource.PlayOneShot(HurtSound);
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
        scoreText.text = score.ToString();
        if (highScoreText.Length > 0) {
            highScoreText[0].text = score.ToString();
            highScoreText[1].text = score.ToString();
        }
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(i < lives);
        }
    }

    IEnumerator ChangeSprite()
    {
        playerSprite.texture = playerSpriteDamaged;
        yield return new WaitForSeconds(.5f);
        playerSprite.texture = playerSpriteNormal;
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
