using UnityEngine;

public class WalkingEnemy : Entity
{
    private float speed = 3.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    private void Start()
    {
        dir = transform.right;
    }

    private void Update()
    {
        Move(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.7f, 0.1f);
        if (colliders.Length > 0) dir *= -1f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }
}
