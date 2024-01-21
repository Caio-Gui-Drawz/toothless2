using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int layer = 9;

    [SerializeField] protected float healthMax;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveAcceleration;
    [SerializeField] protected float staggerTime = .1f;
    protected float health;
    public bool IsStaggered { get; protected set;}

    protected Seeker seeker;
    protected AIPath aiPath;
    protected SpriteRenderer sprite;
    protected Coroutine flashCoroutine;
    protected Coroutine staggerCoroutine;
    


    // UNITY
    protected virtual void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
        aiPath.maxAcceleration = moveAcceleration;
        aiPath.maxSpeed = moveSpeed;
        sprite = GetComponentInChildren<SpriteRenderer>();

        health = healthMax;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        aiPath.destination = Player.Position;
    }

    public virtual void TakeDamage()
    {
        health--;

        if (health <= 0f) Death();
        else
        {
            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(TakeDamageFlash());

            if (staggerCoroutine != null) StopCoroutine(staggerCoroutine);
            staggerCoroutine = StartCoroutine(TakeDamageStagger());
        }
    }

    private IEnumerator TakeDamageFlash()
    {
        sprite.enabled = false;
        yield return new WaitForSeconds(.05f);
        sprite.enabled = true;
    }

    private IEnumerator TakeDamageStagger()
    {
        aiPath.enabled = false;
        IsStaggered = true;
        yield return new WaitForSeconds(staggerTime);
        aiPath.enabled = true;
        IsStaggered = false;
    }

    protected virtual void Death()
    {
        gameObject.SetActive(false);
    }

    // TESTING
    [ContextMenu("Test Movement")]
    private void TestMovement()
    {
        aiPath.destination = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
