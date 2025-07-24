using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kebutuhan UI
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Control game state
    public List<GameObject> trash; // daftar prefab sampah
    private float spawnRate = 2.5f;
    public bool isGameActive;
    private int currentDifficulty;

    // konsep elements
    private int score;
    public int lives;

    // UI elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI yourScoreText;

    // UI buttons
    public Button restartButton;
    public Button mainMenuButton;

    // UI texts
    public GameObject titleScreen;

    public void Start()
    {
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
    }

    public void StartGame(int difficulty)
    {
        Debug.Log("Game started with difficulty: " + difficulty);
        currentDifficulty = difficulty;
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTrash());
        score = 0;
        UpdateScore(0);
        UpdateLives();
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
    }

    IEnumerator SpawnTrash()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, trash.Count);
            Instantiate(trash[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false;

        // Nonaktifkan semua elemen UI yang tidak diperlukan
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);

        // Hancurkan semua sampah dengan tag: Organik, Anorganik, B3
        string[] trashTags = { "Organik", "Anorganik", "B3" };

        foreach (string tag in trashTags)
        {
            GameObject[] trashObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in trashObjects)
            {
                Destroy(obj);
            }
        }

        // Aktifkan elemen UI Game Over
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        yourScoreText.gameObject.SetActive(true);
        yourScoreText.text = "Your Score: " + score;
    }

    public void RestartGame()
    {
        // Restart game langsung dengan difficulty terakhir
        StopAllCoroutines(); // pastikan coroutine lama berhenti
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        yourScoreText.gameObject.SetActive(false);

        // Reset status & langsung mulai ulang
        score = 0;
        lives = 3;
        spawnRate /= currentDifficulty;
        scoreText.text = "Score: 0";
        livesText.text = "Lives: " + lives;
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);

        isGameActive = true;
        StartCoroutine(SpawnTrash());
    }

    public void ReturnToMainMenu()
    {
        // Nonaktifkan elemen UI Game Over
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        yourScoreText.gameObject.SetActive(false);

        // Reset nilai dan status
        isGameActive = false;
        score = 0;
        lives = 3; // atau berapa pun nilai default lives kamu
        spawnRate = 2.5f;
        scoreText.text = "Score: 0";
        livesText.text = "Lives: " + lives;

        // Tampilkan kembali title screen
        titleScreen.gameObject.SetActive(true);
    }
}
