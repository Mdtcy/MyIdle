/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-24 09:12:01
 * @modify date 2018-12-24 09:12:01
 * @desc [实现DontDestroyOnLoad组件]
 */
using UnityEngine;

namespace HM
{
	/// <summary>
	/// 直接绑定至gameObject，则该gameObject具备跨场景生命周期的能力
	/// </summary>
	public class DontDestroyOnLoad : MonoBehaviour
	{

		private static DontDestroyOnLoad _instance;
		// Use this for initialization
		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else if (_instance != this)
			{
				Object.Destroy(gameObject);
			}
		}
	}
}