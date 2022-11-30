/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年11月30日
 * @modify date 2022年11月30日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
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


        [Button]
        public void Initialize()
        {
            for (int index = root.childCount - 1; index >= 0; index--)
            {
                var child = root.GetChild(index);
                DestroyImmediate(child.gameObject);
            }

            for (int i = 0; i < tileSize.x; i++)
            {
                for (int j = 0; j < tileSize.y; j++)
                {
                    var tile = Instantiate(pfbTile, root);
                    tile.transform.position = new Vector2(i, j) + tileOffset;

                    if (i == tileSize.x - 1 ||
                        j == tileSize.y - 1 ||
                        i == 0 ||
                        j == 0)
                    {
                        tile.GetComponent<Tile>().ShowArrow = true;
                        tile.gameObject.name                = "Road";
                    }
                    else
                    {
                        tile.GetComponent<Tile>().ShowArrow = false;
                    }

                    if (i == 0)
                    {
                        tile.GetComponent<Tile>().direction = Tile.Direction.South;
                    }
                    else if (i == tileSize.x - 1)
                    {
                        tile.GetComponent<Tile>().direction = Tile.Direction.North;
                    }
                    else if (j == tileSize.y - 1)
                    {
                        tile.GetComponent<Tile>().direction = Tile.Direction.West;
                    }
                    else if (j == 0)
                    {
                        tile.GetComponent<Tile>().direction = Tile.Direction.East;
                    }

                    tile.GetComponent<Tile>().RefreshArrow();
                    tile.gameObject.name = "Tile";
                }
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
    }
}
#pragma warning restore 0649