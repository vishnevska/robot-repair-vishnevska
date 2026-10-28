using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    private int currentHealth;
    private bool isInvincible;
    private float damageCooldown;

    public int health
    {
        get { return currentHealth; }
    }

    void Start()
    {
        currentHealth = maxHealth;
        MoveAction.Enable();
    }

    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();

        Vector2 position = (Vector2)transform.position
            + move * speed * Time.deltaTime;

        transform.position = position;

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;

            if (damageCooldown <= 0)
            {
                isInvincible = false;
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) { return; }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(
            currentHealth + amount, 0, maxHealth);

        Debug.Log("Health: " + currentHealth + "/" + maxHealth);

        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}