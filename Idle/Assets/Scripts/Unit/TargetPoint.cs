using UnityEngine;

namespace Test
{
    public class TargetPoint : MonoBehaviour
    {
        public Enemy Enemy;

        public Vector3 Position => transform.position;
    }
}