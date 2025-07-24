using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int difficulty;

    void Start() // Awalan dijalankan
    {
        button = GetComponent<Button>(); // akses UI Level Button
        button.onClick.AddListener(SetDifficulty); // EventListener ketika klik UI Level Buttons

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void SetDifficulty() // Atur level kesulitan
    {
        Debug.Log(gameObject.name + " button clicked");
        gameManager.StartGame(difficulty); // Jalankan function StartGame dari script GameManager.cs
    }
}
