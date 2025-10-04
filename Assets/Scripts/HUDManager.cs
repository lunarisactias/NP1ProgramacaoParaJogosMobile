using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Update()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("ModoNormal");
    }

    public void PlayFreeModeButton()
    {
        SceneManager.LoadScene("ModoLivre");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
