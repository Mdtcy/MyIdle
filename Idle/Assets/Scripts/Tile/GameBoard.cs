/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年11月30日
 * @modify date 2022年11月30日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tile
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField]
        Transform ground = default;

        [SerializeField]
        private Transform root;

        [SerializeField]
        private Transform pfbTile;

        [SerializeField]
        private Transform board;

        [SerializeField]
        private Transform boardRoot;

        [SerializeField]
        private Vector2Int tileSize;

        [SerializeField]
        private Vector2Int tileOffset;

        [ShowInInspector]
        public Tile[][] tiles;

        

        [Button]
        public void Initialize()
        {
            for (int index = root.childCount - 1; index >= 0; index--)
            {
                var child = root.GetChild(index);
                DestroyImmediate(child.gameObject);
            }

            tiles = null;
            tiles = new Tile[tileSize.x][];
            for (var index = 0; index < tiles.Length; index++)
            {
                tiles[index] = new Tile[tileSize.y];
            }
            for (int x = 0; x < tileSize.x; x++)
            {
                for (int y = 0; y < tileSize.y; y++)
                {
                    var tile = Instantiate(pfbTile, root);
                    tile.transform.position = new Vector2(x, y) + tileOffset;
                    tiles[x][y] = tile.GetComponent<Tile>();
                }
            }
            for (int x = 0; x < tileSize.x; x++)
            {
                for (int y = 0; y < tileSize.y; y++)
                {
                    var tile = tiles[x][y];
                    if (x == tileSize.x - 1 ||
                        y == tileSize.y - 1 ||
                        x == 0 ||
                        y == 0)
                    {
                        tile.GetComponent<Tile>().ShowArrow = true;
                        tile.gameObject.name = $"Road{x}{y}";
                        tile.Type = Tile.TileType.Road;
                        MakeTile(x, y);
                    }
                    else
                    {
                        tile.GetComponent<Tile>().ShowArrow = false;
                        tile.gameObject.name = $"Tile{x}{y}";
                        tile.Type = Tile.TileType.Normal;
                    }

                    if (x == 0)
                    {
                        if (y == 0)
                        {
                            tile.GetComponent<Tile>().direction = Tile.Direction.East;
                        }
                        else
                        {
                            tile.GetComponent<Tile>().direction = Tile.Direction.South;
                        }
                    }
                    else if (x == tileSize.x - 1)
                    {
                        if (y == tileSize.y - 1)
                        {
                            tile.GetComponent<Tile>().direction = Tile.Direction.West;
                        }
                        else
                        {
                            tile.GetComponent<Tile>().direction = Tile.Direction.North;
                        }
                    }
                    else if (y == tileSize.y - 1)
                    {
                        tile.GetComponent<Tile>().direction = Tile.Direction.West;
                    }
                    else if (y == 0)
                    {
                        tile.GetComponent<Tile>().direction = Tile.Direction.East;
                    }

                    tile.GetComponent<Tile>().RefreshArrow();
                }
            }
        }

        private void MakeTile(int x, int y )
        {
            if (y + 1 < tileSize.y)
            {
                Tile.MakeNorthSouthNeighbors(tiles[x][y + 1], tiles[x][y]);
            }
            else if (y - 1 >= 0)
            {
                Tile.MakeNorthSouthNeighbors(tiles[x][y], tiles[x][y - 1]);
            }

            if (x - 1 > 0)
            {
                Tile.MakeEastWestNeighbors(tiles[x][y], tiles[x-1][y]);
            }
            else if (x + 1 < tileSize.x)
            {
                Tile.MakeEastWestNeighbors(tiles[x + 1][y], tiles[x][y]);
            }
        }

        [SerializeField]
        private Vector2Int boardSize;

        [SerializeField]
        private Vector2Int boardoffset;

        [Button]
        public void InitBoard()
        {
            for (int index = boardRoot.childCount - 1; index >= 0; index--)
            {
                var child = boardRoot.GetChild(index);
                DestroyImmediate(child.gameObject);
            }

            for (int i = 0; i < boardSize.x; i++)
            {
                for (int j = 0; j < boardSize.y; j++)
                {
                    var tile = Instantiate(board, boardRoot);
                    tile.transform.position = new Vector2(i, j) + boardoffset;
                }
            }

        }

        public Tile GetTile()
        {
            // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            // RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            // if (hit.collider != null) {
            //     var tile =
            //         hit.collider.GetComponent<Tile>();
            //     return tile;
            // }
            return null;
        }
    }
}
#pragma warning restore 0649