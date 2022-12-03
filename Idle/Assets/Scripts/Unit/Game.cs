using System;
using PathologicalGames;
using Tile;
using Unity.VisualScripting;
using UnityEngine;

namespace Test
{
    public class Game : MonoBehaviour
    {
        EnemyCollection enemies = new EnemyCollection();
        TowerCollection towers = new TowerCollection();


        [SerializeField] private SpawnPool spawnPool;

        [SerializeField] private Transform pfbEnemy;

        [SerializeField] private GameBoard board;

        private void Start()
        {
            board.Initialize();
        }

        void Update ()
        {
            enemies.GameUpdate();
            // Physics.SyncTransforms();
            towers.GameUpdate();
            
            if (Input.GetMouseButtonDown(0)) {
                HandleTouch();
            }
        }

        // todo
        [SerializeField, FloatRangeSlider(0.2f, 5f)]
        FloatRange enemySpeedRange = new FloatRange(1f);
        
        void SpawnEnemy (Tile.Tile tile)
        {
            var enemy = spawnPool.Spawn(pfbEnemy).GetComponent<Enemy>();
            enemies.Add(enemy);
            enemy.SpawnOn(tile);
            enemy.Init(enemySpeedRange.RandomValueInRange);
        }

        [SerializeField] private Transform pfbTower;
        
        void SpawnTower(Tile.Tile tile)
        {
            var tower = spawnPool.Spawn(pfbTower).GetComponent<Tower>();
            towers.Add(tower);
            tower.transform.position = tile.transform.position;
        }

        void HandleTouch () {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            // todo layermask只检测Default
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, float.MaxValue, 1);
            if (hit.collider != null) {
                var tile =
                    hit.collider.GetComponent<Tile.Tile>();
             
                if (tile != null)
                {
                    if (tile.Type == Tile.Tile.TileType.Road)
                    { 
                        SpawnEnemy(tile);
                    }
                    else if(tile.Type == Tile.Tile.TileType.Normal)
                    {
                        SpawnTower(tile);
                    }

                    Debug.Log(tile.gameObject.name);
                }
            }
        }
    }
}