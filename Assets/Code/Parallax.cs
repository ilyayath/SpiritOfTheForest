using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Налаштування")]
    public Transform mainCamera;

    [Tooltip("0 = Не рухається (як земля), 1 = Рухається разом з камерою (як далеке небо)")]
    public float parallaxEffectMultiplier = 0.5f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        // Якщо камеру не призначили ручками, скрипт знайде її сам
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        lastCameraPosition = mainCamera.position;
    }

    void LateUpdate()
    {
        // Рахуємо, на скільки посунулася камера з минулого кадру
        Vector3 deltaMovement = mainCamera.position - lastCameraPosition;

        // Рухаємо наш фон. Що більший множник, то сильніше він "прилипає" до камери
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0);

        // Запам'ятовуємо нову позицію камери для наступного кадру
        lastCameraPosition = mainCamera.position;
    }
}