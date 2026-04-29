using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 3f;        // Швидкість ворога
    public Transform leftPoint;     // Ліва межа
    public Transform rightPoint;    // Права межа

    private bool movingLeft = true; // Чи йдемо ми зараз вліво?

    void Update()
    {
        // Якщо йдемо вліво
        if (movingLeft)
        {
            // Рухаємо об'єкт вліво
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Якщо дійшли до лівої точки
            if (transform.position.x <= leftPoint.position.x)
            {
                movingLeft = false; // Змінюємо напрямок
                Flip(); // Розвертаємо картинку
            }
        }
        // Якщо йдемо вправо
        else
        {
            // Рухаємо об'єкт вправо
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // Якщо дійшли до правої точки
            if (transform.position.x >= rightPoint.position.x)
            {
                movingLeft = true; // Змінюємо напрямок
                Flip(); // Розвертаємо картинку
            }
        }
    }

    // Функція для розвороту лиця ворога
    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}