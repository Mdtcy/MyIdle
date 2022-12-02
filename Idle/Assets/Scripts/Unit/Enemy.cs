using HM;
using UnityEngine;

namespace Test
{
    public class Enemy : MonoBehaviour
    {
        Tile.Tile tileFrom, tileTo;
        Vector3 positionFrom, positionTo;

        private Tile.Tile.Direction direction = default;
        
        float progress;
        public bool GameUpdate () {
            progress += Time.deltaTime;
            while (progress >= 1f) {
                tileFrom = tileTo;
                tileTo = tileTo.GetNextTile();
                direction = tileFrom.direction;
                positionFrom = positionTo;
                positionTo = tileTo.transform.localPosition;
                progress -= 1f;
            }
            transform.localPosition =
                Vector3.LerpUnclamped(positionFrom, positionTo, progress);
            return true;
        }
        
        public void SpawnOn (Tile.Tile tile)
        {
            transform.position = tile.transform.position;
            tileFrom = tile;
            tileTo = tile.GetNextTile();
            HMLog.Assert(tileTo != null);
            direction = tile.direction;
            positionFrom = tileFrom.transform.localPosition;
            positionTo = tileTo.transform.localPosition;
            progress = 0f;
        }
    }
}