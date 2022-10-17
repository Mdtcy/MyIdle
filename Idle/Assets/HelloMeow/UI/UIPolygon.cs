using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class UIPolygon : MonoBehaviour, ICanvasRaycastFilter
{
    private new PolygonCollider2D collider;
	private RectTransform rectTransform;

	public void Awake() {
		collider = GetComponent<PolygonCollider2D>();
		rectTransform = GetComponent<RectTransform>();
	}

	public bool IsRaycastLocationValid(Vector2 screenPosition, Camera raycastEventCamera)
	{
		if (collider == null || rectTransform == null)
			return true;

		/*	Screen point local to child rectTrans position	*/
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,
			screenPosition, raycastEventCamera,
			out var localPoint);

		/*	Scaled Point	*/
		var pivot = rectTransform.pivot - new Vector2(0.5f, 0.5f);
		var pivotScaled = Vector2.Scale(rectTransform.rect.size, pivot);
		var realPoint = localPoint + pivotScaled;

		if(ContainsPoint(collider.points, realPoint))
		{
			return true;
		}
		return false;
	}

	/*	Check if polygon contains point	*/
	private static bool ContainsPoint(Vector2[] points, Vector2 p) {
		int j = points.Length - 1;
		bool inside = false;

	   for (int i = 0; i<points.Length; j = i++) {
		  if ( ((points[i].y <= p.y && p.y< points[j].y) || (points[j].y <= p.y && p.y< points[i].y)) &&
			 (p.x< (points[j].x - points[i].x) * (p.y - points[i].y) / (points[j].y - points[i].y) + points[i].x))
			 inside = !inside;
	   }
	   return inside;
	}
}