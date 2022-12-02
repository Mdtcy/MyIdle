/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年11月30日
 * @modify date 2022年11月30日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using UnityEngine;

namespace Tile
{
    public class Tile : MonoBehaviour
    {
        #region FIELDS

        public Tile north, east, south, west;

        public enum TileType
        {
            Road,
            Normal
        }

        public TileType Type;
        
        public static void MakeEastWestNeighbors (Tile east, Tile west)
        {
            if (west)
            {
                west.east = east;
            }

            if (east)
            {
                east.west = west;
            }
        }
        public static void MakeNorthSouthNeighbors (Tile north, Tile south) 
        {
            if (south != null)
            {
                south.north = north;
            }

            if (north != null)
            {
                north.south = south;
            }
        }

        static Quaternion
            northRotation = Quaternion.Euler(0f, 0f, 0f),
            eastRotation  = Quaternion.Euler(0f, 0f, -90f),
            southRotation = Quaternion.Euler(0f, 0f, 180f),
            westRotation  = Quaternion.Euler(0f, 0f, 90f);

        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        public bool ShowArrow;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        Transform arrow = default;

        public Direction direction;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void RefreshArrow()
        {
            if (!ShowArrow)
            {
                if(arrow)
                    arrow.gameObject.SetActive(false);

                return;
            }

            arrow.gameObject.SetActive(true);

            switch (direction)
            {
                case Direction.North:
                    arrow.localRotation = northRotation;

                    break;
                case Direction.South:
                    arrow.localRotation = southRotation;

                    break;
                case Direction.East:
                    arrow.localRotation = eastRotation;

                    break;
                case Direction.West:
                    arrow.localRotation = westRotation;

                    break;
            }
        }

        public Tile GetNextTile()
        {
            switch (direction)
            {
                case Direction.North:
                    return north;
                case Direction.South:
                    return south;
                case Direction.East:
                    return east;
                case Direction.West:
                    return west;
            }

            return null;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnValidate()
        {
            RefreshArrow();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649