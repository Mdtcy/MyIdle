using UnityEngine;

namespace Test
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    
    public static class DirectionExtensions {

        static Quaternion[] rotations = {
            Quaternion.identity,
            Quaternion.Euler(0f, 0f, -90f),
            Quaternion.Euler(0f, 0f, 180f),
            Quaternion.Euler(0f, 0f, 90f)
        };

        public static Quaternion GetRotation (this Direction direction) {
            return rotations[(int)direction];
        }
    }
}