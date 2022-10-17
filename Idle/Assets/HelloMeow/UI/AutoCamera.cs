/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-01-12 09:40:21
 * @modify date 2019-01-12 09:40:21
 * @desc [自动设置camera的orthographicSize]
 */
using UnityEngine;

namespace HM.UI
{
	public class AutoCamera : MonoBehaviour
	{

		public int pixelsPerUnit = 128;
		// Use this for initialization
		private void Start()
		{
			var camera = GetComponent<Camera>();
			if (camera != null)
			{
				camera.orthographicSize = (float)Screen.height / pixelsPerUnit / 2.0f;
				HMLog.LogDebug("设置camera的orthographicSize = {0}, 屏幕高度为={1}, ppu = {2}", camera.orthographicSize, Screen.height, pixelsPerUnit);
			}
		}
	}
}