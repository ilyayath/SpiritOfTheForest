using UnityEngine;
using UnityEngine.SceneManagement; // Обов'язково для перемикання рівнів

public class MainMenuManager : MonoBehaviour
{
    public GameObject aboutPanel; // Сюди перетягнемо нашу панель

    // Функція для відкриття вікна
    public void OpenAbout()
    {
        aboutPanel.SetActive(true);
    }

    // Функція для закриття вікна
    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }
    // Ця функція запуститься, коли ми натиснемо "ГРАТИ"
    public void PlayGame()
    {
        // Завантажуємо сцену під номером 1 (твій ігровий рівень)
        SceneManager.LoadScene(1);
    }

    // Ця функція запуститься, коли ми натиснемо "ВИЙТИ"
    public void QuitGame()
    {
        Debug.Log("Гравець вийшов з гри!"); // Це повідомлення для нас в редакторі
        Application.Quit(); // Це закриє гру, коли вона буде скомпільована (.exe)
    }
}