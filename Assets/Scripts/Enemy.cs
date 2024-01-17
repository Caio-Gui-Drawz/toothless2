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
    protected float health;

    protected Seeker seeker;
    protected AIPath aiPath;
    


    // UNITY
    protected virtual void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
        aiPath.maxAcceleration = moveAcceleration;
        aiPath.maxSpeed = moveSpeed;

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

    // TESTING
    [ContextMenu("Test Movement")]
    private void TestMovement()
    {
        aiPath.destination = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
