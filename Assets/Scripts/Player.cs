using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static int layer = 8;

    public static Vector3 Position { get { return (Vector3)instance.rb.position; } }
    private bool IsDead { get { return health <= 0; } }

    [SerializeField] private int healthMax;
    [SerializeField] private float speedMax;
    [SerializeField] private float invulnerabilitySeconds;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRadius;

    private int health;
    private float speed;
    private bool isInvulnerable;
    private Vector2 moveDirection;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private InputMap input;



    // UNITY
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        health = healthMax;
        speed = speedMax;
    }

    private void Start()
    {
        InputInit();
    }

    private void FixedUpdate()
    {
        if (IsDead) return;

        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == Enemy.layer)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
                if (!enemy.IsStaggered) TakeDamage();
        }
    }

    // CLASS
    public void TakeDamage()
    {
        if (isInvulnerable || IsDead) return;

        Debug.Log("Player took damage!");
        health -= 1;
        if (health <= 0) Death();
        else StartCoroutine(TakeDamageInvulnerability());
    }

    private IEnumerator TakeDamageInvulnerability()
    {
        sprite.enabled = false;
        isInvulnerable = true;

        float visibilityDuration = .1f;
        int repetitions = Mathf.FloorToInt(invulnerabilitySeconds / visibilityDuration);
        for (int i = 0; i < repetitions; i++)
        {
            yield return new WaitForSeconds(visibilityDuration);

            sprite.enabled = !sprite.enabled;
        }

        sprite.enabled = true;
        isInvulnerable = false;
    }

    private void Death()
    {
        Debug.Log("Player is dead!");
        health = 0;
        sprite.enabled = false;
        isInvulnerable = true;
    }

    // INPUT
    private void InputInit()
    {
        input = new InputMap();
        input.Enable();

        input.Game.Attack.performed += OnAttack;
        input.Game.Interact.performed += OnInteract;
    }

    private void Move()
    {
        moveDirection = input.Game.Move.ReadValue<Vector2>();
        if (moveDirection != Vector2.zero)
        {
            rb.position += speed * Time.fixedDeltaTime * moveDirection;
            Camera.main.transform.position = new Vector3(rb.position.x, rb.position.y, -10f);
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Vector2 attackDirection = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - (Vector3)rb.position).normalized;

        RaycastHit2D overlap = Physics2D.CircleCast(rb.position, attackRadius, attackDirection, attackRange, 1 << Enemy.layer);
        if (overlap.rigidbody != null)
        {
            Debug.Log("Player attacked " + overlap.rigidbody.name);
            if (overlap.rigidbody.TryGetComponent(out Enemy enemy)) enemy.TakeDamage();
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {

    }
}