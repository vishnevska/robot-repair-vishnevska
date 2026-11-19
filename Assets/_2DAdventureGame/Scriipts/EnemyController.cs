using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;
    int hitCount = 0;

    static int brokenEnemiesCount = 0;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
        brokenEnemiesCount++;
    }

    void OnDestroy()
    {
        brokenEnemiesCount = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y += speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        if (!broken)
        {
            return;
        }

        hitCount++;

        if (hitCount >= 3)
        {
            broken = false;
            rigidbody2d.simulated = false;
            animator.SetTrigger("Fixed");
            brokenEnemiesCount--;

            if (brokenEnemiesCount <= 0)
            {
                UIHandler.instance.ShowWin();
            }
        }
    }
}