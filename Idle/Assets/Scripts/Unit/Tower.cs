using PathologicalGames;
using Sirenix.OdinInspector;
using Test;
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

    [SerializeField, Range(1.5f, 10.5f)]
    float targetingRange = 1.5f;
    
    [SerializeField]
    Transform turret = default;
    [SerializeField]
    Transform laserBeam = default;
    
    private float attackTimer;
    Vector3 laserBeamScale;

    void Awake () {
        laserBeamScale = laserBeam.localScale;
    }
    private void Start()
    {
        attackTimer = attackInterval;
    }

    // // Update is called once per frame
    // private void Update()
    // {
    //     attackTimer -= Time.deltaTime;
    //
    //     if (attackTimer <= 0)
    //     {
    //         attackTimer = attackInterval;
    //         FireProjectile();
    //     }
    // }
    void Shoot () {
        Vector3 point = target.Position;
        
        // look at
        // Vector3 v = (point - turret.position).normalized;
        // turret.right = v;
        Vector2 direction = point - turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        turret.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // turret.LookAt(point);
        laserBeam.localRotation = turret.localRotation;
        
        float d = Vector2.Distance(turret.position, point);
        laserBeamScale.y = d;
        laserBeam.localScale = laserBeamScale;
        
        laserBeam.localPosition =
            turret.localPosition + 0.5f * d * laserBeam.up;
    }
    private void FireProjectile()
    {
        // var projectileObject = spawnPool.Spawn(pfbProjectile);
        // projectileObject.position = firePoint.position;
        //
        // var projectile = projectileObject.GetComponent<Projectile>();
        // projectile.
    }
    const int enemyLayerMask = 1 << 6;

    [ShowInInspector]
    [ReadOnly]
    private TargetPoint target;
    public bool GameUpdate () {
        if (TrackTarget() || AcquireTarget())
        {
            Shoot();
        }
        else {
            laserBeam.localScale = Vector3.zero;
        }

        return true;
    }
    static  Collider2D [] targetsBuffer = new  Collider2D [ 1 ];

    bool AcquireTarget () {
        
        int hits = Physics2D.OverlapCircleNonAlloc(
            transform.localPosition,  targetingRange, targetsBuffer, enemyLayerMask
        );
        if (hits > 0) {
            target = targetsBuffer[0].GetComponent<TargetPoint>();
            Debug.Assert(target != null, "Targeted non-enemy!", targetsBuffer[0]);
            return true;
        }
        
        target = null;
        return false;
    }
    
    // 是否正在追踪目标
    bool TrackTarget () {
        if (target == null) {
            return false;
        }
        
        // 如果超出了距离 则返回false
        Vector3 a = transform.localPosition;
        Vector3 b = target.Position;
        
        // todo 这里实际上要加上enemy自己的半径 暂时忽略 只加0
        if (Vector3.Distance(a, b) > targetingRange + 0) {
            target = null;
            return false;
        }
        
        return true;
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Vector3 position = transform.localPosition;
        position.y += 0.01f;
        Gizmos.DrawWireSphere(position, targetingRange);
        
        if (target != null) {
            Gizmos.DrawLine(position, target.Position);
        }
    }
}
