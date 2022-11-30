using PathologicalGames;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float attackInterval;

    [SerializeField]
    private Transform pfbProjectile;

    [SerializeField]
    private SpawnPool spawnPool;

    [SerializeField]
    private Transform firePoint;


    private float attackTimer;

    private void Start()
    {
        attackTimer = attackInterval;
    }

    // Update is called once per frame
    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            attackTimer = attackInterval;
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        // var projectileObject = spawnPool.Spawn(pfbProjectile);
        // projectileObject.position = firePoint.position;
        //
        // var projectile = projectileObject.GetComponent<Projectile>();
        // projectile.
    }
}
