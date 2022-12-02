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

            if (Input.GetMouseButtonDown(0)) {
                HandleTouch();
            }
        }

        void SpawnEnemy (Tile.Tile tile)
        {
            var enemy = spawnPool.Spawn(pfbEnemy).GetComponent<Enemy>();
            enemies.Add(enemy);
            enemy.SpawnOn(tile);
        }
        
        void HandleTouch () {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                var tile =
                    hit.collider.GetComponent<Tile.Tile>();
             
                if (tile != null)
                {
                    if (tile.Type == Tile.Tile.TileType.Road)
                    { 
                        SpawnEnemy(tile);
                    }

                    Debug.Log(tile.gameObject.name);
                }
            }
        }
    }
}