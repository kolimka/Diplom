using UnityEngine;

public class WalkingEnemy : Entity
{
    public Transform platformStart; // Точка начала платформы
    public Transform platformEnd;   // Точка конца платформы
    private float currentPosition;   // Текущая позиция противника
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
        // Передвигаем противника
        transform.Translate(dir * speed * Time.deltaTime);

        currentPosition = transform.position.x;

        // Если противник достиг начала или конца платформы, меняем направление
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
