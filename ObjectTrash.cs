using UnityEngine;

public class ObjectTrash : MonoBehaviour
{
    // Komponen Pengatur Object Target
    private float minSpeed = 1; // Kecepatan minimum
    private float maxSpeed = 4; // Kecepatan maksimum
    private float zMinRange = -6.5f; // Rentang Z untuk spawn target
    private float zMaxRange = -10.5f; // Rentang Z untuk spawn target
    private float xSpawnPos = 15; // posisi Y untuk spawn target

    // Mendapatkan referensi ke GameManager.cs
    private GameManager gameManager; // Menamai variabel referensi GameManager
    public int pointValue; // pemberian points

    void Start() // Awalan dijalankan
    {
        transform.position = RandomSpawnPos(); // Menentukan posisi spawn acak pada target
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() // Function update terus untuk membuat gameObject Trash bergerak
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * Random.Range(minSpeed, maxSpeed));
    }

    Vector3 RandomSpawnPos() // Fungsi untuk mendapatkan posisi spawn acak
    {
        return new Vector3(xSpawnPos, 0, Random.Range(zMinRange, zMaxRange));
    }

    private void OnTriggerEnter(Collider other) // Fungsi menangani batas Sensor Trash
    {
        if (other.CompareTag("Sensor")) // Jika tag == "Sensor"
        {
            // Penanganan ketika keluar batas Sensor
            Debug.Log("Target keluar dari batas bawah" + gameObject.name);
            gameManager.lives = gameManager.lives - 1; // Lives -1
            gameManager.UpdateLives();
            Destroy(gameObject);
        }
    }
}
