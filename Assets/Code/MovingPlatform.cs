using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Налаштування маршруту")]
    public Transform posA; // Точка старту
    public Transform posB; // Точка фінішу
    public float speed = 3f; // Швидкість платформи

    private Vector3 targetPos;

    void Start()
    {
        // На старті кажемо платформі їхати до точки B
        targetPos = posB.position;
    }

    void Update()
    {
        // Перевіряємо, чи доїхали ми до точки А (якщо так - їдемо до В)
        if (Vector2.Distance(transform.position, posA.position) < 0.1f) targetPos = posB.position;

        // Перевіряємо, чи доїхали ми до точки В (якщо так - їдемо до А)
        if (Vector2.Distance(transform.position, posB.position) < 0.1f) targetPos = posA.position;

        // Рухаємо платформу
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    // МАГІЯ ПРИВ'ЯЗКИ: Коли гравець стає на платформу
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Робимо гравця "дитиною" платформи
            collision.transform.SetParent(transform);
        }
    }

    // Коли гравець зістрибує з платформи
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Відкріплюємо гравця
            collision.transform.SetParent(null);
        }
    }
}