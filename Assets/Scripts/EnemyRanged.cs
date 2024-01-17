using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] protected float shootDelayMin;
    [SerializeField] protected float shootDelayMax;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform bulletPoint;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletLifetime;
    // [SerializeField] protected float bulletDivergence;

    protected float lastShotTime;
    protected float currentShotDelay;



    protected override void FixedUpdate()
    {
        Move();
        Shoot();
    }

    protected override void Move()
    {
        base.Move();
    }

    protected virtual void Shoot()
    {
        if (Time.fixedTime < lastShotTime + currentShotDelay) return;

        EnemyBullet bullet = Instantiate(bulletPrefab).GetComponent<EnemyBullet>();
        bullet.Spawn(bulletPoint.position, Player.Position, bulletSpeed, bulletLifetime);

        lastShotTime = Time.fixedTime;
        currentShotDelay = Random.Range(shootDelayMin, shootDelayMax);
    }
}
