using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Ссылка на объект игрока (или другой объект, за которым должна следовать камера)
    public float smoothSpeed = 0.125f; // Параметр плавности следования камеры

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position; // Рассчитываем начальное смещение камеры относительно игрока
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

