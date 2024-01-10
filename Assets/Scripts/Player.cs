using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float healthMax;
    [SerializeField] private float speedMax;
    private float health;
    private float speed;
    private Vector2 moveDirection;

    private Rigidbody2D rb;
    private InputMap input;



    // UNITY
    private void Awake()
    {
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
        Move();
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