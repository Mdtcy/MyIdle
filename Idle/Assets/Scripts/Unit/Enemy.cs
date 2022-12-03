using HM;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Test
{
    public class Enemy : MonoBehaviour
    {
        [Inject(Id = "EnemyPool")]
        private SpawnPool enemyPool;
        
        Tile.Tile tileFrom, tileTo;
        Vector3 positionFrom, positionTo;

        [ReadOnly]
        public float Health;

        public float MaxHelth;

        private float speed;
        
        
        private Direction direction = default;

        float progress;
        
        public void ApplyDamage (float damage) {
            Debug.Assert(damage >= 0f, "Negative damage applied.");
            Health -= damage;
        }
        
        public bool GameUpdate () {
            
            if (Health <= 0f) {
                enemyPool.Despawn(transform);
                return false;
            }
            
            progress += Time.deltaTime * speed;
            while (progress >= 1f) {
                tileFrom = tileTo;
                tileTo = tileTo.GetNextTile();
                direction = tileFrom.direction;
                transform.localRotation = direction.GetRotation();
                positionFrom = positionTo;
                positionTo = tileTo.transform.localPosition;
                progress -= 1f;
            }
            transform.localPosition =
                Vector3.LerpUnclamped(positionFrom, positionTo, progress);
            return true;
        }

        public void Init(float speed)
        {
            this.speed = speed;
            this.Health = MaxHelth;
        }

        public void SpawnOn (Tile.Tile tile)
        {
            transform.position = tile.transform.position;
            tileFrom = tile;
            tileTo = tile.GetNextTile();
            HMLog.Assert(tileTo != null);
            direction = tile.direction;
            transform.localRotation = direction.GetRotation();
            positionFrom = tileFrom.transform.localPosition;
            positionTo = tileTo.transform.localPosition;
            progress = 0f;
        }
    }
}