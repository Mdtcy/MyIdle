using HM.CustomAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HM.GameBase
{
	public class BaseConfig : ScriptableObject
	{
		/// <summary>
		/// Id
		/// </summary>
		[PropertyOrder(1)]
		public int Id;

		/// <summary>
		/// 名字
		/// </summary>
		[PropertyOrder(2)]
		[LabelText("名称")]
		public string Name = string.Empty;

		/// <summary>
		/// 图片
		/// </summary>
		[PropertyOrder(3)]
		[PreviewField]
		[CustomFieldName("图片")]
        [LabelText("@\"图片:\"+(Image==null?\"\":Image.name)")]
		public Sprite Image;

		/// <summary>
		/// 备注
		/// </summary>
		[LabelText("备注")]
		public string note;

		/// <summary>
		/// 拿到ItemId的最后三位（编号）
		/// </summary>
		/// <returns></returns>
		public int ConfigIndex => Id % 1000;

		/// <summary>
		/// 在配置管理器中创建新配置时会调用该函数
		/// </summary>
		public virtual void OnCreated()
		{
		}

		public virtual void AddToItemParams(int num, ItemParams items)
		{
			items.Add(Id, num);
		}

		/// <summary>
		/// 短介绍
		/// </summary>
		/// <returns></returns>
		public string ShortDesc()
		{
			return $"[{Id}/{Name}]";
		}
	}
}