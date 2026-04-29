using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Інтерфейс")]
    public GameObject pauseMenuUI; // Сюди покладемо нашу PausePanel

    private bool isPaused = false;

    void Update()
    {
        // Перевіряємо, чи натиснув гравець клавішу ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); // Якщо вже на паузі - продовжуємо
            }
            else
            {
                Pause(); // Якщо гра йде - ставимо паузу
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Ховаємо меню
        Time.timeScale = 1f;          // Відновлюємо час
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Показуємо меню
        Time.timeScale = 0f;         // Зупиняємо всі рухи, фізику і ворогів
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;       // ВАЖЛИВО! Відновлюємо час перед виходом, інакше меню зависне
        SceneManager.LoadScene(0); // Завантажуємо нульову сцену (MainMenu)
    }
}