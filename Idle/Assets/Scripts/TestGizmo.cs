using UnityEngine;

public class TestGizmo : MonoBehaviour
{
    [SerializeField]
    private float r = 1;

    [SerializeField]
    private Color color;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, r);
    }
}
