using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public bool  Enable;

    public float ridius = 1;

    private void OnDrawGizmos()
    {
        if (!Enable)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ridius);
    }
}
