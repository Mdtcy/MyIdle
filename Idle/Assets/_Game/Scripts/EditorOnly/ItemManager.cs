
#pragma warning disable 0649
using System.Collections.Generic;
using HM;
using HM.Extensions;
using HM.GameBase;
using HM.Interface;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace NewLife.EditorOnly
{
    public class ItemManager : MonoBehaviour
    {
	    [Inject]
	    private IRequest request;

	    [Inject]
	    private IItemGetter itemGetter;


        [SerializeField]
		private List<int> itemIds;

		[SerializeField]
		private List<BaseConfig> configs;

		[GUIColor(0, 1, 0)]
		[HorizontalGroup("split"), Button("AcquireItems", ButtonSizes.Medium)]
		private void AcquireItems()
		{
			using (var items = ItemParams.Create())
			{
				if (!itemIds.IsNullOrEmpty())
				{
					itemIds.ForEach(v => items.Add(v, 1));
				}

				if (!configs.IsNullOrEmpty())
				{
					configs.ForEach(v => items.Add(v.Id, 1));
				}

				foreach (var itemParam in items)
				{
					HMLog.LogDebug(itemParam);
				}

				request.AcquireItems(items);
			}
		}

		[GUIColor(200/255.0f, 226/255.0f, 243/255.0f)]
		[HorizontalGroup("split"), Button("PrintItems", ButtonSizes.Medium)]
		private void PrintItems()
		{
			using (var items = ItemParams.Create())
			{
				if (!itemIds.IsNullOrEmpty())
				{
					itemIds.ForEach(v => items.Add(v, 1));
				}

				if (!configs.IsNullOrEmpty())
				{
					configs.ForEach(v => items.Add(v.Id, 1));
				}

				int i = 0;
				foreach (var param in items)
				{
					if (itemGetter.HasItem(param.ItemId))
					{
						Debug.Log($"{i}:{itemGetter.GetItem<ItemBase>(param.ItemId)}");
					}
					else
					{
						Debug.Log($"{i}:item not exists for id={param.ItemId}");
					}
					i++;
				}
			}
		}


    }

}
#pragma warning restore 0649
