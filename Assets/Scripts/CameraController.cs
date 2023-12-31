using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ������ �� ������ ������ (��� ������ ������, �� ������� ������ ��������� ������)
    public float smoothSpeed = 0.125f; // �������� ��������� ���������� ������

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position; // ������������ ��������� �������� ������ ������������ ������
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

