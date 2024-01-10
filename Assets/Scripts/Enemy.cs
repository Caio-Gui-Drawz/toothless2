using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float healthMax;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveAcceleration;
    
    [SerializeField] private Seeker seeker;
    [SerializeField] private AIPath aiPath;

    private float health;



    private void Awake()
    {
        health = healthMax;
        aiPath.maxAcceleration = moveAcceleration;
        aiPath.maxSpeed = moveSpeed;
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    [ContextMenu("Test Movement")]
    private void TestMovement()
    {
        aiPath.destination = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
