using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Інтерфейс та Світ")]
    public TMP_Text scoreText;
    public GameObject winText;
    public GameObject exitPortal; // САМ ПОРТАЛ

    [Header("Звуки")]
    public AudioClip collectSound;
    public AudioClip stompSound;

    [Header("Налаштування бою")]
    public float bounceForce = 12f;
    public Transform groundCheck;

    private int lightScore = 0;
    private int totalCherries; // Змінна для підрахунку всіх сфер на рівні

    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        Time.timeScale = 1f; // Відновлюємо час
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // ДЕТЕКТИВ: Отримуємо список усіх знайдених сфер
        GameObject[] allSpheres = GameObject.FindGameObjectsWithTag("LightSphere");
        totalCherries = allSpheres.Length; // Записуємо їхню кількість

        // Просимо Unity вивести ім'я кожної знайденої сфери в Консоль
        foreach (GameObject sphere in allSpheres)
        {
            Debug.Log("ШПИГУН: Знайдено об'єкт з іменем [" + sphere.name + "] на координатах " + sphere.transform.position);
        }

        // 2. Ховаємо портал і текст перемоги на старті
        if (exitPortal != null) exitPortal.SetActive(false);
        if (winText != null) winText.SetActive(false);

        UpdateScoreText();
    }

    [Header("Спецефекти")]
    public GameObject collectParticles; // НОВЕ: Префаб часточок
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ЗБІР
        if (collision.gameObject.CompareTag("LightSphere"))
        {
            lightScore++;
            UpdateScoreText();
            if (collectParticles != null)
            {
                // Instatiate створює префаб на сцені
                GameObject effect = Instantiate(collectParticles, collision.transform.position, Quaternion.identity);

                // Видаляємо об'єкт 'effect' через 1 секунди після створення
                Destroy(effect, 1f);
            }
            if (collectSound != null) audioSource.PlayOneShot(collectSound);
            Destroy(collision.gameObject);

            // ПЕРЕВІРКА: Чи зібрали ми ВСЕ?
            if (lightScore >= totalCherries)
            {
                OpenPortal();
            }
        }

        // СМЕРТЬ
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // БІЙ З ВОРОГОМ
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (rb.linearVelocity.y < 0 && groundCheck.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
                if (stompSound != null) audioSource.PlayOneShot(stompSound);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        // ВХІД У ПОРТАЛ (ПЕРЕМОГА)
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (winText != null) winText.SetActive(true);
            Time.timeScale = 0f;
            rb.linearVelocity = Vector2.zero;
        }
    }

    // ФУНКЦІЯ ВІДКРИТТЯ ПОРТАЛУ
    private void OpenPortal()
    {
        if (exitPortal != null)
        {
            exitPortal.SetActive(true); // Робимо портал видимим
            Debug.Log("Всі сфери зібрані! Портал відчинено.");
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Світло: " + lightScore + " / " + totalCherries;
    }
}