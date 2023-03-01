using DefaultNamespace.Game;
using DefaultNamespace.Item;
using Game.Projectile;
using IdleGame;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public TrackBulletPool trackBulletPool;

    public Entity entity1;
    public Entity entity2;

    public void CreateBullet(Vector3 firePosition, Entity caster, Entity target)
    {
        // 生成 输入prefab 位置 旋转角度 父节点
        var trackBullet      = trackBulletPool.Get();
        var projectileTrans = trackBullet.transform;
        projectileTrans.position = firePosition;
        projectileTrans.rotation = Quaternion.identity;
        projectileTrans.SetParent(null);

        // 传入子弹所需的参数
        trackBullet.Init(caster, target);
    }

    [Button]
    public void Fire()
    {
        CreateBullet(entity1.transform.position, entity1, entity2);
    }

    public ItemDef ItemDef;

    public int Count;

    public Entity Entity;

    [Button]
    public void AddItem()
    {
        GameManager.Instance.Inventory.AddItem(ItemDef, Count);
    }
}
