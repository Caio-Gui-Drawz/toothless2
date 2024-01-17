using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    private Rigidbody2D rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.position += Time.fixedDeltaTime * speed * (Vector2)direction; 
    }

    public void Spawn(Vector3 position, Vector3 target, float speed, float lifetime)
    {
        direction = (Vector2)target - rb.position;
        this.speed = speed;
        StartCoroutine(DeleteAfterDelay(lifetime));
    }

    private IEnumerator DeleteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}