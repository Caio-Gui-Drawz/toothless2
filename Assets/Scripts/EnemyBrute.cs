using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrute : Enemy
{
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackInitialRange;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackRestTime;
    [SerializeField] private Transform attackOriginPoint;

    [Header("Testing")]
    [SerializeField] private SpriteRenderer testRadius;
    [SerializeField] private SpriteRenderer testRange;

    private bool canAttack = true;



    protected void OnValidate()
    {
        testRadius.transform.localScale = attackRadius * 2f * Vector3.one;
        testRange.transform.localScale = attackInitialRange * 2f * Vector3.one;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Spawn()
    {
        base.Spawn();

        canAttack = true;
        CanMove(true);
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canAttack && Vector2.Distance(rb.position, Player.Position) <= attackInitialRange) 
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        Debug.Log("Brute attack");
        CanMove(false);
        canAttack = false;
        yield return new WaitForSeconds(attackDelay); // TESTING

        Collider2D[] overlaps = Physics2D.OverlapCircleAll(attackOriginPoint.position, attackRadius, 1 << Player.layer);
        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.attachedRigidbody.TryGetComponent(out Player player)) player.TakeDamage(); 
        }

        yield return new WaitForSeconds(attackRestTime);
        CanMove(true);
        canAttack = true;
    }

    private void CanMove(bool isTrue)
    {
        aiPath.enabled = isTrue;
    }
}
