using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text gameScoreText;
    public TMP_Text gameOverScoreText;
    public TMP_Text highScoreText;
    public SFXManager sfxManager;

    public void CallGameOver()
    {
        gameOverScoreText.text = gameScoreText.text;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScore < int.Parse(gameScoreText.text))
        {
            highScore = int.Parse(gameScoreText.text);
        }

        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt("HighScore", highScore);

        sfxManager.PlayKnockedOutSound();
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("Life", 3);
        // Destroi todos os inimigos
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject inimigo in inimigos)
        {
            Destroy(inimigo);
        }

        // Reseta o score
        if (gameScoreText != null)
        {
            gameScoreText.text = "0";
        }

        // Chama o GameManager para spawnar um novo inimigo
        GameManager gameManager = FindObjectOfType<GameManager>();
        PlayerControler playerControler = FindObjectOfType<PlayerControler>();
        if (gameManager != null)
        {
            gameManager.SpawnRandomPrefab();
            playerControler.scoreToImproveSpeed = 500;
            gameManager.baseValue = 0.1f;
            Time.timeScale = 1;

            // Reseta os hearts
            if (gameManager.hearts != null)
            {
                foreach (GameObject heart in gameManager.hearts)
                {
                    if (heart != null)
                    {
                        Animator anim = heart.GetComponent<Animator>();
                        if (anim != null)
                        {
                            anim.SetBool("Hit", false);
                        }
                    }
                }
            }
        }
    }
}
