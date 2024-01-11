using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static int layer = 8;
    public static Vector2 Position { get { return instance.rb.position; } }

    private bool IsDead { get { return health <= 0; } }

    [SerializeField] private int healthMax;
    [SerializeField] private float speedMax;
    [SerializeField] private float invulnerabilitySeconds;
    private int health;
    private float speed;
    private bool isInvulnerable;
    private Vector2 moveDirection;

    private Rigidbody2D rb;
    private InputMap input;



    // UNITY
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();

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
            TakeDamage();
        }
    }

    // CLASS
    public void TakeDamage()
    {
        if (isInvulnerable || IsDead) return;

        Debug.Log("Player took damage!");
        health -= 1;
        if (health <= 0) Debug.Log("Player is dead!");
        else StartCoroutine(TakeDamageInvulnerability());
    }

    private IEnumerator TakeDamageInvulnerability()
    {
        isInvulnerable = true;

        float time = 0f;
        while (time < invulnerabilitySeconds)
        {
            time += Time.deltaTime;
            yield return null;
        }

        isInvulnerable = false;
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
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {

    }

    private void OnInteract(InputAction.CallbackContext context)
    {

    }
}