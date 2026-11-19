using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8.0f; public Vector2 direction;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.linearVelocity = direction * speed;
    }

    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }
        Destroy(gameObject);
    }
}
