using UnityEngine;

public class Bin : MonoBehaviour
{
    private GameManager gameManager; // akses GameManager Script
    public string tagSejenis; // ambil tag gameObject Trash

    void Start() // Awal mulai langsung jalankan
    {
        // Agar bisa akses GameManager dan update beberapa nilai
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Aktif ketika terjadi sentuhan antar 2 gameObject
    private void OnTriggerEnter(Collider other)
    {
        // Jika TrashBin bersentuhan dengan tag gameObject yang sejenis
        if (other.CompareTag(tagSejenis))
        {
            Debug.Log("Benar: " + other.name);

            // Akses poin dari object trash
            ObjectTrash trash = other.GetComponent<ObjectTrash>();
            gameManager.UpdateScore(trash.pointValue);
            Destroy(other.gameObject);
        }
        else // Jika bersentuhan namun dengan tag gameObject yang tidak sejenis
        {
            Debug.Log("Salah: " + other.name);
            gameManager.UpdateScore(-5);
            Destroy(other.gameObject);
        }
    }
}
