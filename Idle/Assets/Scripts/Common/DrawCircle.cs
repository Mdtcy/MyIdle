using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public float ridius = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ridius);
    }
}
