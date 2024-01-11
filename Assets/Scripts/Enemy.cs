using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int layer = 9;

    [SerializeField] private float healthMax;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveAcceleration;
    private float health;

    private Seeker seeker;
    private AIPath aiPath;
    


    // UNITY
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
        aiPath.maxAcceleration = moveAcceleration;
        aiPath.maxSpeed = moveSpeed;

        health = healthMax;
    }

    // TESTING
    [ContextMenu("Test Movement")]
    private void TestMovement()
    {
        aiPath.destination = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
