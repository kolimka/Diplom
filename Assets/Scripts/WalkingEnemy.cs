using UnityEngine;

public class WalkingEnemy : Entity
{
    public Transform platformStart; // ����� ������ ���������
    public Transform platformEnd;   // ����� ����� ���������
    private float currentPosition;   // ������� ������� ����������
    private float speed = 3f;
    private Vector3 dir;

    private void Awake()
    {
        currentPosition = transform.position.x;
        dir = Vector3.right;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        // ����������� ����������
        transform.Translate(dir * speed * Time.deltaTime);

        currentPosition = transform.position.x;

        // ���� ��������� ������ ������ ��� ����� ���������, ������ �����������
        if (currentPosition <= platformStart.position.x || currentPosition >= platformEnd.position.x)
        {
            dir *= -1f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }  
}
